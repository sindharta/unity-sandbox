using System;
using System.Diagnostics;
using UnityEngine;

[DebuggerTypeProxy(typeof(SandboxVector3DebugView))]
public struct SandboxVector3 {

    [Serializable]
    internal class SandboxVector3DebugView {
        public SandboxVector3DebugView(SandboxVector3 myVec) {
            this.m_myVec = myVec;
        }

        public float X => m_myVec.X;
        public float Y => m_myVec.Y;

        [SerializeField] internal SandboxVector3 m_myVec;
    }    
//--------------------------------------------------------------------------------------------------------------------------------------------------------------    
    
    public SandboxVector3(float x, float y, float z) {
        this.X = x;
        this.Y = y;
        this.Z = z;
    }

//--------------------------------------------------------------------------------------------------------------------------------------------------------------    
    
    public float X;
    public float Y;
    public float Z;
} 
