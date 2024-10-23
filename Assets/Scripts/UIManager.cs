using System.Collections;
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
        var soundsUI = FMODEvents.instance.SoundsUI;
        AudioManager.instance.PlaySoundSFX(soundsUI, ui_music); ///sonido ambiente
    }

    public void HighlightButton()
    {
        var soundsUI = FMODEvents.instance.SoundsUI;
        AudioManager.instance.PlaySoundSFX(soundsUI, ui_highlight);
    }
    public void PlayButton()
    {
        var soundsUI = FMODEvents.instance.SoundsUI;
        AudioManager.instance.PlaySoundSFX(soundsUI, ui_play);
    }

    public void BackButton()
    {
        var soundsUI = FMODEvents.instance.SoundsUI;
        AudioManager.instance.PlaySoundSFX(soundsUI, ui_cancel);
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
