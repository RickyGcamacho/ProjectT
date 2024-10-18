using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SineZoomImage : MonoBehaviour
{
    public RectTransform imageRectTransform; // Asigna el RectTransform de la imagen en el Inspector
    public float zoomSpeed = 1f; // Velocidad del zoom
    public float zoomAmount = 1.5f; // Factor de zoom (1.0 es sin zoom)
    private Vector3 originalScale;

    void Start()
    {
        // Guardamos la escala original
        originalScale = imageRectTransform.localScale;
    }

    void Update()
    {
        // Calcular el tiempo basado en el tiempo de juego
        float time = Time.time * zoomSpeed;

        // Usar una función seno para calcular el factor de escala
        float scaleFactor = 1 + Mathf.Sin(time) * (zoomAmount - 1) / 2;

        // Aplicar la nueva escala
        imageRectTransform.localScale = new Vector3(scaleFactor, scaleFactor, 1);
    }
}