using Unity.Netcode;
using UnityEngine;

namespace HelloWorld {
public class HelloWorldManager : MonoBehaviour {
    void OnGUI() {
        GUILayout.BeginArea(new Rect(10, 10, 300, 600));
        if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer) {
            DrawStartButtons();
        } else {
            DrawStatusLabels();
            DrawSubmitNewPositionButton();
        }

        GUILayout.EndArea();
    }

    static void DrawStartButtons() {
        if (GUILayout.Button("Host")) NetworkManager.Singleton.StartHost();
        if (GUILayout.Button("Client")) NetworkManager.Singleton.StartClient();
        if (GUILayout.Button("Server")) NetworkManager.Singleton.StartServer();
    }

    static void DrawStatusLabels() {
        string mode = NetworkManager.Singleton.IsHost ? "Host" : NetworkManager.Singleton.IsServer ? "Server" : "Client";

        GUILayout.Label("Transport: " + NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetType().Name);
        GUILayout.Label("Mode: " + mode);
    }

    static void DrawSubmitNewPositionButton() {
        if (GUILayout.Button(NetworkManager.Singleton.IsServer ? "Move" : "Request Position Change")) {
            if (NetworkManager.Singleton.IsServer && !NetworkManager.Singleton.IsClient) {
                foreach (ulong uid in NetworkManager.Singleton.ConnectedClientsIds)
                    NetworkManager.Singleton.SpawnManager.GetPlayerNetworkObject(uid).GetComponent<HelloWorldPlayer>().Move();
            } else {
                NetworkObject    playerObject = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject();
                HelloWorldPlayer player       = playerObject.GetComponent<HelloWorldPlayer>();
                player.Move();
            }
        }
    }
}
}