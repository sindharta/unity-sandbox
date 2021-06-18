using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable;
using UnityEngine;

namespace HelloWorld {
public class HelloWorldPlayer : NetworkBehaviour {
    public NetworkVariableVector3 Position = new NetworkVariableVector3(new NetworkVariableSettings {
        WritePermission = NetworkVariablePermission.ServerOnly,
        ReadPermission  = NetworkVariablePermission.Everyone
    });

//----------------------------------------------------------------------------------------------------------------------    
    
    public override void NetworkStart() {
        RequestMove();
    }
//----------------------------------------------------------------------------------------------------------------------    

    public void RequestMove() {
        if (NetworkManager.Singleton.IsServer) {
            Move();
        }
        else {
            SubmitPositionRequestServerRpc();
        }
    }

    [ServerRpc]
    void SubmitPositionRequestServerRpc(ServerRpcParams rpcParams = default) {
        Move();
    }

    void Move() {
        Position.Value = GetRandomPositionOnPlane();
        
    }

    static Vector3 GetRandomPositionOnPlane() {
        return new Vector3(Random.Range(-3f, 3f), 1f, Random.Range(-3f, 3f));
    }

//----------------------------------------------------------------------------------------------------------------------    
    void Update() {
        transform.position = Position.Value;
    }
}

}