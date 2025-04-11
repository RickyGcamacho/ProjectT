
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// TODO
/// Hacer script modular para los otros objetos TOGGLES

public class ObjectsSwitchable : MonoBehaviour
{
    public TextMeshProUGUI message;

    private Light ligth = null;
    private bool onObject,inRange;



    private void Start()
    {
        if (message.text != null)
        {
            message.gameObject.SetActive(false);
        }
        ligth = GetComponentInChildren<Light>();
            onObject = true;
            if (ligth.GetComponent<Light>().enabled == true)
            {

                message.text = "Presiona la tecla O para encender";
            }
            else
            {

                message.text = "Presiona la tecla O para apagar";
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
            if (Input.GetKeyDown(KeyCode.O))
            {
                if (ligth != null)
                {
                    if (onObject == true)
                    {
                        ligth.GetComponent<Light>().enabled = false;
                        onObject = false;
                        message.text = "Presiona la tecla O para encender";
                    }
                    else
                    {
                        ligth.GetComponent<Light>().enabled = true;
                        onObject = true;
                        message.text = "Presiona la tecla O  para apagar";
                    }
                }
                else if (ligth == null)
                {
                    if (onObject == true)
                    {
                        Debug.Log(gameObject.name + " se ha apagado");
                        onObject = false;
                        message.text = "Presiona la tecla O  para encender";
                    }
                    else
                    {
                        Debug.Log(gameObject.name + " se ha encendido");
                        onObject = true;
                        message.text = "Presiona la tecla O  para apagar";
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
