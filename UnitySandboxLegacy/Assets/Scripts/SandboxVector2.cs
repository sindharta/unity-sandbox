using System.Diagnostics;

[DebuggerTypeProxy(typeof(SandboxVector2DebugView))]
[DebuggerDisplay("({X},{Y})")]
public struct SandboxVector2 {

    internal class SandboxVector2DebugView {
        public SandboxVector2DebugView(SandboxVector2 myVec)
        {
            this.m_myVec = myVec;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public float X => m_myVec.X;

        private SandboxVector2 m_myVec;
        
        public const string TestString = "This should appear in the debug window.";
    }    
//--------------------------------------------------------------------------------------------------------------------------------------------------------------    
    
    public SandboxVector2(float x, float y) {
        this.X = x;
        this.Y = y;
    }
    
    private const string TestString = "This should not appear in the debug window.";    

//--------------------------------------------------------------------------------------------------------------------------------------------------------------    
    
    public float X;
    public float Y;
} 
