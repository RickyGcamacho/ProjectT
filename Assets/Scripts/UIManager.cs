using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public int test = 3;
    [field: Header("Sounds"), Space(5)]
    [field: SerializeField] private Sounds ui_highlight;
    [field: SerializeField] private Sounds ui_play;
    [field: SerializeField] private Sounds ui_cancel;
    [field: SerializeField] private Sounds ui_music;
    private Dictionary<Sounds, EventReference> soundsUI;

    [field: SerializeField] public EventReference Event_Test { get; private set; }
    private void Start()
    {
        soundsUI = FMODEvents.instance.References_UI;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            AudioManager.instance.PlayOneShot(Event_Test,transform.position,test);
        }
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
