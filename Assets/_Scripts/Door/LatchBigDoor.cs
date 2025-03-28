using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class LatchBigDoor : MonoBehaviour
{
    private Rigidbody latch;
    private bool close;
    [SerializeField] private string keyTag;
    private int cerrojo = 0;



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

        if (Input.GetMouseButton(0) && cerrojo == 1)
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


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.StartsWith(keyTag) && collision.gameObject.tag != "Floor")
        {
            cerrojo = 1;
            Destroy(collision.gameObject);

        }


    }

}
