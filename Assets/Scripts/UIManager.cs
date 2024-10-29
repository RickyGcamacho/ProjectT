using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using STOP_MODE = FMOD.Studio.STOP_MODE;

public class UIManager : MonoBehaviour
{
    [field: Header("Sounds"), Space(5)]
    [field: SerializeField] private Sounds ui_highlight;
    [field: SerializeField] private Sounds ui_play;
    [field: SerializeField] private Sounds ui_cancel;
    [field: SerializeField] private Sounds ui_music;
    private Dictionary<Sounds, EventReference> soundsUI;
    private EventInstance _event_generator;

    private void Start()
    {
        soundsUI = FMODEvents.instance.References_UI;
       _event_generator = AudioManager.instance.GetEventInstance(soundsUI, ui_music);


    }
   
    private void Update()
    {
        Generator();
      
    }
    public void Generator()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            _event_generator.start();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            AudioManager.instance.SetParameterByLabel(_event_generator, "generator_condition", "start");
            _event_generator.start();


        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            //Cambiar el valor del parámetro a 'start'
            AudioManager.instance.SetParameterByLabel(_event_generator, "generator_condition", "stop");
        }
    }
    public void HighlightButton()
    {
        AudioManager.instance.GetEventInstance(soundsUI, ui_highlight).start();
    }
    public void PlayButton()
    {
        AudioManager.instance.GetEventInstance(soundsUI, ui_play).start();
    }

    public void BackButton()
    {
        AudioManager.instance.GetEventInstance(soundsUI, ui_cancel).start();
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
