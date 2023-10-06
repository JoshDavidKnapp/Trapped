using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PipelineInstance : RenderPipeline
{

    private PipelineAsset renderPipelineAsset;


    public PipelineInstance(PipelineAsset asset)
    {
        renderPipelineAsset = asset;

    }

    protected override void Render(ScriptableRenderContext context, Camera[] cameras)
    {
        Debug.Log(renderPipelineAsset.String);
    }
}
