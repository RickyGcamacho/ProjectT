using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventory;
    [SerializeField] private bool see;
    private PlayerActions playerActions;
    // Start is called before the first frame update
    void Start()
    {
        inventory.SetActive(false);
        see = false;
        playerActions = GameObject.Find("Player_Agus").GetComponent<PlayerActions>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {

            if (see == false)
            {
                inventory.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
                see = true;
                playerActions.GetComponent<Rigidbody>().isKinematic = true;


            }
            else
            {
                inventory.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked; // Asegúrate de que no esté bloqueado.
                Cursor.visible = false; // Asegúrate de que sea visible.
                see = false;
                playerActions.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }
}
