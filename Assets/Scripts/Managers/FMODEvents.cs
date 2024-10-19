using FMODUnity;
using System.Collections.Generic;
using UnityEngine;

public class FMODEvents : MonoBehaviour
{
    public Dictionary<SoundsPlayer, EventReference> EventReferencesPlayer { get; private set; }
    public Dictionary<SoundsEnemy, EventReference> EventReferencesEnemy { get; private set; }

    [field: Header("Player"), Space(5)]

    [field: SerializeField] public EventReference playerSteps_SEvent { get; private set; }
    [field: SerializeField] public EventReference flashLight_SEvent { get; private set; }
    [field: SerializeField] public EventReference playerRun_SEvent { get; private set; }
    [field: SerializeField] public EventReference heartBeats_SEvent { get; private set; }
    [field: SerializeField] public EventReference heal_SEvent { get; private set; }
    [field: SerializeField] public EventReference grab_SEvent { get; private set; }

    [field: Header("Enemy"), Space(5)]

    [field: SerializeField] public EventReference enemySteps_SEvent { get; private set; }
    [field: SerializeField] public EventReference enemyRun_SEvent { get; private set; }
    [field: SerializeField] public EventReference enemyDetect_SEvent { get; private set; }

    [field: Header("UI"), Space(5)]
    [field: SerializeField] public EventReference button_SEvent { get; private set; }
    [field: SerializeField] public EventReference talk_SEvent { get; private set; }


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


    }

    private void InitializeEventRefPlayer()
    {
        EventReferencesPlayer = new Dictionary<SoundsPlayer, EventReference>
        {
            { SoundsPlayer.PLAYER_WALK, playerSteps_SEvent },
            { SoundsPlayer.FLASHLIGHT_INTERACTION, flashLight_SEvent },
            { SoundsPlayer.PLAYER_RUN, playerRun_SEvent },
            { SoundsPlayer.PLAYER_HEARTBEATS, heartBeats_SEvent },
            { SoundsPlayer.PLAYER_HEAL, heal_SEvent },
            { SoundsPlayer.PLAYER_GRAB, grab_SEvent }
        };
    }

    private void InitializeEventRefEnemy()
    {
        EventReferencesEnemy = new Dictionary<SoundsEnemy, EventReference>
        {
            { SoundsEnemy.ENEMY_STEPS, enemySteps_SEvent },
            { SoundsEnemy.ENEMY_RUN, enemyRun_SEvent },
            { SoundsEnemy.ENEMY_DETECT, enemyDetect_SEvent }
        };
    }
}
