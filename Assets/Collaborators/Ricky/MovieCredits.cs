using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MovieCreditsImages : MonoBehaviour
{
    // Referencias a las imágenes de los créditos
    public Image[] creditImages;
    public float displayTime = 3f;   // Tiempo que se muestra cada imagen (en segundos)
    public float fadeDuration = 1f;  // Duración del fade in y fade out

    // Iniciar la secuencia de créditos
    void Start()
    {
       
    }
    public void ResetCredits()
    {
        foreach (Image credit in creditImages)
        {
            credit.gameObject.SetActive(false);  // Desactivar las imágenes
            credit.color = new Color(credit.color.r, credit.color.g, credit.color.b, 0);  // Poner la transparencia en 0
        }
    }
    public void InitialCredits()
    {
        StartCoroutine(ShowCreditsSequence());
    }
    // Coroutine para manejar la secuencia de imágenes de los créditos
    IEnumerator ShowCreditsSequence()
    {
        foreach (Image credit in creditImages)
        {
            // Aparecer la imagen con un efecto de fade-in
            yield return StartCoroutine(FadeInImage(credit));
            // Mantener la imagen visible durante un tiempo determinado
            yield return new WaitForSeconds(displayTime);
            // Desaparecer la imagen con un efecto de fade-out
            yield return StartCoroutine(FadeOutImage(credit));
        }
    }

    // Coroutine para hacer un fade-in en la imagen
    IEnumerator FadeInImage(Image image)
    {
        image.gameObject.SetActive(true);  // Activar la imagen
        for (float t = 0.01f; t < fadeDuration; t += Time.deltaTime)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.Lerp(0, 1, t / fadeDuration));
            yield return null;
        }
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1);  // Asegurarse de que esté completamente visible
    }

    // Coroutine para hacer un fade-out en la imagen
    IEnumerator FadeOutImage(Image image)
    {
        for (float t = 0.01f; t < fadeDuration; t += Time.deltaTime)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.Lerp(1, 0, t / fadeDuration));
            yield return null;
        }
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);  // Asegurarse de que esté completamente invisible
        image.gameObject.SetActive(false);  // Desactivar la imagen
    }
}
