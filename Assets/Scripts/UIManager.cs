using FMODUnity;
using System.Collections;
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
    private Dictionary<Sounds, EventReference> soundsUI;

    private void Start()
    {
         soundsUI = FMODEvents.instance.References_UI;
         AudioManager.instance.PlaySoundSFX(soundsUI, ui_music); ///sonido ambiente
    }

    public void HighlightButton()
    {
        AudioManager.instance.PlaySoundSFX(soundsUI, ui_highlight);
    }
    public void PlayButton()
    {
        AudioManager.instance.PlaySoundSFX(soundsUI, ui_play);
    }

    public void BackButton()
    {
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
