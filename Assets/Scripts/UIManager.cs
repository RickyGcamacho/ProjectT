using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public string[] states;
    public int count;
    [field: Header("Sounds"), Space(5)]
    [field: SerializeField] private Sounds ui_highlight;
    [field: SerializeField] private Sounds ui_play;
    [field: SerializeField] private Sounds ui_cancel;
    [field: SerializeField] private Sounds ui_music;
    private Dictionary<Sounds, EventReference> soundsUI;
    private EventInstance _instance;

    [field: SerializeField] public EventReference Event_Test { get; private set; }
    private void Start()
    {
        soundsUI = FMODEvents.instance.References_UI;
        _instance = AudioManager.instance.PlayOneShot(Event_Test, transform.position);


    }
    public PARAMETER_ID GetID(EventInstance instance, string parameterName)
    {
        EventDescription eventDescription;
        instance.getDescription(out eventDescription);
        PARAMETER_DESCRIPTION parameterDescription;
        eventDescription.getParameterDescriptionByName(parameterName, out parameterDescription);
        return parameterDescription.id;

    }
    public void SetParameterByLabel(EventInstance instance,string parameterName, string label)
    {
        // Obtiene el PARAMETER_ID del parámetro (asumiendo que tienes la función GetID)
        PARAMETER_ID parameterID = GetID(instance, parameterName);

        // Llama a setParameterByIDWithLabel para ajustar el valor usando la etiqueta
        instance.setParameterByIDWithLabel(parameterID, label);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            SetParameterByLabel(_instance, "generator_condition", "notstart");
            _instance.start();

        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            //Cambiar el valor del parámetro a 'start'
            SetParameterByLabel(_instance,"generator_condition" ,"start");
            _instance.start();


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
