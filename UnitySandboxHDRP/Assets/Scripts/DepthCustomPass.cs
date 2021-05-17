using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering;

class DepthCustomPass : CustomPass
{
    // It can be used to configure render targets and their clear state. Also to create temporary render target textures.
    // When empty this render pass will render to the active camera render target.
    // You should never call CommandBuffer.SetRenderTarget. Instead call <c>ConfigureTarget</c> and <c>ConfigureClear</c>.
    // The render pipeline will ensure target setup and clearing happens in an performance manner.
    protected override void Setup(ScriptableRenderContext renderContext, CommandBuffer cmd) {
        if (null == m_targetDepth)
            return;
        
        rtid = new RenderTargetIdentifier(m_targetDepth);
        
        CoreUtils.SetRenderTarget(cmd, rtid, ClearFlag.All);
        // Setup code here
    }

    protected override void Execute(CustomPassContext ctx)
    {
        if (null == m_targetDepth)
            return;
        
        Graphics.Blit(ctx.cameraDepthBuffer.rt,m_targetDepth);
        
//        CoreUtils.DrawFullScreen(ctx.cmd, fullscreenPassMaterial, shaderPassId: fullscreenPassMaterial.FindPass(materialPassName));

        CoreUtils.DrawFullScreen(ctx.cmd, fullscreenPassMaterial, rtid, shaderPassId: fullscreenPassMaterial.FindPass(materialPassName));
        
    }

    protected override void Cleanup()
    {
        // Cleanup code
    }

    [SerializeField] private RenderTexture m_targetDepth;
    
    public Material fullscreenPassMaterial;
    public string   materialPassName = "Custom Pass 0";

    private RenderTargetIdentifier rtid;


}