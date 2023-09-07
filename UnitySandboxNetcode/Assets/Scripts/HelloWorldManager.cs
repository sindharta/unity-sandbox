using System;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;

namespace HelloWorld {
public class HelloWorldManager : MonoBehaviour {
    void OnGUI() {
        GUILayout.BeginArea(new Rect(10, 10, 300, 300));
        bool isServer = NetworkManager.Singleton.IsServer;
        if (!NetworkManager.Singleton.IsClient && !isServer) {
            DrawStartButtons();
        } else {
            DrawStatusLabels();
            DrawSubmitNewPositionButton();
        }

        GUILayout.EndArea();
        
        if (!isServer) 
            return;

        GUILayout.BeginArea(new Rect(10, 310, 300, 300));
        if (GUILayout.Button(m_autoMove ? "Turn OFF auto-move" : "Turn ON auto-move")) {
            m_autoMove = !m_autoMove;
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
                    NetworkManager.Singleton.SpawnManager.GetPlayerNetworkObject(uid).GetComponent<HelloWorldPlayer>().NetworkMove();
            } else {
                NetworkObject    playerObject = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject();
                HelloWorldPlayer player       = playerObject.GetComponent<HelloWorldPlayer>();
                player.NetworkMove();
            }
        }
    }

    void Update() {
        bool isServer = NetworkManager.Singleton.IsServer;
        if (!isServer)
            return;

        if (!m_autoMove)
            return;
        
        //Move players automatically
        foreach (ulong uid in NetworkManager.Singleton.ConnectedClientsIds) { 
            HelloWorldPlayer   player = NetworkManager.Singleton.SpawnManager.GetPlayerNetworkObject(uid).GetComponent<HelloWorldPlayer>();
            
            float   theta = Time.frameCount / 10.0f;
            float   dist  =  (uid + 1) * 1.5f;
            Vector3 pos   = new Vector3((float)Math.Cos(theta) * dist, 0.0f, (float)Math.Sin(theta) * dist);
            player.NetworkMoveByServer(pos);
        }
    }

//--------------------------------------------------------------------------------------------------------------------------------------------------------------
    
    private bool m_autoMove = false;

}
}