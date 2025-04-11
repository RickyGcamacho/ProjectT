using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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
    [SerializeField] private float runSpeed, stamina, maxStamina, staminaDrain, staminaRegen;
    private bool isRunning = false;

    [Header("Parameters")]
    [SerializeField] private float height, crouchedSpeed, crouchedHeightSizeOriginal, crouchedHeightCenterOriginal, crouchedHeightSizeNew, crouchedHeightCenterNew;


    public bool isNotCrouching;

    private Camera playerCamera;
    private GameManager gameManager;
    private ItemCarousel carrousel;
    private ClosetHiding closetHiding;
    private BoxCollider boxColiderPlayer;
    private Rigidbody _rb;
    private ItemCarousel carousel;
    private Vector3 moveDirection;
    private Vector2 currentInput;
    private float rotationX, x, z, crouchingHeight, standingHeight;
    private bool isTableUnder;

    public float LookSpeedX { get => lookSpeedX; set => lookSpeedX = value; }
    public bool IsTableUnder { get => isTableUnder; set => isTableUnder = value; }
    public float SpeedWalk { get => speedWalk; set => speedWalk = value; }

    private void Awake()
    {
        boxColiderPlayer = GetComponent<BoxCollider>();
        _rb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        closetHiding = GameObject.FindGameObjectWithTag("Locker").GetComponent<ClosetHiding>();
        playerCamera = GetComponentInChildren<Camera>();
        carousel = GameObject.FindObjectOfType<ItemCarousel>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        defaultFOV = playerCamera.fieldOfView;
        isNotCrouching = true;
        crouchingHeight = 0.2f;
        stamina = 100f;
        maxStamina = 100f;
        staminaDrain = 20f;
        staminaRegen = 10f;
        standingHeight = playerCamera.transform.position.y;
        carrousel = GameObject.FindObjectOfType<ItemCarousel>();
    }

    void Update()
    {
        Walking();
        Running();
        Crouching();

        HandleMouseLook();
        HandleZoom();

    }

    private void Crouching()
    {

        if (Input.GetKeyDown(KeyCode.LeftControl) && !IsTableUnder)
        {
            CrouchingMovement();
            if (isNotCrouching == false)
            {
                ///TODO: mover la posicion de la camara
                boxColiderPlayer.size = new Vector3(boxColiderPlayer.size.x, crouchedHeightSizeNew, boxColiderPlayer.size.z);
                // boxColiderPlayer.center = new Vector3(boxColiderPlayer.center.x, crouchedHeightCenterNew, boxColiderPlayer.center.z);
                playerCamera.transform.position = new Vector3(playerCamera.transform.position.x, crouchingHeight, playerCamera.transform.position.z);
                Movement(crouchedSpeed);
            }
            else
            {
                ///TODO: mover la posicion de la camara
                //pararse
                boxColiderPlayer.size = new Vector3(boxColiderPlayer.size.x, crouchedHeightSizeOriginal, boxColiderPlayer.size.z);
                //boxColiderPlayer.center = new Vector3(boxColiderPlayer.center.x, crouchedHeightCenterOriginal, boxColiderPlayer.center.z);
                playerCamera.transform.position = new Vector3(playerCamera.transform.position.x, standingHeight, playerCamera.transform.position.z);
                Movement(SpeedWalk);
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
        Movement(SpeedWalk);
    }
    private void Running()
    {
        if (Input.GetKey(KeyCode.LeftShift) && isNotCrouching == true && stamina > 0)
        {
            isRunning = true;
            stamina -= staminaDrain * Time.deltaTime;
            stamina = Mathf.Clamp(stamina, 0, maxStamina);
        }
        else
        {
            isRunning = false;
            stamina += staminaRegen * Time.deltaTime;
            stamina = Mathf.Clamp(stamina, 0, maxStamina);
        }

        if (isRunning == true)
        {
            Movement(runSpeed);
        }
        else
        {
            Movement(SpeedWalk);
        }
    }

    //Camara
    private void HandleMouseLook()
    {
        if (gameManager.GetComponent<DialogueManager>().DialogueUI.activeSelf == false && carousel.Inspection == false)
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
        if (carrousel.Inspection != true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                if (zoomRoutine != null)
                {
                    StopCoroutine(zoomRoutine);
                    zoomRoutine = null;
                }
                zoomRoutine = StartCoroutine(ToggleZoom(true));
            }
            if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                if (zoomRoutine != null)
                {
                    StopCoroutine(zoomRoutine);
                    zoomRoutine = null;
                }
                zoomRoutine = StartCoroutine(ToggleZoom(false));
            }
        }

    }

    public void Movement(float speed)
    {
        z = Input.GetAxis("Vertical");
        x = Input.GetAxis("Horizontal");

        if (gameManager.GetComponent<DialogueManager>().DialogueUI.activeSelf == false && carousel.Inspection == false)
        {


            Vector3 dir = (transform.forward * z) + (transform.right * x);
            Vector3 dirSpeed = dir * (speed);
            _rb.velocity = dirSpeed;
            dirSpeed.y = _rb.velocity.y;
            dir.y = 0;
        }
        else
        {
            speed = 0;
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
