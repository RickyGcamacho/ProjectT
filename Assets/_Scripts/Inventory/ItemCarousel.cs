using System;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR;
using static UnityEditor.Progress;

public class ItemCarousel : MonoBehaviour
{
    public PlayerActions playerAction;
    public ObjectHandling objectEquip;
    public Item3DView item3DView;
    public RectTransform contentPanelPocket, contentPanelNotes, contentPanelCollectables;
    public InventorySystem inventorySystem;
    public Item3DView itemView;
    public Text itemNamePocket, itemDescriptionPocket, itemNameNotes, itemDescriptionNotes, itemNameCollectables, itemDescriptionCollectables;
    public Button inspectButtonPocket, equipButtonPocket, dropButtonPocket, inspectButtonNotes, inspectButtonCollectables, equipButtonCollectables;
    public InventoryManager inventoryManager;
    public GameObject information, tapaPocket, tapaNotes, tapaCollectables, inspectionCamera;

    [SerializeField] private GameObject itemContainer;
    private InventoryItemData clickedItem;
    private Turn currentInspectedObject;
    private float distanceDrop = 3;
    private int currentIndex = 0;
    private const int visibleItems = 3;
    private bool inspection = false;

    private RectTransform activePanel; // Panel activo

    public bool Inspection { get => inspection; set => inspection = value; }

    void Start()
    {
        activePanel = contentPanelPocket; // Empezamos con la pestaña "Pocket"
        UpdateCarousel();
        tapaPocket.SetActive(false);
        tapaNotes.SetActive(false);
        tapaCollectables.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveUp();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveDown();
        }

