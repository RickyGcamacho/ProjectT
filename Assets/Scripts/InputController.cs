using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController
{
    private PlayerController _playerController;
    
    public InputController(PlayerController playerController)
    {
        _playerController = playerController;
    }
    
    public void MyUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("Interaction");
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            Debug.Log("Zooming in");
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            Debug.Log("Mouse wheel up");
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f ) // backwards
        {
            Debug.Log("Mouse wheel down");
        }
        
        // Mouse movement to move camera

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            Debug.Log("Moving");
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Turn on or off the flashlight");
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("Healing");
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Debug.Log("Crouch");
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Debug.Log("Run");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Pause menu");
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("Select items");
        }
    }
}