using UnityEngine;
public class ObjectHandling : MonoBehaviour
{
    public GameObject player;
    //if you copy from below this point, you are legally required to like the video
    public float throwForce = 500f; //force at which the object is thrown at
    public float pickUpRange = 5f; //how far the player can pickup the object from
    private GameObject heldObj; //object which we pick up
    private Rigidbody heldObjRb; //rigidbody of object we pick up
    private Vector3 offSet = new Vector3(0, -0.2f, 0);//Vector so that the grab element appears more in the center when following the camera
 
    public void PickUpObject()
    {
        if (heldObj != null)
        {
            heldObjRb = heldObj.GetComponent<Rigidbody>(); // asigna el Rigidbody
            heldObjRb.isKinematic = true;
            Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), true);
        }
        else
        {
            Debug.LogWarning("No se puede levantar el objeto, porque 'heldObj' es nulo.");
        }
    }

    public void DropObject()
    {
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        heldObjRb.isKinematic = false;
        heldObj.transform.parent = null; //unparent object
    }
    public void MoveObject()
    {
        heldObj.transform.SetParent(this.transform);
        //heldObj.transform.LookAt(this.transform);
    }


    

    public void ThrowObject()
    {
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        heldObjRb.isKinematic = false;
        heldObj.transform.parent = null;

        heldObjRb.AddForce(transform.forward * throwForce);

    }
    public void StopClipping() //function only called when dropping/throwing
    {
        var clipRange = Vector3.Distance(heldObj.transform.position, transform.position); //distance from holdPos to the camera
        //have to use RaycastAll as object blocks raycast in center screen
        //RaycastAll returns array of all colliders hit within the cliprange
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.forward), clipRange);
        //if the array length is greater than 1, meaning it has hit more than just the object we are carrying
        if (hits.Length > 1)
        {
            //change object position to camera position 
            heldObj.transform.position = transform.position + new Vector3(0f, -0.5f, 0f);
            heldObj = null;//offset slightly downward to stop object dropping above player 
            //if your player is small, change the -0.5f to a smaller number (in magnitude) ie: -0.1f
        }
        else
        {
            heldObj = null;
        }

    }

    public void SetHeldObj(GameObject obj)
    {
        heldObj = obj;
    }
}