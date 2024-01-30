using System;
using UnityEditor;
using UnityEngine;
using System.IO;
using Editor.CodeGenerator;
using Unity.Collections;
using UnityEngine.Assertions;

public static class Menu {

    [MenuItem("Debug/Create SRGB RenderTexture")]
    static void CreateSRGBRenderTexture() {             
        RenderTexture colorMap = new RenderTexture(256, 256, 24, RenderTextureFormat.ARGB32, RenderTextureReadWrite.sRGB);
        colorMap.Create();
        string path = "Assets/SRGB_RenderTexture.rendertexture";
        AssetDatabase.CreateAsset(colorMap, path);
        Debug.Log("SRGB RenderTexture created in " + path);
    }    
    
//----------------------------------------------------------------------------------------------------------------------    
    
    [MenuItem("Debug/Create Gray Texture")]
    static void CreateGrayTexture() {

        const int WIDTH   = 4;
        const int HEIGHT  = 4;
        Texture2D tex = new Texture2D(WIDTH, HEIGHT, TextureFormat.RGBA32 , mipChain: false, linear: true);
        
        NativeArray<Color32> texData = tex.GetRawTextureData<Color32>();

        int index = 0;
        for (int j = 0; j < HEIGHT; ++j) {
            for (int i = 0; i < WIDTH; ++i) {
                float grayscale = 0.5f;
                Color col       = new Color(grayscale, grayscale, grayscale, 1);                
                texData[index++] = col; 
            }
        }
        
        tex.Apply();
        
        byte[] encodedData = null;
        encodedData = tex.EncodeToPNG();
        string path = "Assets/Textures/Gray.png";
        File.WriteAllBytes("Assets/Textures/Gray.png", encodedData);
        Debug.Log("Gray texture created in " + path);
        
        //Cleanup
        UnityEngine.Object.DestroyImmediate(tex);
        AssetDatabase.Refresh();
    }

//--------------------------------------------------------------------------------------------------------------------------------------------------------------
    
    [MenuItem("Debug/Encode Gray Scene Output RT")]
    static void EncodeGraySceneOutputRT() {

        string root = "Assets/Scenes/GrayScene/";

        string[] rtFileNames = new string[] {
            "GraySceneOutputLinearRT",
            "GraySceneOutputSrgbRT",

        };
        
        foreach (string fileName in rtFileNames){
            string rtFullPath = Path.Combine(root, fileName + ".renderTexture");
            string outputPath = fileName + ".png";
            CaptureToFile(rtFullPath, outputPath, TextureFormat.RGBA32);
            Debug.Log("RT encoded to " + outputPath);
        }
    }
    
    static void CaptureToFile(string inputRenderTexturePath, string outputFilePath, TextureFormat textureFormat) {

        RenderTexture rt = AssetDatabase.LoadAssetAtPath<RenderTexture>(inputRenderTexturePath);
        Assert.IsNotNull(rt);
        
        RenderTexture prevRenderTexture = RenderTexture.active;
        RenderTexture.active = rt;
                   
        Texture2D tempTex = new Texture2D(rt.width, rt.height, textureFormat,mipChain: false);
        tempTex.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0, false);
        tempTex.Apply();

        try {
            byte[] encodedData = tempTex.EncodeToPNG();             
            File.WriteAllBytes(outputFilePath, encodedData);
        } catch (Exception e) {
            Debug.LogError($"Can't write to file: {outputFilePath}." + Environment.NewLine 
                + $"Error: {e.ToString()}"); 
        }
       
        //Cleanup
        UnityEngine.Object.DestroyImmediate(tempTex);
        RenderTexture.active = prevRenderTexture;        
    }
    
//--------------------------------------------------------------------------------------------------------------------------------------------------------------
    
    [MenuItem("Code Generation/Generate Tags")]
    static void GenerateTags() {
        Generate("Tags", UnityEditorInternal.InternalEditorUtility.tags);
    }

    [MenuItem("Code Generation/Generate Layers")]
    static void GenerateLayers() {
        Generate("Layers", UnityEditorInternal.InternalEditorUtility.layers);
    }

    private static void Generate(string name, string[] data) {
        // Build the generator with the class name and data source.
        StringItemsGenerator generator = new StringItemsGenerator(name, data);

        // Generate output (class definition).
        string classDefinition = generator.TransformText();
        string outputPath      = Path.Combine(Application.dataPath, name + ".cs");

        try {
            File.WriteAllText(outputPath, classDefinition); // Save new class to assets folder.
            AssetDatabase.Refresh();                        // Refresh assets.
        }
        catch (Exception e) {
            Debug.Log("An error occurred while saving file: " + e);
        }
    }
    


}