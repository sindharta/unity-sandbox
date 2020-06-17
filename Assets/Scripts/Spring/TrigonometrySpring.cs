using UnityEngine;

public class TrigonometrySpring : MonoBehaviour {
    void Update()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Sin(Time.time) * m_springSize;
        transform.position = pos;

    }

//----------------------------------------------------------------------------------------------------------------------    
    [SerializeField] private float m_springSize = 10;

}
