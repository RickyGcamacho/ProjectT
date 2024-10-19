using FMOD.Studio;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActorView : MonoBehaviour
{
    protected Dictionary<Sounds, FMODUnity.EventReference> _dicReferences;

    public Dictionary<Sounds, EventInstance> _sounds { get; private set; }

    protected virtual void Start()
    {
        SetReferences();
        InitializeInstances();
    }
    public virtual void SetReferences()
    {

    }
    private void InitializeInstances()
    {
        _sounds = new Dictionary<Sounds, EventInstance>();
        var eventReferences = _dicReferences;

        foreach (var reference in eventReferences)
        {
            _sounds[reference.Key] = AudioManager.instance.CreateInstance(reference.Value);
        }
    }

    public void PlaySoundSFX(Sounds soundKey)
    {
        if (_sounds.ContainsKey(soundKey))
        {
            _sounds[soundKey].start();
        }
    }

    public void StopSoundSFX(Sounds soundKey, STOP_MODE mode)
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
            PLAYBACK_STATE playbackState;
            soundInstance.getPlaybackState(out playbackState);
            if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
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
