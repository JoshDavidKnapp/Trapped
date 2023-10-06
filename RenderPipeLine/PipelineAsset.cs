using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(menuName = "Rendering/ExampleRenderPipelineAsset")]

public class PipelineAsset : RenderPipelineAsset
{

    public Color color;
    public string String;


    protected override RenderPipeline CreatePipeline()
    {
        return new PipelineInstance(this);

        
    }
   
}
