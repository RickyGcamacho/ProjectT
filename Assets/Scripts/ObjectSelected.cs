using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectSelected : MonoBehaviour
{
    
    public Camera _mainCam;
    public GameObject menuInspection,socket;
    public GameObject pointerStandard,pointerHand;
    
    private ItemObject item;

    [SerializeField]private GameObject inventory;
    [SerializeField] private ObjectHandling _objHandling;
  
    private Transform highlight;
    private RaycastHit _raycastHit;
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
    }
    private void Update()
    {
       
        if (!inMenu)
        {
            CheckForHover();
        }
     
        if (Input.GetMouseButtonDown(0) && highlight != null) 
        {
            if (highlight.CompareTag("Saveables"))
            {
                MenuInteraction();
            }

            if (highlight.CompareTag("Pickup") && !isPickUp)
            {            
                Pickup();
            }
            

        }
        if (isPickUp && highlight != null)
        {
            _objHandling.MoveObject();
            if (Input.GetMouseButtonDown(0)) // El clic izquierdo está presionado
            {
                isHolding = true;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (isHolding)
                {
                    _objHandling.ThrowObject();
                    ResetPick();
                }
            }
            if (Input.GetMouseButtonDown(1)) // Clic derecho para dejar el objeto
            {
                _objHandling.DropObject();
                ResetPick();
            }

        }

    }
    private void ResetPick()
    {
        isHolding = false; // Resetear el estado de "sosteniendo"
        isPickUp = false;
        _objHandling.StopClipping();
        highlight = null;
    }
   
    private void Pickup()
    {
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
            SetPointer(true, false);
            RemoveOutline();
        }
    }

    public void Save()
    {
        if (inventory.transform.childCount <= 2)
        {
            menuInspection.SetActive(false);
            item.OnHandlePickUp();
            Cursor.lockState = CursorLockMode.Locked; // Asegúrate de que no esté bloqueado.
            Cursor.visible = false; // Asegúrate de que sea visible.

        }
    }

    public void Inspection()
    {
        menuInspection.SetActive(false);
        item.transform.position = socket.transform.position;
        _mainCam.transform.rotation = Quaternion.Euler(Vector3.zero);

    }

    public void ExitMenu()
    {
        inMenu = false;
        menuInspection.SetActive(false);
        Cursor.visible = false; // Asegúrate de que sea visible.
        Cursor.lockState = CursorLockMode.Locked; // Asegúrate de que no esté bloqueado.

    }

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
    }
}
