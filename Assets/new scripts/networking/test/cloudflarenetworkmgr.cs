using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;

// Data structures matching our Cloudflare Worker JSON payloads
[Serializable]
public class HostPayload { public string offer; }

[Serializable]
public class JoinPayload { public string answer; }

[Serializable]
public class JoinResponse { public string offer; }

[Serializable]
public class IcePayload { public string candidate; }

[Serializable]
public class SyncResponse
{
    public bool connected;
    public string answer;
    public string[] remoteIceCandidates;
}

public class CloudflareNetworkManager : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private string workerBaseUrl = "https://your-worker-name.workers.dev";
    [SerializeField] private string targetRoomId = "Lobby123";

    // JSLIB Native Bridge Imports
    [DllImport("__Internal__")] private static extern void InitWebRTC(bool isHost);
    [DllImport("__Internal__")] private static extern void SendNetworkMessage(string messageStr);

    private bool _isHost;
    private bool _isP2PConnected;
    private string _currentRole => _isHost ? "host" : "client";

    // ==========================================
    // 1. PUBLIC UNITY INTERFACE (UI Buttons call these)
    // ==========================================

    public void StartGameAsHost()
    {
        _isHost = true;
        Debug.Log($"[Network] Starting Host routine for Room: {targetRoomId}");

#if !UNITY_EDITOR && UNITY_WEBGL
        InitWebRTC(true); // Triggers jslib -> browser creates Offer and fires local event
#else
        Debug.LogWarning("WebRTC native bridge skipped. You must run this in a WebGL browser build!");
#endif
    }

    public void StartGameAsClient()
    {
        _isHost = false;
        Debug.Log($"[Network] Starting Client routine for Room: {targetRoomId}");

#if !UNITY_EDITOR && UNITY_WEBGL
        InitWebRTC(false); // Triggers jslib -> browser prepares to receive connection
#endif
    }

    public void SendChatMessage(string text)
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        SendNetworkMessage(text);
#endif
    }

    // ==========================================
    // 2. BACKEND CALLBACKS FROM JSLIB (Do not rename)
    // ==========================================

    // Automatically triggered by window.webrtcPeer.onicecandidate in browser
    public void OnIceCandidateFound(string iceCandidateJson)
    {
        StartCoroutine(PostIceCandidate(iceCandidateJson));
    }

    // Automatically triggered when browser completes token swap and establishes direct data channel
    public void OnPeerConnected()
    {
        _isP2PConnected = true;
        Debug.Log("<color=green>[Network] SUCCESS! WebRTC Direct P2P Channel is open. Cloudflare disconnected.</color>");
    }

    // Automatically triggered when data arrives via WebRTC DataChannel
    public void OnMessageReceived(string data)
    {
        Debug.Log($"[Game Data Received]: {data}");
    }

    // ==========================================
    // 3. CLOUDFLARE SIGNALING ROUTINES
    // ==========================================

    // Automatically triggered if we are hosting, right after JS finishes cooking the Local SDP Offer
    public void OnLocalOfferCreated(string sdpOffer)
    {
        StartCoroutine(UploadHostOffer(sdpOffer));
    }

    // Automatically triggered if we are a client, right after JS finishes cooking our Local SDP Answer
    public void OnLocalAnswerCreated(string sdpAnswer)
    {
        StartCoroutine(UploadClientAnswer(sdpAnswer));
    }

    private IEnumerator UploadHostOffer(string offer)
    {
        string url = $"{workerBaseUrl}/api/room/{targetRoomId}/host";
        HostPayload payload = new HostPayload { offer = offer };
        string json = JsonUtility.ToJson(payload);

        using (UnityWebRequest request = UnityWebRequest.PostWwwForm(url, json))
        {
            SetupJsonPost(request, json);
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"[Network] Host registration failed: {request.error}");
                yield break;
            }

            Debug.Log("[Network] Room registered on Cloudflare. Starting client synchronization loop...");
            StartCoroutine(PollSignalingServer());
        }
    }

    private IEnumerator UploadClientAnswer(string answer)
    {
        string url = $"{workerBaseUrl}/api/room/{targetRoomId}/join";
        JoinPayload payload = new JoinPayload { answer = answer };
        string json = JsonUtility.ToJson(payload);

        using (UnityWebRequest request = UnityWebRequest.PostWwwForm(url, json))
        {
            SetupJsonPost(request, json);
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"[Network] Join handshake failed: {request.error}");
                yield break;
            }

            // Cloudflare responds to the join with the Host's original offer string
            JoinResponse response = JsonUtility.FromJson<JoinResponse>(request.downloadHandler.text);

#if !UNITY_EDITOR && UNITY_WEBGL
            // Push Host Offer down to Javascript to finalize Client handshake
            Application.ExternalEval($"window.webrtcPeer.setRemoteDescription(new RTCSessionDescription({response.offer}));");
#endif

            Debug.Log("[Network] Exchanged tokens with Host. Syncing ICE candidates...");
            StartCoroutine(PollSignalingServer());
        }
    }

    private IEnumerator PollSignalingServer()
    {
        string url = $"{workerBaseUrl}/api/room/{targetRoomId}/sync?role={_currentRole}";

        // Loop continuously every 1.5 seconds until WebRTC sets up the P2P hardware pipe
        while (!_isP2PConnected)
        {
            using (UnityWebRequest request = UnityWebRequest.Get(url))
            {
                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.Success)
                {
                    SyncResponse syncData = JsonUtility.FromJson<SyncResponse>(request.downloadHandler.text);

                    // If we are hosting and a client joins, pass their remote Session Description into WebRTC
                    if (_isHost && syncData.connected && !string.IsNullOrEmpty(syncData.answer))
                    {
#if !UNITY_EDITOR && UNITY_WEBGL
                        Application.ExternalEval($"if(!window.webrtcPeer.remoteDescription) window.webrtcPeer.setRemoteDescription(new RTCSessionDescription({syncData.answer}));");
#endif
                    }

                    // Feed any external ICE candidates found by our peer into the browser engine
                    if (syncData.remoteIceCandidates != null)
                    {
                        foreach (string candidateStr in syncData.remoteIceCandidates)
                        {
#if !UNITY_EDITOR && UNITY_WEBGL
                            Application.ExternalEval($"window.webrtcPeer.addIceCandidate(new RTCIceCandidate({candidateStr})).catch(e=>{});");
#endif
                        }
                    }
                }
            }

            yield return new WaitForSeconds(1.5f); // 1.5s interval keeps server usage low and saves execution costs
        }
    }

    private IEnumerator PostIceCandidate(string iceCandidateJson)
    {
        string url = $"{workerBaseUrl}/api/room/{targetRoomId}/ice?role={_currentRole}";
        IcePayload payload = new IcePayload { candidate = iceCandidateJson };
        string json = JsonUtility.ToJson(payload);

        using (UnityWebRequest request = UnityWebRequest.PostWwwForm(url, json))
        {
            SetupJsonPost(request, json);
            yield return request.SendWebRequest();
        }
    }

    private void SetupJsonPost(UnityWebRequest req, string jsonPayload)
    {
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonPayload);
        req.uploadHandler = new UploadHandlerRaw(bodyRaw);
        req.downloadHandler = new DownloadHandlerBuffer();
        req.SetRequestHeader("Content-Type", "application/json");
    }
}