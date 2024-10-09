using UnityEngine;

public class HelloFromSourceGenerator : MonoBehaviour {
    static string GetStringFromSourceGenerator() {
        return SandboxSrcGen.SandboxSrcGenerated.GetTestText();
    }
    void Start() {
        string output = GetStringFromSourceGenerator();
        Debug.Log(output);
    }
}


