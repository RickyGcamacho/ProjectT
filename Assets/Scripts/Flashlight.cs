using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public GameObject flashlight;
    private bool on;

    private void Start()
    {
        on = true;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        FlashlightSystem();
    }

    void FlashlightSystem()
    {
        if (Input.GetMouseButton(0))
        {
            if (on)
            {
                flashlight.SetActive(false);
                on = false;
            }
            else
            {
                flashlight.SetActive(true);
                on = true;
            }
        }
    }

}
