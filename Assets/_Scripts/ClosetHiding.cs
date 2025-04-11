using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosetHiding : MonoBehaviour
{
    public Transform hidePoint;
    public GameObject interactionUI; // UI que dice [E] para esconderse
    private bool isPlayerNearby = false;
    private bool isHiding = false;
    private GameObject player;
    private Camera playerCam;

    public bool IsHiding { get => isHiding; set => isHiding = value; }

    void Start()
    {
       interactionUI.SetActive(false); // Ocultar UI al inicio
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            if (!IsHiding)
            {
                EnterCloset();
            }
            else
            {
                ExitCloset();
            }
        }
    }

    void EnterCloset()
    {
        player.transform.position = hidePoint.position;
        player.transform.rotation = hidePoint.rotation;
        IsHiding = true;

        // Desactivar movimiento del jugador y cámara
        player.GetComponent<PlayerActions>().enabled = false;
        

        interactionUI.SetActive(false);
    }

    void ExitCloset()
    {
        player.transform.position = transform.position + transform.forward * 1.5f;
        IsHiding = false;

        player.GetComponent<PlayerActions>().enabled = true;
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            player = other.gameObject;
            playerCam = player.GetComponentInChildren<Camera>();

            interactionUI.SetActive(true); // Mostrar el cartel
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            interactionUI.SetActive(false);
        }
    }
}
