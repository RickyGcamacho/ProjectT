using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactuable : MonoBehaviour
{
    public GameObject menuInteractuable;

    private PlayerActions playerAction;
    private float range = 5f;

    private void Start()
    {
        menuInteractuable.SetActive(false);
        playerAction = GetComponent<PlayerActions>();
    }

    private void Update()
    {
        Intaraction();
    }

    void Intaraction()
    {
        if (Input.GetMouseButtonDown(0))
        {

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, range))
            {
                if (hit.transform.gameObject.tag == "Interactuable")
                {
                    menuInteractuable.SetActive(true);
                }
            }

        }
    }
}
