using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParpadeoTerrorifico : MonoBehaviour
{
    public Light dirLight;           // La luz que va a parpadear
    public Material xenonMaterial;   // El material que usa el shader XenonLightShader
    public float minIntensity = 0.1f; // Intensidad mínima de la luz
    public float maxIntensity = 1f;   // Intensidad máxima de la luz
    public float minFlickerTime = 0.05f;  // Tiempo mínimo entre parpadeos
    public float maxFlickerTime = 0.3f;   // Tiempo máximo entre parpadeos
    public float flickerDuration = 0.1f;  // Duración del parpadeo

    private float nextFlickerTime;    // Momento para el próximo parpadeo
    private bool isFlickering = false; // Si la luz está parpadeando actualmente
    private float flickerEndTime;     // Cuándo finalizar el parpadeo actual

    void Start()
    {
        if (dirLight == null)
        {
            dirLight = GetComponent<Light>(); // Asignar la luz automáticamente si no está asignada
        }

        if (xenonMaterial == null)
        {
            Debug.LogError("No se ha asignado el material del shader Xenon.");
        }

        ScheduleNextFlicker(); // Planificar el primer parpadeo
    }

    void Update()
    {
        // Si es hora de empezar un nuevo parpadeo
        if (Time.time >= nextFlickerTime && !isFlickering)
        {
            StartFlicker();
        }

        // Si la luz está en modo parpadeo, restablecer la intensidad cuando acabe
        if (isFlickering && Time.time >= flickerEndTime)
        {
            EndFlicker();
        }
    }

    void ScheduleNextFlicker()
    {
        // Definir el tiempo para el próximo parpadeo en un intervalo aleatorio
        nextFlickerTime = Time.time + Random.Range(minFlickerTime, maxFlickerTime);
    }

    void StartFlicker()
    {
        // Parpadeo: cambia la intensidad de la luz y también la del shader
        float intensity = Random.Range(minIntensity, maxIntensity);

        // Aplicar la intensidad a la luz
        dirLight.intensity = intensity;

        // Aplicar la intensidad al shader
        xenonMaterial.SetFloat("_ExternalIntensity", intensity);

        isFlickering = true;
        flickerEndTime = Time.time + flickerDuration;
    }

    void EndFlicker()
    {
        // Restaurar la intensidad original de la luz y el shader
        dirLight.intensity = maxIntensity;
        xenonMaterial.SetFloat("_ExternalIntensity", maxIntensity);

        isFlickering = false;
        ScheduleNextFlicker();
    }
}