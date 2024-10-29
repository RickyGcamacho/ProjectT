using FMOD.Studio;
using FMODUnity;
using System.Collections.Generic;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;

[RequireComponent(typeof(Rigidbody))]
public class PlayerView : MonoBehaviour
{
    [field: SerializeField] private Sounds _sfxWalk;
    [field: SerializeField] private Sounds _sfxRun;
    [field: SerializeField] private Sounds _sfxCrouch;
    [field: SerializeField] private Sounds _sfxFlashlight;
    [field: SerializeField] private Sounds _sfxGrab;
    private Rigidbody _rb;
    private Dictionary<Sounds, EventReference> _banksPlayer;
    private EventInstance playerWalk;
    private EventInstance playerCrouch;

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



        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Movement(playerCrouch);
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            Movement(playerWalk);
        }
        else
        {
            Movement(playerWalk);
        }

    }

    public void Movement(EventInstance move)
    {

        if (_rb.velocity != Vector3.zero) // Reproduce el sonido 
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
            move.stop(STOP_MODE.ALLOWFADEOUT);
        }

    }
    public void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            AudioManager.instance.PlayOneShot(_banksPlayer, _sfxCrouch);
        }
    }

}
