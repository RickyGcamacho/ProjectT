
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [field: SerializeField] private Sounds _stepsSfx;
    [field: SerializeField] private Sounds _grabSfx;


 

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) // Reproduce el sonido 
        {
            //PlaySoundSFX(_stepsSfx);
        }

        if (Input.GetKeyDown(KeyCode.Y)) // Detiene el sonido
        {
            //StopSoundSFX(_stepsSfx, FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }
}
