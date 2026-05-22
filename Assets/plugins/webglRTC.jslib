mergeInto(LibraryManager.library, {
    
    InitWebRTC: function(isHost) {
        window.webrtcPeer = new RTCPeerConnection({
            iceServers: [{ urls: 'stun:stun.l.google.com:19302' }] // Free STUN Server for NAT punching
        });

        // Add inside the InitWebRTC function block in WebGLWebRTC.jslib:

        if (isHost) {
            window.webrtcPeer.createOffer().then(offer => {
                window.webrtcPeer.setLocalDescription(offer);
                // Stringify and send up to Unity's C# script
                SendMessage('NetworkManager', 'OnLocalOfferCreated', JSON.stringify(offer));
            });
        } else {
            // If client, wait to capture the offer, then generate an answer:
            window.webrtcPeer.onnegotiationneeded = function() {
                window.webrtcPeer.createAnswer().then(answer => {
                    window.webrtcPeer.setLocalDescription(answer);
                    SendMessage('NetworkManager', 'OnLocalAnswerCreated', JSON.stringify(answer));
                });
            };
        }
        // Gather ICE network candidates to share via your Cloudflare Worker
        window.webrtcPeer.onicecandidate = function(event) {
            if (event.candidate) {
                // Send this candidate string back to Unity to upload to Cloudflare
                SendMessage('NetworkManager', 'OnIceCandidateFound', JSON.stringify(event.candidate));
            }
        };

        function setupChannelEvents(channel) {
            channel.onopen = function() { SendMessage('NetworkManager', 'OnPeerConnected'); };
            channel.onmessage = function(e) { SendMessage('NetworkManager', 'OnMessageReceived', e.data); };
        }
    },

    SendNetworkMessage: function(messageStr) {
        var msg = UTF8ToString(messageStr);
        if (window.gameChannel && window.gameChannel.readyState === "open") {
            window.gameChannel.send(msg);
        }
    }
});