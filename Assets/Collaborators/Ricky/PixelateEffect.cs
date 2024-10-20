using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PixelateEffect : MonoBehaviour
{
    public Material pixelateMaterial;

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (pixelateMaterial != null)
        {
            Graphics.Blit(src, dest, pixelateMaterial);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }
}