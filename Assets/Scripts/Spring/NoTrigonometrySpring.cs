using UnityEngine;

public class NoTrigonometrySpring : MonoBehaviour {

    void Awake() {
        m_transform = transform;
    }

    void Start() {

        m_velocity = new Vector3(m_waveSize, 0f, 0f);
    }
    
//----------------------------------------------------------------------------------------------------------------------    
    
    
    void Update() {
        Vector3 pos = transform.position;

        //Velocity: double derivatives
        //d(sin(x)) = cos(x) * dt
        //d(cos(x)) = -sin(x) * dt
        
        //Then, from TrigonometrySpring: pos.x = Mathf.Sin(Time.time) * m_waveSize;        
        m_velocity.x += -pos.x * Time.deltaTime;

        pos.x += m_velocity.x * Time.deltaTime;
        
        m_transform.position = pos;

    }

//----------------------------------------------------------------------------------------------------------------------    
    [SerializeField] private float m_waveSize = 10;

    private Vector3 m_velocity;
    private Transform m_transform = null;

}
