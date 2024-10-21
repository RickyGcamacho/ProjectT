using FMODUnity;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [field: Header("Sounds"), Space(5)]
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

    public void StartScene(string value)
    {
        SceneManager.LoadScene(value);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
