using MLAPI;
using MLAPI.NetworkVariable;
using UnityEngine;

public class NetworkVariableTest : NetworkBehaviour {

    void Start() {
        ClientNetworkVariable.Settings.WritePermission = NetworkVariablePermission.OwnerOnly;
        ClientNetworkVariable.Settings.ReadPermission  = NetworkVariablePermission.ServerOnly;

        if (IsServer) {
            ServerNetworkVariable.Value = 0.0f;
            Debug.Log("Server's var initialized to: " + ServerNetworkVariable.Value);
        }
        else if (IsClient) {
            ClientNetworkVariable.Value = 0.0f;
            Debug.Log("Client's var initialized to: " + ClientNetworkVariable.Value);
        }
    }

    void Update() {
        float timeNow = Time.time;
        if (IsServer) {
            ServerNetworkVariable.Value = ServerNetworkVariable.Value + 0.1f;
            if (timeNow - m_lastLogTime > 0.5f) {
                m_lastLogTime = timeNow;
                Debug.Log("Server set its var to: " + ServerNetworkVariable.Value + ", has client var at: " +
                    ClientNetworkVariable.Value);
            }
        }
        else if (IsClient) {
            ClientNetworkVariable.Value = ClientNetworkVariable.Value + 0.1f;
            if (timeNow - m_lastLogTime > 0.5f) {
                m_lastLogTime = timeNow;
                Debug.Log("Client set its var to: " + ClientNetworkVariable.Value + ", has server var at: " +
                    ServerNetworkVariable.Value);
            }
        }
    }

//----------------------------------------------------------------------------------------------------------------------
    
    private NetworkVariable<float> ServerNetworkVariable = new NetworkVariable<float>();
    private NetworkVariable<float> ClientNetworkVariable = new NetworkVariable<float>();
    private float                  m_lastLogTime       = 0.0f;
    
}