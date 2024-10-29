using FMOD.Studio;
using FMODUnity;
using System.Collections.Generic;
using UnityEngine;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Player"), Space(5)]
    [field: SerializeField] public EventReference Player_Steps { get; private set; }
    [field: SerializeField] public EventReference Player_Flashlight { get; private set; }
    [field: SerializeField] public EventReference Player_Run { get; private set; }
    [field: SerializeField] public EventReference Player_HeartBeats { get; private set; }
    [field: SerializeField] public EventReference Player_GrabObject { get; private set; }
    public Dictionary<Sounds, EventReference> References_Player { get; private set; }

    [field: Header("Enemy"), Space(5)]
    [field: SerializeField] public EventReference Enemy_Steps { get; private set; }
    [field: SerializeField] public EventReference Enemy_Run { get; private set; }
    [field: SerializeField] public EventReference Enemy_DetectPlayer { get; private set; }
    public Dictionary<Sounds, EventReference> References_Enemy { get; private set; }


    [field: Header("Props"), Space(5)]
    [field: SerializeField] public EventReference Prop_Generator { get; private set; }
    [field: SerializeField] public EventReference Prop_TV { get; private set; }
    [field: SerializeField] public EventReference Prop_Radio { get; private set; }
    public Dictionary<Sounds, EventReference> References_Props { get; private set; }

    [field: Header("UI"), Space(5)]
    [field: SerializeField] public EventReference Button_Play { get; private set; }
    [field: SerializeField] public EventReference Button_Highlight { get; private set; }
    [field: SerializeField] public EventReference Button_Cancel { get; private set; }
    [field: SerializeField] public EventReference UI_Music { get; private set; }
    public Dictionary<Sounds, EventReference> References_UI { get; private set; }

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
        SetBanksProps();
        SetBanksUI();
    }

    private void SetBanksPlayer()
    {
        References_Player = new Dictionary<Sounds, EventReference>
        {
            { Sounds.PLAYER_WALK, Player_Steps },
            { Sounds.FLASHLIGHT_INTERACTION, Player_Flashlight },
            { Sounds.PLAYER_RUN, Player_Run },
            { Sounds.PLAYER_HEARTBEATS, Player_HeartBeats },
            { Sounds.PLAYER_GRAB, Player_GrabObject }
        };
    }

    private void SetBanksEnemy()
    {
        References_Enemy = new Dictionary<Sounds, EventReference>
        {
            { Sounds.ENEMY_STEPS, Enemy_Steps },
            { Sounds.ENEMY_RUN, Enemy_Run },
            { Sounds.ENEMY_DETECT, Enemy_DetectPlayer }
        };
    }

    private void SetBanksProps()
    {
        References_Props = new Dictionary<Sounds, EventReference>
        {
            { Sounds.PROPS_GENERATOR, Prop_Generator },
            { Sounds.PROPS_RADIO, Prop_Radio },
            { Sounds.PROPS_TV, Prop_TV }
        };
    }
    private void SetBanksUI()
    {
        References_UI = new Dictionary<Sounds, EventReference>
        {
            { Sounds.UI_HIGHLIGHT, Button_Highlight },
            { Sounds.UI_PLAYBUTTON, Button_Play },
            { Sounds.UI_CANCEL, Button_Cancel },
            { Sounds.UI_MUSIC, UI_Music },
        };
    }

}
