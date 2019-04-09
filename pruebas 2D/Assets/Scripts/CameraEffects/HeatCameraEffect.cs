using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class HeatCameraEffect : MonoBehaviour
{
    public Material effectMaterial;

    [Range(.0f,.03f)]public float Magnitude = .1f;
    [Range(0, 1)] public float Velocity = .5f;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        effectMaterial.SetFloat("_Magnitude", Magnitude);
        effectMaterial.SetFloat("_Velocity", Velocity);

        Graphics.Blit(source, destination, effectMaterial);
    }
}
