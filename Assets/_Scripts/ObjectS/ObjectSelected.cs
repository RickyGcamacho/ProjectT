using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ObjectSelected : MonoBehaviour
{
    
    public Camera _mainCam;
    public GameObject menuInspection,socket;
    public GameObject pointerStandard,pointerHand;
    
    private ItemObject item;
    private ObjectsSwitchableSound objSwitch;

    [SerializeField]private GameObject inventoryPocket,inventoryNotes, inventoryCollectables;
    [SerializeField]private EventSystem _eventSystem;
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
        _playerActions = GameObject.FindObjectOfType<PlayerActions>();

    }
    private void Update()
    {
       
        if (!inMenu && !isPickUp)
        {
            CheckForHover();
        }
     
        if (Input.GetMouseButtonDown(0) && highlight != null) 
        {
            switch (highlight.GetComponent<ItemObject>().itemData.tipo)
            {
                case Tipo.Saveables:
                    if (inventoryPocket.transform.childCount <= 3)
                    {
                        ItemObject currentItem = highlight.gameObject.GetComponent<ItemObject>();
                        currentItem.OnHandlePickUp();

                    }
                    break;
                case Tipo.Notes:
                    if (inventoryNotes.transform.childCount <= 3)
                    {
                        ItemObject currentItem = highlight.gameObject.GetComponent<ItemObject>();
                        currentItem.OnHandlePickUp();

                    }
                    break;
                case Tipo.Collectables:
                    if (inventoryCollectables.transform.childCount <= 3)
                    {
                        ItemObject currentItem = highlight.gameObject.GetComponent<ItemObject>();
                        currentItem.OnHandlePickUp();

                    }
                    break;
                case Tipo.Throw:
                    if (!isPickUp)
                    {
                        Pickup();
                    }
                    else
                    {
                        isHolding = true;
                    }
                    break;
                default:
                    break;
            }
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
   

    public void Pickup()
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
        if (!_eventSystem.IsPointerOverGameObject() && Physics.Raycast(ray, out _raycastHit, _rayDistance, LayerMask.GetMask("Hovering")))
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
            if (outline != null)
            {
                outline.enabled = false;
            }
            outlineAdded = false;
        }
    }

}
