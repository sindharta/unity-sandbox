using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEditor.Build;
using UnityEditor.Rendering;

class VariantStrippingPreprocessor : IPreprocessShaders  {
    // Use callbackOrder to set when Unity calls this shader preprocessor. Unity starts with the preprocessor that has the lowest callbackOrder value.
    public int callbackOrder { get { return 0; } }

    public void OnProcessShader(Shader shader, ShaderSnippetData snippet, IList<ShaderCompilerData> data) {
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