using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode] //this allows us to easily debug the shader

public class ShaderHandler : MonoBehaviour
{
    public Material effectMaterial;


    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, effectMaterial);
    }

}