        objectEquip.DropObject();
        Debug.Log(activePanel.name);
    }

    public void MoveUp()
    {
        int itemCount = activePanel.childCount;

        currentIndex = (currentIndex - 1 + itemCount) % itemCount;

       UpdateCarousel();
    }

    public void MoveDown()
    {
        int itemCount = activePanel.childCount;

        currentIndex = (currentIndex + 1) % itemCount;

        UpdateCarousel();
    }

    private int UpdateCarousel()
    {
        int itemCount = activePanel.childCount;
        if (itemCount == 0) return -1;

        int indexReturn = 0;

        for (int i = 0; i < itemCount; i++)
        {
            Transform item = activePanel.GetChild(i);
            Image itemImage = item.GetComponent<Image>();

            bool isSelected = (i == currentIndex);
            item.gameObject.SetActive(IsVisible(i, itemCount));
            item.localScale = isSelected ? Vector3.one * 1.2f : Vector3.one;

            if (itemImage != null)
                itemImage.color = isSelected ? Color.yellow : Color.white;

            if (isSelected)
            {
                Information(i);
                indexReturn = i;
                information.SetActive(true);
                tapaPocket.SetActive(false);
                tapaNotes.SetActive(false);
                tapaCollectables.SetActive(false);
            }
        }
        return indexReturn;
    }

    private void Information(int index)
    {
        if (index < 0) return;

        InventoryItemData itemData;
        if (activePanel == contentPanelPocket)
        {
            itemData = inventorySystem.inventoryPocket[index].data;
        }
        else if (activePanel == contentPanelNotes)
        {
            itemData = inventorySystem.inventoryNotes[index].data;
        }
        else
        {
            itemData = inventorySystem.inventoryCollectables[index].data;
        }

        switch (itemData.tipo)
        {
            case Tipo.Saveables:
                itemNamePocket.text = itemData.name;
                itemDescriptionPocket.text = itemData.itemDescription;
                itemView.ItemView(itemData);
                break;
            case Tipo.Notes:
                itemNameNotes.text = itemData.name;
                itemDescriptionNotes.text = itemData.itemDescription;
                itemView.ItemView(itemData);
                break;
            case Tipo.Collectables:
                itemNameCollectables.text = itemData.name;
                itemDescriptionCollectables.text = itemData.itemDescription;
                itemView.ItemView(itemData);
                break;
        }
        
        GameObject.FindObjectOfType<Turn>().inInventory = true;


    }

    private bool IsVisible(int itemIndex, int itemCount)
    {
        int offsetIndex = (itemIndex - currentIndex + itemCount) % itemCount;
        return offsetIndex >= 0 && offsetIndex < visibleItems;
    }

    public void AddItem(GameObject newItem, Tipo itemType)
    {
        Transform targetPanel = GetPanelByType(itemType);

        newItem.transform.SetParent(targetPanel);
        newItem.transform.localScale = Vector3.one;

        if (targetPanel.childCount == 1)
        {
            currentIndex = 0;
            UpdateCarousel();
        }
    }

    private RectTransform GetPanelByType(Tipo itemType)
    {
        switch (itemType)
        {
            case Tipo.Notes:
                return contentPanelNotes;
            case Tipo.Collectables:
                return contentPanelCollectables;
            default:
                return contentPanelPocket;
        }
    }

    public void SwitchToPocket()
    {
        currentIndex = 0;
        activePanel = contentPanelPocket;
        tapaPocket.SetActive(true);
        tapaNotes.SetActive(true);
        tapaCollectables.SetActive(true);
        UpdateCarousel();
    }

    public void SwitchToNotes()
    {
        currentIndex = 0;
        activePanel = contentPanelNotes;
        tapaPocket.SetActive(true);
        tapaNotes.SetActive(true);
        tapaCollectables.SetActive(true);
        UpdateCarousel();
    }

    public void SwitchToCollectables()
    {
        currentIndex = 0;
        activePanel = contentPanelCollectables;
        tapaPocket.SetActive(true);
        tapaNotes.SetActive(true);
        tapaCollectables.SetActive(true);
        UpdateCarousel();
    }

    public void ButtonClicked()
    {
        int itemIndex = UpdateCarousel();
        if (itemIndex >= 0 && itemIndex < inventorySystem.inventoryPocket.Count || itemIndex < inventorySystem.inventoryNotes.Count || itemIndex < inventorySystem.inventoryCollectables.Count)
        {

            dropButtonPocket.onClick.AddListener(() => HandleButtonClick("ButtonDrop", itemIndex));
            equipButtonPocket.onClick.AddListener(() => HandleButtonClick("ButtonEquip", itemIndex));
            inspectButtonPocket.onClick.AddListener(() => HandleButtonClick("ButtonInspect", itemIndex));
            inspectButtonNotes.onClick.AddListener(() => HandleButtonClick("ButtonInspect", itemIndex));
            equipButtonCollectables.onClick.AddListener(() => HandleButtonClick("ButtonEquip", itemIndex));
            inspectButtonCollectables.onClick.AddListener(() => HandleButtonClick("ButtonInspect", itemIndex));
        }
        else
        {
            Debug.LogError("Índice de ítem inválido.");
        }
    }

    void HandleButtonClick(string buttonName, int itemIndex)
    {
        itemIndex = UpdateCarousel();
        if (itemIndex >= 0 && itemIndex < inventorySystem.inventoryPocket.Count || itemIndex < inventorySystem.inventoryNotes.Count || itemIndex < inventorySystem.inventoryCollectables.Count)
        {

            if (activePanel == contentPanelPocket)
                clickedItem = inventorySystem.inventoryPocket[itemIndex].data;
            else if (activePanel == contentPanelNotes)
                clickedItem = inventorySystem.inventoryNotes[itemIndex].data;
            else if (activePanel == contentPanelCollectables)
                clickedItem = inventorySystem.inventoryCollectables[itemIndex].data;

            // Lógica específica según el botón
            if (buttonName == "ButtonDrop")
            {
                inventorySystem.Remove(clickedItem);
                GameObject objecto = Instantiate(clickedItem.worldPrefab, new Vector3(playerAction.transform.position.x, 0, (playerAction.transform.position.z + distanceDrop)), Quaternion.Euler(-90, 0, 0));
                objecto.GetComponent<Turn>().inInventory = false;
                information.SetActive(false);
                tapaPocket.SetActive(true);
                tapaCollectables.SetActive(true);
                tapaNotes.SetActive(true);
                inventoryManager.CloseInventory();
            }
            else if (buttonName == "ButtonEquip")
            {
                SpawnObject(clickedItem);
            }
            else if (buttonName == "ButtonInspect")
            {
                inspection = true;
                GameObject spawnedObject = SpawnObject(clickedItem); // Ahora devuelve el objeto instanciado
               
                if (spawnedObject != null)
                {
                    Turn turnComponent = spawnedObject.GetComponent<Turn>();
                    if (turnComponent != null)
                    {
                        InspectObject(turnComponent);
                        
                    }
                    else
                    {
                        Debug.LogError("❌ No se encontró el componente 'Turn' en el objeto instanciado.");
                    }
                    
                }
                else
                {
                    Debug.LogError("❌ No se pudo instanciar el objeto.");
                }
            }

        }
        else
        {
            Debug.LogError("Índice de ítem inválido.");
        }
    }

    private GameObject SpawnObject(InventoryItemData gameObject)
{
        if (gameObject.worldPrefab != null && itemContainer != null)
        {
            foreach (Transform child in itemContainer.transform)
            {
                Destroy(child.gameObject);
            }
            GameObject hand;
            if (gameObject.id == "Key_Duchas" || gameObject.id == "Key_Celdas" || gameObject.id == "Key_Administracion")
            {
                hand = Instantiate(gameObject.worldPrefab, itemContainer.transform.position, Quaternion.Euler(-90, 0, 0));
            }
            else
            {
                hand = Instantiate(gameObject.worldPrefab, itemContainer.transform.position, Quaternion.Euler(-90, -135, 0));
            }
        if (hand != null)
        {
            hand.transform.SetParent(itemContainer.transform);
            objectEquip.SetHeldObj(hand);
            objectEquip.PickUpObject();
            hand.GetComponent<Turn>().inInventory = false;
            hand.GetComponent<ItemObject>().isCatching = true;
            inventorySystem.Remove(gameObject);
            information.SetActive(false);
            tapaPocket.SetActive(true);
            tapaNotes.SetActive(true);
            tapaCollectables.SetActive(true);
            inventoryManager.CloseInventory();

            return hand;  // 🔥 Devolvemos el objeto instanciado
        }
        else
        {
            Debug.LogError("❌ Error al instanciar el objeto.");
        }
    }
    return null; // En caso de error, devolvemos null
}

    public void InspectObject(Turn newObject)
    {
        if (currentInspectedObject != null)
        {
            currentInspectedObject.SetInspectionState(false);
        }

        currentInspectedObject = newObject;
        currentInspectedObject.SetInspectionState(true);
        Inspection = true;
    }

    public void StopInspecting()
    {
        if (currentInspectedObject != null)
        {
            currentInspectedObject.SetInspectionState(false);
            currentInspectedObject = null;
        }
        Inspection = false;
    }
}
