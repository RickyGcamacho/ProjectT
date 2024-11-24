using UnityEngine;
public class ObjectHandling : MonoBehaviour
{
    public GameObject player;
    public Transform holdPoint; // Punto donde se sostiene el objeto
    public float moveSpeed = 10f; // Velocidad de movimiento hacia el holdPoint
    //if you copy from below this point, you are legally required to like the video
    public float throwForce = 500f; //force at which the object is thrown at
    public float pickUpRange = 5f; //how far the player can pickup the object from
    private GameObject heldObj; //object which we pick up
    private Rigidbody heldObjRb; //rigidbody of object we pick up
    private ItemObject itemObject;
    private Vector3 offSet = new Vector3(0, -0.2f, 0);//Vector so that the grab element appears more in the center when following the camera

    private void Start()
    {
        itemObject = GameObject.FindGameObjectWithTag("Saveables").GetComponent<ItemObject>();
    }
    void Update()
    {
        // Continuar moviendo el objeto si está agarrado
        if (heldObj != null)
        {
            MoveObject();

           
            if (Input.GetKey(KeyCode.X))
            {
                DropObject();
                itemObject.enabled = true;
            }
        }
    }

    public void PickUpObject()
    {
        if (heldObj != null)
        {
            heldObjRb = heldObj.GetComponent<Rigidbody>(); // asigna el Rigidbody
            heldObjRb.useGravity = false; // Desactiva la gravedad
            heldObjRb.constraints = RigidbodyConstraints.None; // Permite movimiento y rotación
            Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), true);
            itemObject.enabled = false;

        }
        else
        {
            Debug.LogWarning("No se puede levantar el objeto, porque 'heldObj' es nulo.");
        }
    }

    public void DropObject()
    {
        if (heldObj != null)
        {
            // Restaurar las propiedades físicas originales
            Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
            heldObj.transform.parent = null;
            heldObjRb.useGravity = true;
            heldObj = null;
        }


    }

    public void MoveObject()
    {
        if (heldObj != null)
        {
            // Calcular la dirección hacia el punto de agarre
            Vector3 direction = (holdPoint.position - heldObj.transform.position);

            // Aplicar una fuerza para mover el objeto hacia el punto
            heldObjRb.velocity = direction * moveSpeed;

            // Asegurarse de que el objeto siga rotando normalmente
            heldObjRb.angularVelocity = Vector3.zero;
        }
    }




    public void ThrowObject()
    {
        if (heldObj != null)
        {
            Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
            heldObj.transform.parent = null;
            heldObjRb.useGravity = true;
            heldObjRb.AddForce(transform.forward * throwForce);
        }
      

    }
    public void StopClipping() //function only called when dropping/throwing
    {
        if (heldObj != null)
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

    }

    public void SetHeldObj(GameObject obj)
    {
        heldObj = obj;
    }
}