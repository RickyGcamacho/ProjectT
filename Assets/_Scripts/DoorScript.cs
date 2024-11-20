using UnityEngine;
using UnityEngine.UI;

public class DoorScript : MonoBehaviour
{
    [SerializeField] private float maxGrabDistance = 20f; // Maximum raycast distance to check if there is a rigidbody on the way
    [SerializeField] private float maxEmptyDistance = 25f; // Maximum empty distance from the camera when moving shpere with mouse
    [SerializeField] private float grabSpring = 40f;  // Adjust grab strength values / SpringJoint values
    [SerializeField] private float grabDamper = 0.2f; // Adjust grab strength values / SpringJoint values
    [SerializeField] private float throwForce = 10.0f;
    [SerializeField] private float emptyMoveSpeed = 0.1f; // move empty with mouse speed 
    [SerializeField] private Image image1;  // Can grab image
    [SerializeField] private Image image2;  // Is grabbing image

    private Rigidbody _hitRigidbody;
    private RaycastHit _hitInfo;

    private GameObject _empty;
    private Rigidbody _emptyRb;


    [SerializeField] private Camera mainCamera;
    private void Start()
    {
        _empty = new GameObject();
        _empty.transform.parent = mainCamera.transform;
        _empty.AddComponent<Rigidbody>();
        _emptyRb = _empty.GetComponent<Rigidbody>();
        _emptyRb.isKinematic = true;

    }

    private void Update()
    {
        ShootRaycast();


        if (Input.GetMouseButton(0))
        {
            ApplySpringConstraint();
            MoveEmptyWithMouse();
        }
        else if (Input.GetMouseButtonUp(0) && _hitRigidbody != null)
        {

            // Release the object by removing the SpringJoint
            Destroy(_hitRigidbody.GetComponent<SpringJoint>());
            _hitRigidbody = null;



        }
    }

    private void ShootRaycast()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out _hitInfo, maxGrabDistance))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (_hitInfo.collider.GetComponent<Hide>() == true)
                {
                    _hitInfo.collider.GetComponent<Hide>().enter = true;
                }
            }

            if (Input.GetMouseButtonDown(0))
            {

                _hitRigidbody = _hitInfo.collider.GetComponent<Rigidbody>();
                MoveEmpty(_hitInfo.point);
            }
        }


    }

    private void MoveEmpty(Vector3 position)
    {
        _empty.transform.position = position;
        _empty.transform.parent = mainCamera.transform;
    }

    private void ApplySpringConstraint()
    {

        if (_hitRigidbody && _empty)
        {
            if (_hitRigidbody.gameObject.tag != "Pickup")
            {
                SpringJoint spring = _hitRigidbody.gameObject.GetComponent<SpringJoint>();
                if (!spring)
                {
                    spring = _hitRigidbody.gameObject.AddComponent<SpringJoint>();
                    spring.autoConfigureConnectedAnchor = false;
                    spring.connectedBody = _emptyRb;
                    spring.connectedAnchor = Vector3.zero;
                    spring.spring = grabSpring;
                    spring.damper = grabDamper;

                    spring.massScale = 1f;
                    spring.minDistance = 0.1f;
                    spring.maxDistance = 0f;

                    // Set the spring joint anchors
                    Vector3 localHitPoint = _hitInfo.point - _hitRigidbody.gameObject.transform.position;
                    Vector3 scaleOfHitObject = _hitRigidbody.gameObject.transform.localScale;
                    Quaternion rotation = Quaternion.Euler(_hitRigidbody.transform.rotation.eulerAngles);
                    rotation.x *= -1;
                    rotation.y *= -1;
                    rotation.z *= -1;
                    Vector3 rotatedLocalHitPoint = rotation * localHitPoint;
                    rotatedLocalHitPoint = new Vector3(
                        rotatedLocalHitPoint.x / scaleOfHitObject.x,
                        rotatedLocalHitPoint.y / scaleOfHitObject.y,
                        rotatedLocalHitPoint.z / scaleOfHitObject.z);
                    //Debug.Log(rotatedLocalHitPoint);
                    spring.anchor = rotatedLocalHitPoint;
                }



            }
        }
    }

    private void MoveEmptyWithMouse()
    {
        float mouseY = Input.GetAxis("Mouse Y");
        if (_empty)
        {

            Vector3 emptyMovement = mainCamera.transform.forward * (emptyMoveSpeed * mouseY);
            Vector3 emptyPos = _empty.transform.position + emptyMovement;

            // Clamp empty position relative to the camera
            float emptyDistanceFromCamera = Vector3.Distance(Camera.main.transform.position, emptyPos);
            if (emptyDistanceFromCamera <= maxEmptyDistance && emptyDistanceFromCamera > 0.4f)
            {
                _empty.transform.position = emptyPos;
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (_empty != null)
        {
            Gizmos.DrawWireSphere(_empty.transform.position, 0.2f);
        }
    }
}


