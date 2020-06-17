using UnityEngine;

public class TrigonometrySpring : MonoBehaviour {
    void Update()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Sin(Time.time) * m_waveSize;
        transform.position = pos;

    }

//----------------------------------------------------------------------------------------------------------------------    
    [SerializeField] private float m_waveSize = 10;

}
