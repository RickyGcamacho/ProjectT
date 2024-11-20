using UnityEngine;

[ExecuteInEditMode]
public class ScreenEffect : MonoBehaviour
{
    public Material effectMaterial;

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (effectMaterial != null)
        {
            // Aplica el material en la imagen de pantalla
            Graphics.Blit(src, dest, effectMaterial);
        }
        else
        {
            // Si no hay material, renderiza normalmente
            Graphics.Blit(src, dest);
        }
    }
}
