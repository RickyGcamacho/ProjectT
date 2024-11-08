using FMOD.Studio;
using FMODUnity;
using System.Collections.Generic;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;

[RequireComponent(typeof(Rigidbody))]
public class PlayerView : MonoBehaviour
{
    [field: SerializeField] private Sounds _sfxWalk;
    [field: SerializeField] private Sounds _sfxRun; //TODO
    [field: SerializeField] private Sounds _sfxCrouch;
    [field: SerializeField] private Sounds _sfxFlashlight; //TODO
    [field: SerializeField] private Sounds _sfxGrab; //TODO
    private Rigidbody _rb;
    private Dictionary<Sounds, EventReference> _banksPlayer;
    private EventInstance playerWalk;
    private EventInstance playerCrouch;
    private bool canCrouch = false;
    private bool wasCrouching = false;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        
    }


    private void Start()
    {
        _banksPlayer = FMODEvents.instance.References_Player;
        playerWalk = AudioManager.instance.GetEventInstance(_banksPlayer, _sfxWalk);
        playerCrouch = AudioManager.instance.GetEventInstance(_banksPlayer, _sfxCrouch);
        
    }



    private void Update()
    {
        // Detecta si el jugador está agachado
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            canCrouch = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            canCrouch = false;
        }

        // Determina qué sonido reproducir basado en el estado de agachado
        if (canCrouch)
        {
            // Reproduce el sonido de agacharse si el jugador está agachado
            playerWalk.stop(STOP_MODE.ALLOWFADEOUT); // Detiene el sonido de caminar
            Movement(playerCrouch);
        }
        else
        {
            // Reproduce el sonido de caminar si el jugador no está agachado
            playerCrouch.stop(STOP_MODE.ALLOWFADEOUT); // Detiene el sonido de agacharse
            Movement(playerWalk);
        }
    }

    public void Movement(EventInstance move)
    {
        if (_rb.velocity != Vector3.zero) // Reproduce el sonido cuando el jugador se mueve
        {
            PLAYBACK_STATE playbackState;
            move.getPlaybackState(out playbackState);
            if (playbackState == PLAYBACK_STATE.STOPPED)
            {
                move.start();
            }
        }
        else
        {
            move.stop(STOP_MODE.ALLOWFADEOUT); // Detiene el sonido cuando el jugador deja de moverse
        }
    }



}
