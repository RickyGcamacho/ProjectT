using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsSwitchable : MonoBehaviour
{
    private Light ligth = null;
    private bool onObject;



    private void Start()
    {
        ligth = GetComponentInChildren<Light>();
        onObject = true;
    }

    void OnMouseDown()
    {
        if (gameObject.tag == "Switchable")
        {

                if (ligth != null)
                {
                    if (onObject == true)
                    {
                        ligth.gameObject.SetActive(false);
                    onObject = false;
                    }
                    else
                    {
                        ligth.gameObject.SetActive(true);
                    onObject = true;
                    }
                }
            else if (ligth == null)
            {
                if (onObject == true)
                {
                    Debug.Log(gameObject.name + " se ha apagado");
                    onObject = false;
                }
                else
                {
                    Debug.Log(gameObject.name + " se ha encendido");
                    onObject = true;
                }
                
            }
        }
        
        
    }
}
