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
    [SerializeField] private float height, crouchedSpeed, crouchedHeightSizeOriginal, crouchedHeightCenterOriginal,crouchedHeightSizeNew, crouchedHeightCenterNew;

    public GameObject cameraCrouched;

    private Camera playerCamera;
    private Rigidbody _rb;
    private Vector3 moveDirection;
    private Vector2 currentInput;
    private float rotationX, speed, x, z;

    public float Speed { get => speed; set => speed = value; }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        playerCamera = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        defaultFOV = playerCamera.fieldOfView;
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
        if (Input.GetKey(KeyCode.LeftControl))
        {
            cameraCrouched.SetActive(true);
            playerCamera.gameObject.SetActive(false);
            GetComponent<BoxCollider>().size = new Vector3(GetComponent<BoxCollider>().size.x, crouchedHeightSizeNew, GetComponent<BoxCollider>().size.z);
            GetComponent<BoxCollider>().center = new Vector3(GetComponent<BoxCollider>().center.x, crouchedHeightCenterNew, GetComponent<BoxCollider>().center.z);
            Speed = crouchedSpeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl)) //&& isNotHide)
        {
            //pararse
            cameraCrouched.SetActive(false);
            playerCamera.gameObject.SetActive(true);
            GetComponent<BoxCollider>().size = new Vector3(GetComponent<BoxCollider>().size.x, crouchedHeightSizeOriginal, GetComponent<BoxCollider>().size.z);
            GetComponent<BoxCollider>().center = new Vector3(GetComponent<BoxCollider>().center.x, crouchedHeightCenterOriginal, GetComponent<BoxCollider>().center.z);
        }

    }
    private void Walking()
    {
        z = Input.GetAxis("Vertical");

        Vector3 dir = transform.forward * z;
        Vector3 dirSpeed = dir * (Speed);
        _rb.velocity = dirSpeed;

        Speed = speedWalk;
        dirSpeed.y = _rb.velocity.y;
        dir.y = 0;
    }
    private void Running()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Speed = runSpeed;
        }
    }

    //Camara
    private void HandleMouseLook()
    {
        rotationX -= Input.GetAxis("Mouse Y") * lookSpeedY;
        rotationX = Mathf.Clamp(rotationX, -uperLookLimit, lowerLookLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeedX, 0);
    }

    //Zoom
    private void HandleZoom()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (zoomRoutine != null)
            {
                StopCoroutine(zoomRoutine);
                zoomRoutine = null;
            }
            zoomRoutine = StartCoroutine(ToggleZoom(true));
        }
        if (Input.GetMouseButtonUp(1))
        {
            if (zoomRoutine != null)
            {
                StopCoroutine(zoomRoutine);
                zoomRoutine = null;
            }
            zoomRoutine = StartCoroutine(ToggleZoom(false));
        }
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