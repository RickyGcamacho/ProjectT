using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public GameObject flashlightCrouching, flashlightStandUp;
    private bool isOn;

    private void Update()
    {
        Interact();
    }
    public void Interact()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            isOn = !isOn; // Alterna el valor de isOn entre true y false
            flashlightStandUp.SetActive(isOn); // Activa o desactiva la linterna según el estado de isOn
        }




    }

}
