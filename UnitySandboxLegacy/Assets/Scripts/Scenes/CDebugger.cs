using UnityEngine;
using Random = UnityEngine.Random;

public class CDebugger : MonoBehaviour
{
    private void Start() {
        m_user = new CDebuggerUser {
            Login = "JohnDoe",
            FullName = new CDebuggerFullName("John", "Doe"),
            HashedPassword = "123"
        };
    }

    void Update() {
        //Debug the following variables in your C# editor
        CDebuggerUser currentUser = m_user;
        SandboxVector3 myVec3 = new SandboxVector3(Random.Range(0.0f,10.0f), Random.Range(0.0f, 10.0f), Random.Range(0.0f, 10.0f));

    }
    
    
    CDebuggerUser m_user;
}
