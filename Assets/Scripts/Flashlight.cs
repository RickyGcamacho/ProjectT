using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public GameObject flashlight;
    private bool isOn;

    private void Update()
    {
        Interact();
    }
    void Interact()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isOn = !isOn; // Alterna el valor de isOn entre true y false
            flashlight.SetActive(isOn); // Activa o desactiva la linterna según el estado de isOn
        }




    }

}
