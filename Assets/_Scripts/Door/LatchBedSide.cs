using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LatchBedSide : MonoBehaviour
{
    private Rigidbody latch;
    private bool close;



    // Start is called before the first frame update

    private void Start()
    {
        latch = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        OpenOrCloseDoor();

    }

    public void OpenOrCloseDoor()
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
