using System;
using System.Diagnostics;
using UnityEngine;

[DebuggerDisplay("({X},{Y})")]
public struct SandboxVector2 {
    
    public SandboxVector2(float x, float y) {
        this.X = x;
        this.Y = y;
    }
//--------------------------------------------------------------------------------------------------------------------------------------------------------------    
    
    public float X;
    public float Y;
} 
