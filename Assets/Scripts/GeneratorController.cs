using FMOD.Studio;
using UnityEngine;

public class GeneratorController : MonoBehaviour
{
    [field: SerializeField] private Sounds env_generator;
    private EventInstance _eventGenerator;
    private bool isRunning = false;
    private float fuelLevel = 100f; // Ejemplo de nivel de combustible

    private void Update()
    {
        HandleGeneratorInput();
    }

    private void HandleGeneratorInput()
    {
        // Encender el generador
        if (fuelLevel > 0 && Input.GetKeyDown(KeyCode.T) && !isRunning)
        {
            _eventGenerator = AudioManager.instance.GetInstance(env_generator, FMODEvents.instance.References_UI);
            _eventGenerator.start(); // Iniciar el sonido
            isRunning = true; // Cambiar el estado
            AudioManager.instance.SetParameterByLabel(_eventGenerator, "generator_condition", "start");
        }

        // Apagar el generador
        if (fuelLevel<=0 && isRunning)
        {
            AudioManager.instance.SetParameterByLabel(_eventGenerator, "generator_condition", "stop");
            isRunning = false; // Cambiar el estado
        }

        // Lógica para reducir el combustible mientras está en funcionamiento
        if (isRunning)
        {
            fuelLevel -= Time.deltaTime; // Reducir el combustible con el tiempo
            if (fuelLevel <= 0)
            {
                StopGenerator(); // Detener el generador si no hay combustible
            }
        }
    }

    private void StopGenerator()
    {
        if (isRunning)
        {
            AudioManager.instance.SetParameterByLabel(_eventGenerator, "generator_condition", "stop");
            isRunning = false; // Cambiar el estado
        }
    }



    public void Refuel(float amount)
    {
        fuelLevel += amount;
        if (fuelLevel > 100f) fuelLevel = 100f; // Limitar el combustible a un máximo
    }
}
