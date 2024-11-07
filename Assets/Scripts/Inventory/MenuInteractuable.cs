using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MenuInteractuable : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject menuInspection,socket;
    public OutlineSelected outlineSelected;
    
    private ItemObject item;
    private bool isSelected;
    [SerializeField]private GameObject inventory;

    private void Start()
    {
        menuInspection.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {          
            LooKMenu();
        }
           
    }

    void LooKMenu()
    {
        //perform raycast to check if player is looking at object within pickuprange
        RaycastHit hit;
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, 20f))
        {
         
            if (hit.transform.gameObject.tag == "Selectable" && isSelected == false)
            {
                outlineSelected.interactMenuVisible = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
                MenuInteraction();
                item = hit.transform.gameObject.GetComponent<ItemObject>();
                isSelected = true;


            }
        }

     

    }

    public void Save()
    {
        if (inventory.transform.childCount <= 2)
        {
            menuInspection.SetActive(false);
            item.OnHandlePickUp();
            isSelected = false;
            outlineSelected.interactMenuVisible = false;
            Cursor.lockState = CursorLockMode.Locked; // Asegúrate de que no esté bloqueado.
            Cursor.visible = false; // Asegúrate de que sea visible.

        }
    }

    public void Inspection()
    {
        menuInspection.SetActive(false);
        if (isSelected == true)
        {
            item.transform.position = socket.transform.position;
            mainCamera.transform.rotation = Quaternion.Euler(Vector3.zero);
            
        }

    }

    public void ExitMenu()
    {
        menuInspection.SetActive(false);
        isSelected = false;
        outlineSelected.interactMenuVisible = false;
        Cursor.visible = false; // Asegúrate de que sea visible.
        Cursor.lockState = CursorLockMode.Locked; // Asegúrate de que no esté bloqueado.

    }

    private void MenuInteraction()
    {
        menuInspection.SetActive(true);
       
    }
}
