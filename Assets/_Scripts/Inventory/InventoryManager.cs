using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public ItemCarousel itemCarousel;
    public Button buttonPocket, buttonNotes, buttonCollectables;
    public GameObject inventory,pocket,collectables,notes;
    [SerializeField] private bool see;
    private PlayerActions playerActions;
    // Start is called before the first frame update
    void Start()
    {
        pocket.SetActive(true);
        notes.SetActive(false);
        collectables.SetActive(false);
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
                OpenInventory();


            }
            else
            {
                CloseInventory();
            }
        }
       
    }

    private void OpenInventory()
    {
        inventory.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        see = true;
        playerActions.GetComponent<Rigidbody>().isKinematic = true;
    }
    public void CloseInventory()
    {
        inventory.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked; // Asegúrate de que no esté bloqueado.
        Cursor.visible = false; // Asegúrate de que sea visible.
        see = false;
        playerActions.GetComponent<Rigidbody>().isKinematic = false;
    }
    public void ActiveInventoryPocket()
    {
        itemCarousel.SwitchToPocket();
        pocket.SetActive(true);
        notes.SetActive(false);
        collectables.SetActive(false);
    }
    public void ActiveInventoryNotes()
    {
        itemCarousel.SwitchToNotes();
        pocket.SetActive(false);
        notes.SetActive(true);
        collectables.SetActive(false);
    }
    public void ActiveInventoryCollectables()
    {
        itemCarousel.SwitchToCollectables();
        pocket.SetActive(false);
        notes.SetActive(false);
        collectables.SetActive(true);
    }

}
