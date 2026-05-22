using System.Runtime.InteropServices;
using UnityEngine;

public class WebRTCBridge : MonoBehaviour
{
    // Link to our .jslib functions
    [DllImport("__Internal__")]
    private static extern void InitWebRTC(bool isHost);

    [DllImport("__Internal__")]
    private static extern void SendNetworkMessage(string messageStr);

    public void StartAsHost()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        InitWebRTC(true);
#endif
    }

    // Called automatically from JS when network data arrives
    public void OnMessageReceived(string message)
    {
        Debug.Log("Data received from peer: " + message);
        // Handle your custom game serialization/input sync here
    }

    public void OnPeerConnected()
    {
        Debug.Log("P2P Pipe established via WebRTC!");
    }
}
