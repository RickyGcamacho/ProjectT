using FMOD.Studio;
using FMODUnity;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }
    public Dictionary<Sounds, EventInstance> Sounds { get => _sounds; set => _sounds = value; }

    private Dictionary<Sounds, EventInstance> _sounds;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

    }

    public void InitializeInstances(Dictionary<Sounds, EventReference> dicReferences)
    {
        _sounds = new Dictionary<Sounds, EventInstance>();
        foreach (var reference in dicReferences)
        {
            _sounds[reference.Key] = CreateInstance(reference.Value);
        }
    }

    public EventInstance CreateInstance(EventReference eventReference)
    {
        return RuntimeManager.CreateInstance(eventReference);
    }

    public void PlayOneShot(EventReference sound, Vector2 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }

    public void PlaySoundSFX(Sounds soundKey)
    {
        if (_sounds.ContainsKey(soundKey))
        {
            _sounds[soundKey].start();
        }
    }

    public void StopSoundSFX(Sounds soundKey, FMOD.Studio.STOP_MODE mode)
    {
        if (_sounds.ContainsKey(soundKey))
        {
            _sounds[soundKey].stop(mode);
        }
    }

    public void UpdateSound(Sounds soundKey)
    {
        if (_sounds.TryGetValue(soundKey, out EventInstance soundInstance))
        {
            soundInstance.getPlaybackState(out PLAYBACK_STATE playbackState);
            if (playbackState == PLAYBACK_STATE.STOPPED)
            {
                PlaySoundSFX(soundKey);
            }
        }
        else
        {
            Debug.LogWarning($"No sound instance found for key: {soundKey}");
        }
    }
}
