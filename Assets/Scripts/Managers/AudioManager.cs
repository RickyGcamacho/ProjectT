using FMOD.Studio;
using FMODUnity;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    public Dictionary<Sounds, EventInstance> SoundsUI { get; private set; }
    public Dictionary<Sounds, EventInstance> SoundsEnemy { get; private set; }
    public Dictionary<Sounds, EventInstance> SoundsPlayer { get; private set; }

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
        SoundsUI = InitialInstances(FMODEvents.instance.EventReferencesUI);
        SoundsPlayer = InitialInstances(FMODEvents.instance.EventReferencesPlayer);
        SoundsEnemy = InitialInstances(FMODEvents.instance.EventReferencesEnemy);
    }
    public Dictionary<Sounds, EventInstance> InitialInstances(Dictionary<Sounds, EventReference> dicReferences)
    {
        // Crear un nuevo diccionario para almacenar las instancias de eventos
        Dictionary<Sounds, EventInstance> soundsInstances = new Dictionary<Sounds, EventInstance>();

        foreach (var reference in dicReferences)
        {
            // Crear la instancia del evento
            EventInstance instance = RuntimeManager.CreateInstance(reference.Value);

            // Almacenar en el diccionario
            soundsInstances[reference.Key] = instance;
        }

        // Retornar el diccionario de instancias
        return soundsInstances;
    }


    public void PlayOneShot(EventReference sound, Vector2 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }

    public void PlaySoundSFX(Dictionary<Sounds, EventInstance> typeSound, Sounds soundKey)
    {
        if (typeSound.ContainsKey(soundKey))
        {
            typeSound[soundKey].start();
        }
    }

    public void StopSoundSFX(Dictionary<Sounds, EventInstance> typeSound,Sounds soundKey, FMOD.Studio.STOP_MODE mode)
    {
        if (typeSound.ContainsKey(soundKey))
        {
            typeSound[soundKey].stop(mode);
        }
    }


    public void UpdateSound(Dictionary<Sounds, EventInstance> typeSound,Sounds soundKey)
    {
        if (typeSound.TryGetValue(soundKey, out EventInstance soundInstance))
        {
            soundInstance.getPlaybackState(out PLAYBACK_STATE playbackState);
            if (playbackState == PLAYBACK_STATE.STOPPED)
            {
                PlaySoundSFX(typeSound,soundKey);
            }
        }
        else
        {
            Debug.LogWarning($"No sound instance found for key: {soundKey}");
        }
    }
}
