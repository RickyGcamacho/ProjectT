using UnityEngine;

[ExecuteInEditMode]
public class VHSPostProcess : MonoBehaviour
{
    public Material vhsMaterial; // Asigna tu material VHS con el shader aquí

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (vhsMaterial != null)
        {
            // Aplica el shader al renderizado de la imagen
            Graphics.Blit(source, destination, vhsMaterial);
        }
        else
        {
            // Si no hay material, renderiza la imagen sin modificaciones
            Graphics.Blit(source, destination);
        }
    }
}
