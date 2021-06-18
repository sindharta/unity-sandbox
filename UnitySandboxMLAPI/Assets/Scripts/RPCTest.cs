using MLAPI;
using MLAPI.Messaging;
using UnityEngine;

public class RpcTest : NetworkBehaviour
{
    private bool firstTime = true;

    [ClientRpc]
    void TestClientRpc(int value)
    {
        if (IsClient)
        {
            Debug.Log("Client Received the RPC #" + value);
            TestServerRpc(value + 1);
        }
    }

    [ServerRpc]
    void TestServerRpc(int value)
    {
        if (IsServer)
        {
            Debug.Log("Server Received the RPC #" + value);
            TestClientRpc(value);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (IsClient && firstTime)
        {
            firstTime = false;
            TestServerRpc(0);
        }
    }
}