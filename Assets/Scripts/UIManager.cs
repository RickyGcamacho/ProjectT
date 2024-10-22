using FMOD.Studio;
using FMODUnity;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [field: Header("Sounds"), Space(5)]
    [field: SerializeField] private Sounds ui_highlight;
    [field: SerializeField] private Sounds ui_play;
    [field: SerializeField] private Sounds ui_cancel;
    [field: SerializeField] private Sounds ui_music;


    private void Start()
    {
        //AudioManager.instance?.PlaySoundSFX(_soundsUI,ui_music);
    }
    public void HighlightButton()
    {
        var temp = AudioManager.instance.SoundsUI;
        AudioManager.instance.PlaySoundSFX(temp, ui_highlight);
    }
    public void PlayButton()
    {
        var temp = AudioManager.instance.SoundsUI;
        AudioManager.instance.PlaySoundSFX(temp, ui_play);
    }

    public void BackButton()
    {
        var temp = AudioManager.instance.SoundsUI;
        AudioManager.instance.PlaySoundSFX(temp, ui_cancel);
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
