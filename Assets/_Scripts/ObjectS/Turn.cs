using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour
{

    void Update()
    {
        // Rotar el objeto continuamente
        transform.Rotate(Vector3.forward * 50 * Time.deltaTime);
    }
}

