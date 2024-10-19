using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Necesario para los eventos de UI

public class ButtonSound : MonoBehaviour, IPointerEnterHandler
{
    public AudioSource audioSource;  // Fuente de audio que reproducirá el sonido
    public AudioClip hoverClip;      // Clip de sonido cuando el mouse pasa sobre el botón

    // Método que se llama automáticamente cuando el mouse entra en el botón
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Reproduce el sonido cuando el mouse pasa sobre el botón
        audioSource.PlayOneShot(hoverClip);
    }
}