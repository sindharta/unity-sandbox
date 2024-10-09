using UnityEngine;

public class HelloFromSourceGenerator : MonoBehaviour {
    static string GetStringFromSourceGenerator() {
        return SandboxSrcGen.SandboxSrcGenerated.GetTestText();
    }
    void Start() {
        string output = "Test";
        output = GetStringFromSourceGenerator();
        Debug.Log(output);
    }
}


