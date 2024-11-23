using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchingSystem : MonoBehaviour
{
    private PlayerActions player;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("Player_Agus").GetComponent<PlayerActions>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player.IsTableUnder = true;

        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            player.IsTableUnder = false;

        }
        
    }
}
