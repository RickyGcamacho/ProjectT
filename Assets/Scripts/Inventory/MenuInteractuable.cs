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
    public TextMeshProUGUI textName;
    
    private PlayerActions playerActions;
    private ItemObject item;
    private bool isSelected;
    [SerializeField]private GameObject inventory;
    private void Start()
    {
        menuInspection.SetActive(false);
        playerActions = GameObject.Find("Player").GetComponent<PlayerActions>();


    }
    private void Update()
    {
        LooKMenu();
    }

    void LooKMenu()
    {
        if (Input.GetMouseButtonDown(0))
        {

            //perform raycast to check if player is looking at object within pickuprange
            RaycastHit hit;
            if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit,20f))
            {
                if (hit.transform.gameObject.tag == "Interactuable" && isSelected == false)
                {
                    MenuInteraction();
                    item = hit.transform.gameObject.GetComponent<ItemObject>();
                    isSelected = true;
                    
                }
            }

        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (item.gameObject.active == true)
            {
                item.transform.position = new Vector3(item.transform.position.x, 0, item.transform.position.z);
                isSelected = false;
                mainCamera.gameObject.SetActive(true);
            
            }
            Time.timeScale = 1;
        }
        CursorLook();
    }

    public void Save()
    {
        if (inventory.transform.childCount <= 2)
        {
            menuInspection.SetActive(false);
            item.OnHandlePickUp();
            isSelected = false;
          
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
    }

    void CursorLook()
    {
        if (menuInspection.activeSelf == true)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
        }
    }
    private void MenuInteraction()
    {
        menuInspection.SetActive(true);
        if (menuInspection.gameObject.active == true)
        {
            
        }

    }
}
