using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : MonoBehaviour
{
    public GameObject player;


    private bool trigger, isHide;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            trigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
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
            isHide = true;
            player.GetComponent<PlayerActions>().enabled = false;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.E) && trigger == false && isHide == true)
            {
                player.GetComponent<Animation>().Play("Unhide");
                isHide = false;
                player.GetComponent<PlayerActions>().enabled = true;
            }
        }

    }
}
