﻿using UnityEngine;

public class DoubleDerivativeSpring : MonoBehaviour {

    void Start() {

        m_velocity = new Vector3(m_waveSize, 0f, 0f);
    }
    
//----------------------------------------------------------------------------------------------------------------------    
    
    
    void Update() {
        Vector3 pos = transform.position;

        //Velocity: double derivatives
        //d(sin(x)) = cos(x) * dt
        //d(cos(x)) = -sin(x) * dt
        m_velocity.x += -Mathf.Sin(Time.time) * m_waveSize * Time.deltaTime;
        
        pos.x += m_velocity.x * Time.deltaTime;
        transform.position = pos;

    }

//----------------------------------------------------------------------------------------------------------------------    
    [SerializeField] private float m_waveSize = 10;

    private Vector3 m_velocity;

}
