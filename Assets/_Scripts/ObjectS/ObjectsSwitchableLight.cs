using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectsSwitchableLight : MonoBehaviour
{
    public TextMeshProUGUI message;
    public GameObject luz;
    private Light light = null;
    private bool onObject, inRange;



    private void Start()
    {
        if (message.text != null)
        {
            message.gameObject.SetActive(false);
        }
        light = GetComponentInChildren<Light>();
        onObject = true;
        if (light.GetComponentInChildren<Light>().enabled != true)
        {

            message.text = "Haz click para encender";
        }
        else
        {

            message.text = "Haz click para apagar";
        }
    }

    private void Update()
    {
        OnOff();
    }

    void OnOff()
    {
        if (gameObject.tag == "Switchable" && inRange)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (light != null)
                {
                    if (onObject == true)
                    {
                        light.GetComponentInChildren<Light>().enabled = false;
                        luz.SetActive(false);
                        onObject = false;
                        message.text = "Haz click para encender";
                    }
                    else
                    {
                        light.GetComponentInChildren<Light>().enabled = true;
                        luz.SetActive(true);
                        onObject = true;
                        message.text = "Haz click para apagar";
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            inRange = true;

            if (message != null)
            {

                message.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            inRange = false;

            if (message != null)
            {
                message.gameObject.SetActive(false);
            }
        }
    }
}