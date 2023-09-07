using Unity.Netcode;
using UnityEngine;
using UnityEngine.Serialization;

namespace HelloWorld {
public class HelloWorldPlayer : NetworkBehaviour {

    public override void OnNetworkSpawn() {
        if (IsOwner) {
            NetworkMove();
        }
    }

    public void NetworkMove() {
        if (NetworkManager.Singleton.IsServer) {
            Vector3 randomPosition = GetRandomPositionOnPlane();
            transform.position = randomPosition;
            m_networkPos.Value = randomPosition;
        }
        else {
            SubmitPositionRequestServerRpc();
        }
    }

    [ServerRpc]
    void SubmitPositionRequestServerRpc(ServerRpcParams rpcParams = default) {
        m_networkPos.Value = GetRandomPositionOnPlane();
    }

    static Vector3 GetRandomPositionOnPlane() {
        return new Vector3(Random.Range(-3f, 3f), 1f, Random.Range(-3f, 3f));
    }

    void Update() {
        transform.position = m_networkPos.Value;
    }

//--------------------------------------------------------------------------------------------------------------------------------------------------------------
    
    [SerializeField] private NetworkVariable<Vector3> m_networkPos = new NetworkVariable<Vector3>();
    
}
}