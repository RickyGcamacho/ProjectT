using FMOD.Studio;
using System.Collections;
using UnityEngine;

public class GeneratorController : MonoBehaviour
{
    [field: SerializeField] private Sounds prop_generator;
    private EventInstance _eventGenerator;
    private bool isRunning = false;
    public float fuelLevel = 100f; // Ejemplo de nivel de combustible
    private bool canFuel ; // Ejemplo de nivel de combustible
    private bool canInteract ; // Ejemplo de nivel de combustible

    private void Start()
    {
        canFuel = true;
        _eventGenerator = AudioManager.instance.GetEventInstance(FMODEvents.instance.References_Props, prop_generator);
        _eventGenerator.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform)); // Establecer atributos 3D

        StartCoroutine(StartGenerator());
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && canInteract) 
        {
            HandleGeneratorInput();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            canFuel = true;
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            canFuel = false;
        }
    }

    private void HandleGeneratorInput()
    {
        // Encender el generador
        if (!isRunning)
        {
            if (canFuel)
            {
                Refuel(10);
                isRunning = true;

                StartCoroutine(StartGenerator());
            }
            else
            {
                AudioManager.instance.SetParameterByLabel(_eventGenerator, "generator_condition", "notstart");
                _eventGenerator.start();
            }
           
        }
        else
        {
            if (canFuel)
            {
                Refuel(10);
            }
           
        }

    }


    private void StopGenerator()
    {
        if (isRunning)
        {
            isRunning = false; // Cambiar el estado
            AudioManager.instance.SetParameterByLabel(_eventGenerator, "generator_condition", "stop");
            StopCoroutine(StartGenerator());
        }
    }

    private IEnumerator StartGenerator()
    {
        isRunning = true;
        AudioManager.instance.SetParameterByLabel(_eventGenerator, "generator_condition", "start");
        _eventGenerator.start(); // Iniciar el sonido si es necesario

        while (fuelLevel >= 1)
        {
            yield return new WaitForSeconds(1f); // Espera 1 segundo antes de reducir el combustible
            fuelLevel -= 1f; // Reducir el combustible por la cantidad deseada
          
            if (fuelLevel <= 0)
            {
                fuelLevel = 0; // Asegurar que no se pase a un valor negativo
                StopGenerator(); // Detener el generador si el combustible se agota
            }
        }
    }
    private void Refuel(float amount)
    {
        canFuel = false;
        fuelLevel += amount;
        if (fuelLevel > 100f) fuelLevel = 100f; // Limitar el combustible a un máximo
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            canInteract = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            canInteract = false;
        }
    }
}
