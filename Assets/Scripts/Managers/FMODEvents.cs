using FMOD.Studio;
using FMODUnity;
using System.Collections.Generic;
using UnityEngine;

public class FMODEvents : MonoBehaviour
{

    [field: Header("Player"), Space(5)]
    public Dictionary<Sounds, EventInstance> Sounds_Player { get; private set; }
    public Dictionary<Sounds, EventReference> References_Player { get; private set; }

    [field: SerializeField] public EventReference Event_PlayerSteps { get; private set; }
    [field: SerializeField] public EventReference Event_Flashlight { get; private set; }
    [field: SerializeField] public EventReference Event_PlayerRun { get; private set; }
    [field: SerializeField] public EventReference Event_HeartBeats { get; private set; }
    [field: SerializeField] public EventReference Event_GrabObject { get; private set; }

    [field: Header("Enemy"), Space(5)]
    public Dictionary<Sounds, EventInstance> Sounds_Enemy { get; private set; }
    public Dictionary<Sounds, EventReference> References_UI { get; private set; }
    public Dictionary<Sounds, EventReference> References_Enemy { get; private set; }

    [field: SerializeField] public EventReference Event_EnemySteps { get; private set; }
    [field: SerializeField] public EventReference Event_EnemyRun { get; private set; }
    [field: SerializeField] public EventReference Event_DetectPlayer { get; private set; }

    [field: Header("UI"), Space(5)]
    public Dictionary<Sounds, EventInstance> Sounds_UI { get; private set; }
    [field: SerializeField] public EventReference Event_PlayUI { get; private set; }
    [field: SerializeField] public EventReference Event_HighlightUI { get; private set; }
    [field: SerializeField] public EventReference Event_CancelUI { get; private set; }
    [field: SerializeField] public EventReference Event_MenuMusicUI { get; private set; }


    public static FMODEvents instance { get; private set; }


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        // Si no hay instancia, asigna esta instancia
        instance = this;

        InitializeReferences();

    }
    public void InitializeReferences()
    {
        SetBanksPlayer();
        SetBanksEnemy();
        SetBanksUI();
    }

    private void SetBanksPlayer()
    {
        References_Player = new Dictionary<Sounds, EventReference>
        {
            { Sounds.PLAYER_WALK, Event_PlayerSteps },
            { Sounds.FLASHLIGHT_INTERACTION, Event_Flashlight },
            { Sounds.PLAYER_RUN, Event_PlayerRun },
            { Sounds.PLAYER_HEARTBEATS, Event_HeartBeats },
            { Sounds.PLAYER_GRAB, Event_GrabObject }
        };
    }

    private void SetBanksEnemy()
    {
        References_Enemy = new Dictionary<Sounds, EventReference>
        {
            { Sounds.ENEMY_STEPS, Event_EnemySteps },
            { Sounds.ENEMY_RUN, Event_EnemyRun },
            { Sounds.ENEMY_DETECT, Event_DetectPlayer }
        };
    }

    private void SetBanksUI()
    {
        References_UI = new Dictionary<Sounds, EventReference>
        {
            { Sounds.UI_HIGHLIGHT, Event_HighlightUI },
            { Sounds.UI_PLAYBUTTON, Event_PlayUI },
            { Sounds.UI_CANCEL, Event_CancelUI },
            { Sounds.UI_MUSIC, Event_MenuMusicUI },
        };
    }

    public Dictionary<Sounds, EventInstance> CreateInstances(Dictionary<Sounds, EventReference> dicReferences)
    {
        // Crear un nuevo diccionario para almacenar las instancias de eventos
        Dictionary<Sounds, EventInstance> soundsInstances = new Dictionary<Sounds, EventInstance>();

        foreach (var reference in dicReferences)
        {
            // Crear la instancia del evento
            EventInstance instance = RuntimeManager.CreateInstance(reference.Value);

            // Almacenar en el diccionario
            soundsInstances[reference.Key] = instance;
        }

        // Retornar el diccionario de instancias
        return soundsInstances;
    }
}
