
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// TODO
/// Hacer script modular para los otros objetos TOGGLES

public class ObjectsSwitchableSound : MonoBehaviour
{
    public TextMeshProUGUI message;

    private AudioSource audio = null;
    private bool onObject,inRange;



    private void Start()
    {
        if (message.text != null)
        {
            message.gameObject.SetActive(false);
        }
             audio = GetComponentInChildren<AudioSource>();
            onObject = true;
            if (audio.GetComponentInChildren<AudioSource>().enabled != true)
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
                if (audio != null)
                {
                    if (onObject == true)
                    {
                        audio.GetComponentInChildren<AudioSource>().enabled = false;
                        onObject = false;
                        message.text = "Haz click para encender";
                    }
                    else
                    {
                        audio.GetComponentInChildren<AudioSource>().enabled = true;
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
