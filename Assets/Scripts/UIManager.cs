using FMODUnity;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private Dictionary<Sounds, EventReference> _eventReferencesUI;
    [field: SerializeField] private Sounds ui_highlight;
    [field: SerializeField] private Sounds ui_play;
    [field: SerializeField] private Sounds ui_cancel;
    [field: SerializeField] private Sounds ui_music;

    private void Awake()
    {
        _eventReferencesUI = FMODEvents.instance.EventReferencesUI;
        AudioManager.instance.InitializeInstances(_eventReferencesUI);      
    }
    private void Start()
    {
        AudioManager.instance.PlaySoundSFX(ui_music);
    }
    public void HighlightButton()
    {
        AudioManager.instance.PlaySoundSFX(ui_highlight);
        //AudioManager.instance.Sounds[ui_highlight].
 

    }
    public void PlayButton()
    {
        AudioManager.instance.PlaySoundSFX(ui_play);
    }
    public void BackButton()
    {
        AudioManager.instance.PlaySoundSFX(ui_cancel);
    }

}
