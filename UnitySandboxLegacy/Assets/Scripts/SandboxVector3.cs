using System;
using System.Diagnostics;

[DebuggerDisplay("({X},{Y},{Z})")] //controls how the class is displayed in the debugger variable windows (one line / root)
[DebuggerTypeProxy(typeof(SandboxVector3DebugView))] //controls how the class is displayed in the debugger variable windows when expanded
public struct SandboxVector3 {

    [Serializable]
    internal class SandboxVector3DebugView {
        public SandboxVector3DebugView(SandboxVector3 myVec) {
            this.m_myVec = myVec;
        }

        public float X100 => m_myVec.X * 100;
        public float Y100 => m_myVec.Y * 100;
        public float Z100 => m_myVec.Z * 100;

        private SandboxVector3 m_myVec;
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
