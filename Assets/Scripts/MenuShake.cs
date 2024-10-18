using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuShake : MonoBehaviour
{
    [SerializeField] private float moveDistance = 6f; // Distancia que la cámara se moverá hacia adelante y hacia atrás
    [SerializeField] private float moveSpeed = 2f;    // Velocidad de movimiento de la cámara
    private Vector3 initialPosition; // Posición inicial de la cámara

    void Start()
    {
        // Guardar la posición inicial de la cámara al empezar
        initialPosition = transform.position;
    }

    void Update()
    {
        // Crear un movimiento oscilante hacia adelante y hacia atrás usando una función seno
        float offset = Mathf.Sin(Time.time * moveSpeed) * moveDistance;

        // Actualizar la posición de la cámara
        transform.position = initialPosition + transform.forward * offset;
    }
}
