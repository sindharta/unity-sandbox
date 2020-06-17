using UnityEngine;

public class DerivativeSpring : MonoBehaviour {
    void Update() {
        //d(sin(x)) = cos(x) * dt
        Vector3 pos = transform.position;
        pos.x += Mathf.Cos(Time.time) * m_waveSize * Time.deltaTime;
        transform.position = pos;

    }

//----------------------------------------------------------------------------------------------------------------------    
    [SerializeField] private float m_waveSize = 10;

}
