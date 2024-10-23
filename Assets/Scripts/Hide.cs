using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : MonoBehaviour
{
    public GameObject player;


    private bool trigger, isHide;

    private void OnTriggerEnter(Collider other)
    {
        trigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        trigger = false;
    }

    private void Update()
    {
        IsHidePlayer();
    }

    void IsHidePlayer()
    {
        if (Input.GetKeyDown(KeyCode.E) && trigger == true && isHide == false)
        {
            player.GetComponent<Animation>().Play("Hide");
            player.GetComponent<PlayerActions>().Speed = 0;
            isHide = true;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.E) && trigger == false && isHide == true)
            {
                player.GetComponent<Animation>().Play("Unhide");
                player.GetComponent<PlayerActions>().Speed = 5;
                isHide = false;
            }
        }
    }
}
