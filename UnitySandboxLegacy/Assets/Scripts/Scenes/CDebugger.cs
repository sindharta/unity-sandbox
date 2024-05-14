using UnityEngine;

public class CDebugger : MonoBehaviour
{
    void Update() {
        SandboxVector2 myVec = new SandboxVector2(Random.Range(0.0f,10.0f), Random.Range(0.0f, 10.0f));
        Debug.Log(myVec);
    }
}
