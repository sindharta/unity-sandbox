using MLAPI;
using MLAPI.Messaging;
using UnityEngine;

public class RPCTest : NetworkBehaviour {

    [ClientRpc]
    void TestClientRpc(int value) {
        if (IsClient) {
            Debug.Log("Client Received the RPC #" + value);
            TestServerRpc(value + 1);
        }
    }

    [ServerRpc]
    void TestServerRpc(int value) {
        if (IsServer) {
            Debug.Log("Server Received the RPC #" + value);
            TestClientRpc(value);
        }
    }

    // Update is called once per frame
    void Update() {
        if (IsClient && m_firstTime) {
            m_firstTime = false;
            TestServerRpc(0);
        }
    }

//----------------------------------------------------------------------------------------------------------------------
    
    private bool m_firstTime = true;
    
}