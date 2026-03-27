using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public static class SpriteToPngExporter
{
    [MenuItem("Assets/Export Selected Sprites To PNG", true)]
    private static bool ValidateExportSelectedSpritesToPng()
    {
        Object[] selected = Selection.objects;
        if (selected == null || selected.Length == 0)
            return false;

        foreach (Object obj in selected)
        {
            if (!(obj is Sprite))
                return false;
        }

        return true;
    }

    [MenuItem("Assets/Export Selected Sprites To PNG")]
    private static void ExportSelectedSpritesToPng()
    {
        Object[] selectedObjects = Selection.objects;
        List<Sprite> sprites = new List<Sprite>();

        foreach (Object obj in selectedObjects)
        {
            if (obj is Sprite sprite)
                sprites.Add(sprite);
        }

        if (sprites.Count == 0)
        {
            EditorUtility.DisplayDialog("Export Sprites", "No sprites selected.", "OK");
            return;
        }

        string outputFolder = EditorUtility.OpenFolderPanel(
            "Choose Output Folder",
            Application.dataPath,
            ""
        );

        if (string.IsNullOrEmpty(outputFolder))
            return;

        int successCount = 0;
        List<string> failedSprites = new List<string>();

        foreach (Sprite sprite in sprites)
        {
            bool success = ExportSingleSprite(sprite, outputFolder);
            if (success)
                successCount++;
            else
                failedSprites.Add(sprite.name);
        }

        string message = $"Exported {successCount}/{sprites.Count} sprite(s) to:\n{outputFolder}";

        if (failedSprites.Count > 0)
        {
            message += "\n\nFailed:";
            foreach (string failed in failedSprites)
                message += $"\n- {failed}";
        }

        EditorUtility.DisplayDialog("Export Complete", message, "OK");
        AssetDatabase.Refresh();
    }

    private static bool ExportSingleSprite(Sprite sprite, string outputFolder)
    {
        if (sprite == null || sprite.texture == null)
            return false;

        string texturePath = AssetDatabase.GetAssetPath(sprite.texture);
        TextureImporter importer = AssetImporter.GetAtPath(texturePath) as TextureImporter;

        if (importer == null)
            return false;

        bool originalReadable = importer.isReadable;
        bool changedReadable = false;

        try
        {
            if (!importer.isReadable)
            {
                importer.isReadable = true;
                importer.SaveAndReimport();
                changedReadable = true;
            }

            Texture2D sourceTexture = AssetDatabase.LoadAssetAtPath<Texture2D>(texturePath);
            if (sourceTexture == null)
                return false;

            Rect rect = sprite.rect;
            int width = Mathf.RoundToInt(rect.width);
            int height = Mathf.RoundToInt(rect.height);

            Texture2D outputTexture = new Texture2D(width, height, TextureFormat.RGBA32, false);

            Color[] pixels = sourceTexture.GetPixels(
                Mathf.RoundToInt(rect.x),
                Mathf.RoundToInt(rect.y),
                width,
                height
            );

            outputTexture.SetPixels(pixels);
            outputTexture.Apply();

            byte[] pngData = outputTexture.EncodeToPNG();
            Object.DestroyImmediate(outputTexture);

            if (pngData == null || pngData.Length == 0)
                return false;

            string safeFileName = MakeSafeFileName(sprite.name) + ".png";
            string outputPath = Path.Combine(outputFolder, safeFileName);

            outputPath = GetUniquePath(outputPath);

            File.WriteAllBytes(outputPath, pngData);
            return true;
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Failed to export sprite '{sprite.name}': {ex}");
            return false;
        }
        finally
        {
            if (changedReadable && importer != null)
            {
                importer.isReadable = originalReadable;
                importer.SaveAndReimport();
            }
        }
    }

    private static string MakeSafeFileName(string fileName)
    {
        foreach (char c in Path.GetInvalidFileNameChars())
            fileName = fileName.Replace(c, '_');

        return fileName;
    }

    private static string GetUniquePath(string path)
    {
        if (!File.Exists(path))
            return path;

        string directory = Path.GetDirectoryName(path);
        string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path);
        string extension = Path.GetExtension(path);

        int index = 1;
        string newPath;

        do
        {
            newPath = Path.Combine(directory, $"{fileNameWithoutExtension}_{index}{extension}");
            index++;
        }
        while (File.Exists(newPath));

        return newPath;
    }
}
