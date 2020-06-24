using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public sealed class GrayscaleRenderer : PostProcessEffectRenderer<Grayscale> {
    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("Hidden/Custom/Grayscale"));
        sheet.properties.SetFloat("_Blend", settings.blend);
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}