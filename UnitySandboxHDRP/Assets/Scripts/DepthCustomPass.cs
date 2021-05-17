using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.Rendering;

class DepthCustomPass : CustomPass
{
    // It can be used to configure render targets and their clear state. Also to create temporary render target textures.
    // When empty this render pass will render to the active camera render target.
    // You should never call CommandBuffer.SetRenderTarget. Instead call <c>ConfigureTarget</c> and <c>ConfigureClear</c>.
    // The render pipeline will ensure target setup and clearing happens in an performance manner.
    protected override void Setup(ScriptableRenderContext renderContext, CommandBuffer cmd) {
        if (null == m_targetDepth)
            return;
        
        m_targetDepthTextureID = new RenderTargetIdentifier(m_targetDepth);        
        CoreUtils.SetRenderTarget(cmd, m_targetDepthTextureID, ClearFlag.Color);
    }

    protected override void Execute(CustomPassContext ctx)
    {
        if (null == m_targetDepth || null == m_fullscreenPassMaterial)
            return;
        
        CoreUtils.DrawFullScreen(ctx.cmd, m_fullscreenPassMaterial, m_targetDepthTextureID);
        
    }

    protected override void Cleanup() {
    }

//----------------------------------------------------------------------------------------------------------------------    
    [SerializeField] private Material      m_fullscreenPassMaterial;
    [SerializeField] private RenderTexture m_targetDepth;
    
//----------------------------------------------------------------------------------------------------------------------    
    

    private RenderTargetIdentifier m_targetDepthTextureID;


}