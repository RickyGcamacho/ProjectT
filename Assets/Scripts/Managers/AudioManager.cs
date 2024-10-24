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

    public void PlaySoundSFX(Dictionary<Sounds, EventReference> typeSound, Sounds soundKey)
    {     
        if (typeSound.ContainsKey(soundKey))
        {
            EventInstance instance = RuntimeManager.CreateInstance(typeSound[soundKey]);        
            instance.start();
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
