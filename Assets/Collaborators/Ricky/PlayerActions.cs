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
    [SerializeField] private float timeToZoom,zoomFOV;
    private float defaultFOV;

    [Header("Run Parameters")]
    [SerializeField] private float runSpeed;

    [Header("Parameters")]
    [SerializeField] private float height, crouchedHeight, crouchedSpeed;

    private Camera playerCamera;
    private Vector3 moveDirection;
    private Vector2 currentInput;
    private float rotationX,speed,x, y;

    public float Speed { get => speed; set => speed = value; }

    private void Awake()
    {
        playerCamera = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        defaultFOV = playerCamera.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        HandleMouseLook();
        HandleZoom();
 
    }

    //Movimiento Player
    void Movement()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        transform.Translate(Vector3.forward * y * Speed * Time.deltaTime);
        height = transform.position.y;
        Speed = speedWalk;
        if (Input.GetKey(KeyCode.LeftControl))
        {
           height = crouchedHeight;
           Speed = crouchedSpeed;
        }

        transform.position = new Vector3(transform.position.x,height,transform.position.z);

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
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX,0,0);
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
