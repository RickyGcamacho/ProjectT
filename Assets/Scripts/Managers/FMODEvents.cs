using FMODUnity;
using System.Collections.Generic;
using UnityEngine;

public class FMODEvents : MonoBehaviour
{
    public Dictionary<Sounds, EventReference> EventReferencesPlayer { get; private set; }
    public Dictionary<Sounds, EventReference> EventReferencesEnemy { get; private set; }
    public Dictionary<Sounds, EventReference> EventReferencesUI { get; private set; }

    [field: Header("Player"), Space(5)]

    [field: SerializeField] public EventReference playerSteps_SEvent { get; private set; }
    [field: SerializeField] public EventReference flashLight_SEvent { get; private set; }
    [field: SerializeField] public EventReference playerRun_SEvent { get; private set; }
    [field: SerializeField] public EventReference heartBeats_SEvent { get; private set; }
    [field: SerializeField] public EventReference grab_SEvent { get; private set; }

    [field: Header("Enemy"), Space(5)]

    [field: SerializeField] public EventReference enemySteps_SEvent { get; private set; }
    [field: SerializeField] public EventReference enemyRun_SEvent { get; private set; }
    [field: SerializeField] public EventReference enemyDetect_SEvent { get; private set; }

    [field: Header("UI"), Space(5)]
    [field: SerializeField] public EventReference ui_play_SEvent { get; private set; }
    [field: SerializeField] public EventReference ui_highlight_SEvent { get; private set; }
    [field: SerializeField] public EventReference ui_cancel_SEvent { get; private set; }
    [field: SerializeField] public EventReference ui_music_SEvent { get; private set; }


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

        InitializeEventRefPlayer();
        InitializeEventRefEnemy();
        InitializeEventRefUI();


    }

    private void InitializeEventRefPlayer()
    {
        EventReferencesPlayer = new Dictionary<Sounds, EventReference>
        {
            { Sounds.PLAYER_WALK, playerSteps_SEvent },
            { Sounds.FLASHLIGHT_INTERACTION, flashLight_SEvent },
            { Sounds.PLAYER_RUN, playerRun_SEvent },
            { Sounds.PLAYER_HEARTBEATS, heartBeats_SEvent },
            { Sounds.PLAYER_GRAB, grab_SEvent }
        };
    }

    private void InitializeEventRefEnemy()
    {
        EventReferencesEnemy = new Dictionary<Sounds, EventReference>
        {
            { Sounds.ENEMY_STEPS, enemySteps_SEvent },
            { Sounds.ENEMY_RUN, enemyRun_SEvent },
            { Sounds.ENEMY_DETECT, enemyDetect_SEvent }
        };
    }

    private void InitializeEventRefUI()
    {
        EventReferencesUI = new Dictionary<Sounds, EventReference>
        {
            { Sounds.UI_HIGHLIGHT, ui_highlight_SEvent },
            { Sounds.UI_PLAYBUTTON, ui_play_SEvent },
            {Sounds.UI_CANCEL, ui_cancel_SEvent },
            { Sounds.UI_MUSIC, ui_music_SEvent },
        };
    }
}
