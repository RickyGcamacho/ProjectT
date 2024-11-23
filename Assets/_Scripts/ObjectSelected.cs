using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectSelected : MonoBehaviour
{
    
    public Camera _mainCam;
    public GameObject menuInspection,socket;
    public GameObject pointerStandard,pointerHand;
    
    private ItemObject item;
    private ObjectsSwitchable objSwitch;

    [SerializeField]private GameObject inventory;
    [SerializeField] private ObjectHandling _objHandling;
  
    private Transform highlight;
    private RaycastHit _raycastHit;
    private PlayerActions _playerActions;
    [SerializeField] private float _rayDistance = 5;

    private Outline outline;
    [SerializeField, Range(0f, 10f)]
    private float outlineWidth = 2f;
    private bool outlineAdded;
    private bool inMenu;
    private bool isPickUp;
    private bool isHolding;

    public bool IsPickUp { get => isPickUp; private set => isPickUp = value; }

    private void Start()
    {
        menuInspection.SetActive(false);
        SetPointer(true, false);
        _playerActions = GameObject.Find("Player_Agus").GetComponent<PlayerActions>();

        item = GameObject.FindWithTag("Saveables").GetComponent<ItemObject>();
 
    }
    private void Update()
    {
       
        if (!inMenu && !isPickUp)
        {
            CheckForHover();
        }
     
        if (Input.GetMouseButtonDown(0) && highlight != null) 
        {
            if (highlight.CompareTag("Saveables"))
            {
                if (inventory.transform.childCount <= 3)
                {
                    item.OnHandlePickUp();
                }
            }

            if (highlight.CompareTag("Pickup"))
            {
                if (!isPickUp)
                {
                    Pickup();
                }
                else
                {
                    isHolding = true;
                }

              
            }



            //if (highlight.CompareTag("Toggle"))
            //{

            //}

        }
        if (highlight != null && isPickUp)
        {
            _objHandling.MoveObject();
            if (Input.GetMouseButtonDown(0) && isHolding)
            {
                _playerActions.GetComponent<Rigidbody>().isKinematic = true;
            }else if (Input.GetMouseButtonUp(0) && isHolding)
            {
                _objHandling.ThrowObject();
                ResetPick();
                _playerActions.GetComponent<Rigidbody>().isKinematic = false;
            }

              
            
  

        }
        

    }
    private void ResetPick()
    {
        isHolding = false; // Resetear el estado de "sosteniendo"
        _objHandling.StopClipping();
        highlight = null;
        isPickUp = false;
    }
   

    private void Pickup()
    {
        SetPointer(true, false);
        isPickUp = true;
        _objHandling.SetHeldObj(highlight.gameObject);
        _objHandling.PickUpObject();
    }
    public void SetPointer(bool standard, bool hand)
    {
        pointerStandard.SetActive(standard);
        pointerHand.SetActive(hand);
    }
    public void CheckForHover()
    {
        Ray ray = _mainCam.ScreenPointToRay(Input.mousePosition);
        print(_raycastHit.transform);
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out _raycastHit, _rayDistance, LayerMask.GetMask("Hovering")))
        {
            highlight = _raycastHit.transform;
            SetPointer(false, true);

            if (highlight.CompareTag("Saveables") && !outlineAdded)
            {
                AddOutline();
            }
        }
        else
        {
            highlight = null;

            SetPointer(true, false);
            RemoveOutline();
        }
    }

    //TODO pasar esto a un solo script
    public void Save()
    {
    
    }

    public void Inspection()
    {
        menuInspection.SetActive(false);
       // item.transform.position = socket.transform.position;
        _mainCam.transform.rotation = Quaternion.Euler(Vector3.zero);
        inMenu = false;

    }

    public void ExitMenu()
    {
   
        menuInspection.SetActive(false);
        Cursor.visible = false; // Asegúrate de que sea visible.
        Cursor.lockState = CursorLockMode.Locked; // Asegúrate de que no esté bloqueado.
        inMenu = false;
        _playerActions.GetComponent<Rigidbody>().isKinematic = false;

    }

    //TODO pasar esto a un solo script
    private void AddOutline()
    {   
        if (!highlight.gameObject.TryGetComponent(out outline))
        {
            outline = highlight.gameObject.AddComponent<Outline>(); // Añadir el Outline si no existe
        }
        if (outline != null)
        {
            outline = highlight.gameObject.GetComponent<Outline>();
            outline.OutlineWidth = outlineWidth;
            outline.enabled = true; // Habilitar el Outline
            outlineAdded = true;
        }
    }
    private void RemoveOutline()
    {
      
        if (outlineAdded)
        {
            highlight = null;
            // Si el Outline existe, deshabilitarlo
            if (outline != null)
            {
                outline.enabled = false;
            }
            outlineAdded = false;
        }


    }
    private void MenuInteraction()
    {
        inMenu = true;
        SetPointer(true, false);
        highlight = _raycastHit.transform;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        menuInspection.SetActive(true);
        item = highlight.GetComponent<ItemObject>();
        _playerActions.GetComponent<Rigidbody>().isKinematic = true;
    }
}