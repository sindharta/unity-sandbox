using UnityEngine;

public class SpringEnemy : MonoBehaviour
{
    public void SetNextTargetPos(Vector3 pos, float delay) {
        m_nextTargetPos = pos;
        m_targetDirty = true;
        m_targetUpdateTime = Time.time + delay;
    }

//----------------------------------------------------------------------------------------------------------------------    
    public void SetCoefficients(float springCoef, float dragCoef) {
        m_springCoef = springCoef;
        m_dragCoef = dragCoef;
    }
    
//----------------------------------------------------------------------------------------------------------------------    
    
    
    void Awake() {
        m_transform = transform;
    }

    void Start() {
        m_velocity = m_targetPos - transform.position;
        if (Vector3.SqrMagnitude(m_velocity) <= float.Epsilon) {
            m_velocity = new Vector3(0.0001f,0f,0f);
        }
            
    }
    
//----------------------------------------------------------------------------------------------------------------------    
    void Update() {
        //Update active target position
        if (m_targetDirty && Time.time >= m_targetUpdateTime) {
            m_targetPos = m_nextTargetPos;
            m_targetDirty = false;
        }
        
        Vector3 pos = transform.position;

        //Based on NoTrigonometrySpring
        m_velocity += (m_targetPos-pos) * (m_springCoef * Time.deltaTime);
        m_velocity -= (m_velocity * (Time.deltaTime * m_dragCoef)); //drag
        pos += m_velocity * Time.deltaTime;
        
        m_transform.position = pos;
        m_transform.rotation = Quaternion.LookRotation(Vector3.Normalize(m_velocity), Vector3.forward);

    }

//----------------------------------------------------------------------------------------------------------------------    

    private Vector3   m_velocity;
    private Transform m_transform = null;
    private Vector3   m_targetPos = Vector3.zero;

    private Vector3 m_nextTargetPos    = Vector3.zero;
    private float   m_targetUpdateTime = 0;
    private bool    m_targetDirty      = false;

    private float m_springCoef = 4.0f;
    private float m_dragCoef   = 1.0f;
    


}
