using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [Header("Walk Paramenter")]
    [SerializeField] private float speedWalk;


    [Header("Look Paramenter")]
    [SerializeField, Range(1, 10)] private float lookSpeedX;
    [SerializeField, Range(1, 10)] private float lookSpeedY;
    [SerializeField, Range(1, 180)] private float lowerLookLimit;
    [SerializeField, Range(1, 180)] private float uperLookLimit;




    [Header("Zoom Parameters")]
    private Coroutine zoomRoutine;
    [SerializeField] private float timeToZoom, zoomFOV;
    private float defaultFOV;

    [Header("Run Parameters")]
    [SerializeField] private float runSpeed;

    [Header("Parameters")]
    [SerializeField] private float height, crouchedSpeed, crouchedHeightSizeOriginal, crouchedHeightCenterOriginal, crouchedHeightSizeNew, crouchedHeightCenterNew;

    public GameObject cameraCrouched;
    public bool isNotCrouching;

    private Camera playerCamera;
    private Rigidbody _rb;
    private Vector3 moveDirection;
    private Vector2 currentInput;
    private float rotationX, x, z;


    public float LookSpeedX { get => lookSpeedX; set => lookSpeedX = value; }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        playerCamera = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        defaultFOV = playerCamera.fieldOfView;
        isNotCrouching = true;
    }

    void Update()
    {
        Walking();
        Crouching();
        Running();
        HandleMouseLook();
        HandleZoom();

    }

    private void Crouching()
    {

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
          CrouchingMovement();
            if (isNotCrouching == false)
            {
                cameraCrouched.SetActive(true);
                playerCamera.gameObject.SetActive(false);
                GetComponent<BoxCollider>().size = new Vector3(GetComponent<BoxCollider>().size.x, crouchedHeightSizeNew, GetComponent<BoxCollider>().size.z);
                GetComponent<BoxCollider>().center = new Vector3(GetComponent<BoxCollider>().center.x, crouchedHeightCenterNew, GetComponent<BoxCollider>().center.z);
                Movement(crouchedSpeed);
            }
            else
            {
                //pararse
                cameraCrouched.SetActive(false);
                playerCamera.gameObject.SetActive(true);
                GetComponent<BoxCollider>().size = new Vector3(GetComponent<BoxCollider>().size.x, crouchedHeightSizeOriginal, GetComponent<BoxCollider>().size.z);
                GetComponent<BoxCollider>().center = new Vector3(GetComponent<BoxCollider>().center.x, crouchedHeightCenterOriginal, GetComponent<BoxCollider>().center.z);
                Movement(speedWalk);
            }
        }


    }

    private void CrouchingMovement()
    {
        if (isNotCrouching == true)
        {
            isNotCrouching = false;
 
        }
        else
        {
            isNotCrouching = true;
        }
    }

    private void Walking()
    {
        Movement(speedWalk);
    }
    private void Running()
    {
        if (Input.GetKey(KeyCode.LeftShift) && isNotCrouching == true)
        {
            Movement(runSpeed);
        }
    }

    //Camara
    private void HandleMouseLook()
    {
        if (Time.timeScale == 1)
        {
            rotationX -= Input.GetAxis("Mouse Y") * lookSpeedY;
            rotationX = Mathf.Clamp(rotationX, -uperLookLimit, lowerLookLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * LookSpeedX, 0);
        }
    }

    //Zoom
    private void HandleZoom()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (zoomRoutine != null)
            {
                StopCoroutine(zoomRoutine);
                zoomRoutine = null;
            }
            zoomRoutine = StartCoroutine(ToggleZoom(true));
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            if (zoomRoutine != null)
            {
                StopCoroutine(zoomRoutine);
                zoomRoutine = null;
            }
            zoomRoutine = StartCoroutine(ToggleZoom(false));
        }
    }

    public void Movement(float speed)
    {
        z = Input.GetAxis("Vertical");
        x = Input.GetAxis("Horizontal");

        Vector3 dir = (transform.forward * z) + (transform.right * x);
        Vector3 dirSpeed = dir * (speed);
        _rb.velocity = dirSpeed;
        dirSpeed.y = _rb.velocity.y;
        dir.y = 0;
    }
    private IEnumerator ToggleZoom(bool isEnter)
    {
        float targetFOV = isEnter ? zoomFOV : defaultFOV;
        float startingFOV = playerCamera.fieldOfView;
        float timeElapsed = 0;

        while (timeElapsed < startingFOV)
        {
            playerCamera.fieldOfView = Mathf.Lerp(startingFOV, targetFOV, timeElapsed / timeToZoom);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        playerCamera.fieldOfView = targetFOV;
        zoomRoutine = null;
    }

}