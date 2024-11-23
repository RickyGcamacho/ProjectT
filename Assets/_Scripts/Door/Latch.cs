using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Latch : MonoBehaviour
{
    private Rigidbody latch;
    private bool close;

    public bool Close { get => close; set => close = value; }

    // Start is called before the first frame update
    void Start()
    {
        latch = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        OpenOrCloseDoor();
    }

    private void OpenOrCloseDoor()
    {
        if (Input.GetMouseButton(0))
        {
            latch.isKinematic = false;
            close = false;
        }
        else
        {
            latch.isKinematic = true;
            close = true;
        }
    }
}
