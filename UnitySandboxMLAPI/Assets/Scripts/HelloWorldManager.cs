using MLAPI;
using UnityEngine;

namespace HelloWorld {
public class HelloWorldManager : MonoBehaviour {

    void OnGUI() {

        if (null == m_buttonTextStyle) {
            m_buttonTextStyle = new GUIStyle(GUI.skin.button) {
                fontSize = 36, 
            };            
        }
        
        GUILayout.BeginArea(new Rect(10, 10, 300, 300));
        if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer) {
            DrawStartButtons();
        }
        else {
            DrawStatusLabels();

            SubmitNewPosition();
        }

        GUILayout.EndArea();
    }
    
//----------------------------------------------------------------------------------------------------------------------    

    void DrawStartButtons() {
        if (GUILayout.Button("Host",m_buttonTextStyle, GUILayout.Width(BUTTON_WIDTH), GUILayout.Height(BUTTON_HEIGHT))) 
            NetworkManager.Singleton.StartHost();
        if (GUILayout.Button("Client", m_buttonTextStyle,GUILayout.Width(BUTTON_WIDTH), GUILayout.Height(BUTTON_HEIGHT))) 
            NetworkManager.Singleton.StartClient();
        if (GUILayout.Button("Server", m_buttonTextStyle,GUILayout.Width(BUTTON_WIDTH), GUILayout.Height(BUTTON_HEIGHT))) 
            NetworkManager.Singleton.StartServer();
    }

//----------------------------------------------------------------------------------------------------------------------    
    static void DrawStatusLabels() {
        var mode = NetworkManager.Singleton.IsHost ? "Host" : NetworkManager.Singleton.IsServer ? "Server" : "Client";

        GUILayout.Label("Transport: " +
            NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetType().Name);
        GUILayout.Label("Mode: " + mode);
    }

//----------------------------------------------------------------------------------------------------------------------    
    
    void SubmitNewPosition() {
        if (GUILayout.Button(NetworkManager.Singleton.IsServer ? "Move" : "Request Position Change", 
            m_buttonTextStyle, GUILayout.Width(BUTTON_WIDTH), GUILayout.Height(BUTTON_HEIGHT))) 
        {
            if (NetworkManager.Singleton.ConnectedClients.TryGetValue(NetworkManager.Singleton.LocalClientId,
                out var networkedClient)) {
                var player = networkedClient.PlayerObject.GetComponent<HelloWorldPlayer>();
                if (player) {
                    player.RequestMove();
                }
            }
        }
    }

//----------------------------------------------------------------------------------------------------------------------


    private GUIStyle m_buttonTextStyle;
        
    private const int BUTTON_WIDTH = 360;
    private const int BUTTON_HEIGHT = 60;

}
}