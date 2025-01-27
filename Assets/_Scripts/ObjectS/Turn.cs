using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour
{
    public bool inInventory;

    private float speed = 50f;

    public float Speed { get => speed; set => speed = value; }

    void Update()
    {
        if (inInventory == true)
        {
            // Rotar el objeto continuamente
            transform.Rotate(Vector3.forward * Speed * Time.deltaTime);
        }
        else
        {
            transform.Rotate(Vector3.zero * Speed * Time.deltaTime);
        }
    }
}

