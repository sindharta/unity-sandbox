using UnityEngine;

public class Vector3Test : MonoBehaviour {
    [SerializeField]
    int m_loop = 100;

    void Update() {
        Vector3 op0 = Vector3.zero;
        Vector3 op1 = Vector3.zero;
        Vector3 result = Vector3.zero;


        Profiler.BeginSample("Direct Add");
        for (int i=0; i < m_loop; ++i) {
            result = op0 + op1;
        }
        Profiler.EndSample();

        Profiler.BeginSample("Indirect Add");
        for (int i=0; i < m_loop; ++i) {
            result = new Vector3(op0.x + op1.x, op0.y + op1.y, op0.z + op1.z);
        }
        Profiler.EndSample();

        Profiler.BeginSample("Indirect Add With Func");
        for (int i=0; i < m_loop; ++i) {
            result = Add(op0, op1);
        }
        Profiler.EndSample();

    }

    Vector3 Add(Vector3 op0, Vector3 op1) {
        return new Vector3(op0.x + op1.x, op0.y + op1.y, op0.z + op1.z);
    }
}