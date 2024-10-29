using FMOD.Studio;
using FMODUnity;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
   public static AudioManager instance { get; private set; }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

     
    }
    private void Start()
    {
        
    }
   

    public void PlayOneShot(EventReference sound, Vector2 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);

    }
    private PARAMETER_ID GetID(EventInstance instance, string parameterName)
    {
        EventDescription eventDescription;
        instance.getDescription(out eventDescription);
        PARAMETER_DESCRIPTION parameterDescription;
        eventDescription.getParameterDescriptionByName(parameterName, out parameterDescription);
        return parameterDescription.id;

    }
    public void SetParameterByLabel(EventInstance instance, string parameterName, string value)
    {
        // Obtiene el PARAMETER_ID del parámetro (asumiendo que tienes la función GetID)
        PARAMETER_ID parameterID = GetID(instance, parameterName);

        // Llama a setParameterByIDWithLabel para ajustar el valor usando la etiqueta
        instance.setParameterByIDWithLabel(parameterID, value);



    }
    public StudioEventEmitter InitializeEventEmitter(Dictionary<Sounds, EventReference> dictionary, Sounds soundKey, GameObject emitterGO)
    {
        if (dictionary.ContainsKey(soundKey))
        {
            StudioEventEmitter emitter = emitterGO.GetComponent<StudioEventEmitter>();
            emitter.EventReference = dictionary[soundKey];
            return emitter;

        }
        return null;
    }
            
    public EventInstance GetEventInstance(Dictionary<Sounds, EventReference> dictionary,Sounds soundKey)
    {
        // Inicializa la instancia como un valor por defecto
        EventInstance instance = default;
        if (dictionary.ContainsKey(soundKey))
        {
            instance = RuntimeManager.CreateInstance(dictionary[soundKey]);
        }
        else
        {
            Debug.LogWarning($"Sound key '{soundKey}' not found in typeSound dictionary.");
        }

        return instance; // Devolver la instancia (puede ser nula si no se encontró la clave)

    }

    


    public void UpdateSound(Dictionary<Sounds, EventReference> typeSound,Sounds soundKey)
    {
        if (typeSound.ContainsKey(soundKey))
        {
            EventInstance instance = RuntimeManager.CreateInstance(typeSound[soundKey]);
            instance.getPlaybackState(out PLAYBACK_STATE playbackState);
            if (playbackState == PLAYBACK_STATE.STOPPED)
            {
                instance.start();
            }
            else
            {
                Debug.LogWarning($"No sound instance found for key: {soundKey}");
            }
        }
 
    }
}
