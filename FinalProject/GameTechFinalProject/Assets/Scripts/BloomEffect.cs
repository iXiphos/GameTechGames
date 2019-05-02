using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[ExecuteInEditMode, ImageEffectAllowedInSceneView]
public class BloomEffect : MonoBehaviour
{
    //Trying to get a bloom effect to happen but it is not
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination);
    }
}
