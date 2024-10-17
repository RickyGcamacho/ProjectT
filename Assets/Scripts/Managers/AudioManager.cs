using FMODUnity;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [field: SerializeField] EventReference playTestSfxEvent;


    private void Start()
    {
        RuntimeManager.PlayOneShot(playTestSfxEvent);
    }

  
}
