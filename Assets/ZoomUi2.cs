using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomImage : MonoBehaviour
{
    public RectTransform imageRectTransform; // Asigna el RectTransform de la imagen en el Inspector
    public float zoomSpeed = 1f; // Velocidad del zoom
    public float zoomAmount = 1.5f; // Factor de zoom (1.0 es sin zoom)

    private Vector3 originalScale;
    private bool isZoomingIn = true;

    void Start()
    {
        // Guardamos la escala original
        originalScale = imageRectTransform.localScale;
    }

    void Update()
    {
        // Calcular la escala deseada
        float scale = isZoomingIn ? Mathf.Lerp(originalScale.x, originalScale.x * zoomAmount, Mathf.PingPong(Time.time * zoomSpeed, 1))
                                   : Mathf.Lerp(originalScale.x * zoomAmount, originalScale.x, Mathf.PingPong(Time.time * zoomSpeed, 1));

        // Aplicar la nueva escala
        imageRectTransform.localScale = new Vector3(scale, scale, 1);

        // Cambiar dirección de zoom si alcanzamos el límite
        if (Mathf.Approximately(scale, originalScale.x * zoomAmount) || Mathf.Approximately(scale, originalScale.x))
        {
            isZoomingIn = !isZoomingIn;
        }
    }
}
