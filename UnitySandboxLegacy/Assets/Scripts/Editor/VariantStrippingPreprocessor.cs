using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEditor.Build;
using UnityEditor.Rendering;

class VariantStrippingPreprocessor : IPreprocessShaders  {
    public int callbackOrder { get { return 0; } }

    public void OnProcessShader(Shader shader, ShaderSnippetData snippet, IList<ShaderCompilerData> data) {
        
        Debug.Log($"Shader {shader.name}, Type: {snippet.shaderType}, numVariants: {data.Count}");
        
        for (int i = 0; i < data.Count; ++i) {
            if (!data[i].shaderKeywordSet.IsEnabled(m_KeywordToStrip) || EditorUserBuildSettings.development) 
                continue;
            string foundKeywordSet = string.Join(" ", data[i].shaderKeywordSet.GetShaderKeywords()); 
            Debug.Log("Found keyword DEBUG in variant " + i + " of shader " + shader);
            Debug.Log("Keyword set: " + foundKeywordSet);
            data.RemoveAt(i);
            --i;
        }
    }
    
//--------------------------------------------------------------------------------------------------------------------------------------------------------------    
    ShaderKeyword m_KeywordToStrip = new("DEBUG");
    
}