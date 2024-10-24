using FMOD.Studio;
using FMODUnity;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public float test=20;
    public float test2=20;
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
    public void SetAttributes(Dictionary<Sounds, EventInstance> typeSound)
    {
        foreach (var item in typeSound)
        {
            FMOD.ATTRIBUTES_3D attributes = new FMOD.ATTRIBUTES_3D();

            // Define la posición correcta utilizando transform.position para x, y, z
            attributes.position = new FMOD.VECTOR
            {
                x = transform.position.x,
                y = transform.position.y,
                z = transform.position.z
            };

            // Aplica los atributos 3D al sonido
            item.Value.set3DAttributes(attributes);
        }
    }

    public void PlayOneShot(EventReference sound, Vector2 worldPos)
    {
     
        RuntimeManager.PlayOneShot(sound, worldPos);
    }

    public void PlaySoundSFX(Dictionary<Sounds, EventReference> typeSound, Sounds soundKey)
    {     
        if (typeSound.ContainsKey(soundKey))
        {
            EventInstance instance = RuntimeManager.CreateInstance(typeSound[soundKey]);
            var feet3DPosition = RuntimeUtils.To3DAttributes(gameObject.transform.position);
            instance.set3DAttributes(feet3DPosition);

            // Obtener la descripción del evento
            FMOD.Studio.EventDescription eventDescription;
            instance.getDescription(out eventDescription);

            // Obtener la descripción y el ID del parámetro 'generator_condition'
            FMOD.Studio.PARAMETER_DESCRIPTION parameterDescription;
            FMOD.Studio.PARAMETER_DESCRIPTION parameterDescription2;
            eventDescription.getParameterDescriptionByName("generator_condition", out parameterDescription);
            eventDescription.getParameterDescriptionByName("Distance", out parameterDescription2);
            FMOD.Studio.PARAMETER_ID parameterID = parameterDescription.id;
            FMOD.Studio.PARAMETER_ID parameterID2 = parameterDescription2.id;

            // Cambiar el valor del parámetro a 'start'
            instance.setParameterByID(parameterID, test);  // 1.0f para encender
            instance.setParameterByID(parameterID, test2);  // 1.0f para encender


            instance.start();
  
            //typeSound[soundKey].setParameterByName("Distance", test);
            //typeSound[soundKey].start();
        }
    }

    public void StopSoundSFX(Dictionary<Sounds, EventReference> typeSound,Sounds soundKey, FMOD.Studio.STOP_MODE mode)
    {
        if (typeSound.ContainsKey(soundKey))
        {
            EventInstance instance = RuntimeManager.CreateInstance(typeSound[soundKey]);
            instance.start();
        }
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
