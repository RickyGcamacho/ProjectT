using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ItemCarousel : MonoBehaviour
{
    public PlayerActions playerAction;
    public ObjectHandling objectEquip;
    public Item3DView item3DView;
    public RectTransform contentPanelPocket, contentPanelNotes, contentPanelCollectables; // Panel que contiene los ítems
    public InventorySystem inventorySystem;
    public Item3DView itemView;
    public Color highlightedColor = Color.yellow; // Color del ítem seleccionado
    public Color normalColor = Color.white;       // Color normal de los ítems
    public Text itemName, itemDescription;
    public Button inspectButton,equipButton,dropButton;

    [SerializeField] private GameObject itemContainer;
    private float distanceDrop = 3;
    private int currentIndex = 0; // Índice del ítem seleccionado
    private const int visibleItems = 3; // Siempre mostrar 3 ítems
    private Turn vueltas;

    void Start()
    {
        UpdateCarousel();
        Turn vueltas = GameObject.FindObjectOfType<Turn>();
        vueltas.enabled = false;
    }

    void Update()
    {
        // Navegación con teclas W y S
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveUp();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveDown();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            objectEquip.DropObject();
        }
    }

    public void MoveUp()
    {
        int itemCount = contentPanelPocket.childCount;

        // Si estamos en el primer ítem, saltamos al último
        currentIndex = (currentIndex - 1 + itemCount) % itemCount;

        UpdateCarousel();
    }

    public void MoveDown()
    {
        int itemCount = contentPanelPocket.childCount;

        // Si estamos en el último ítem, saltamos al primero
        currentIndex = (currentIndex + 1) % itemCount;

        UpdateCarousel();
    }

    private int UpdateCarousel()
    {
        int itemCount = contentPanelPocket.childCount;
        int indexReturn = 0;
        // Iterar sobre los ítems
        for (int i = 0; i < itemCount; i++)
        {
            Transform item = contentPanelPocket.GetChild(i);
            Image itemImage = item.GetComponent<Image>();

            // Determinar si el ítem está dentro del rango visible
            if (IsVisible(i, itemCount))
            {
                item.gameObject.SetActive(true); // Mostrar el ítem


                // Resaltar el ítem seleccionado
                if (i == currentIndex)
                {
                    //Debug.Log(inventorySystem.inventory[i].data.name);//Obtengo el dato del objeto correspondiente al indice
                    itemName.text = inventorySystem.inventory[i].data.name;
                    itemDescription.text = inventorySystem.inventory[i].data.itemDescription;
                    itemView.ItemView(inventorySystem.inventory[i].data);
                    vueltas.enabled = true;


                    item.localScale = Vector3.one * 1.2f; // Escalar el ítem seleccionado
                    if (itemImage != null)
                        itemImage.color = highlightedColor;
                    indexReturn = i;

                }
                else
                {
                    item.localScale = Vector3.one; // Normalizar escala
                    if (itemImage != null)
                        itemImage.color = normalColor;
                }
            }
            else
            {
                item.gameObject.SetActive(false); // Ocultar ítem
            }
        }
        return indexReturn;
    }

    private bool IsVisible(int itemIndex, int itemCount)
    {
        // Determinar si el ítem debe estar visible considerando el índice circular
        int offsetIndex = (itemIndex - currentIndex + itemCount) % itemCount;
        return offsetIndex >= 0 && offsetIndex < visibleItems;
    }

    public void AddItem(GameObject newItem)
    {
        // Agregar el ítem al contentPanel
        newItem.transform.SetParent(contentPanelPocket);
        newItem.transform.localScale = Vector3.one;

        // Si es el primer ítem, inicializar el carrusel
        if (contentPanelPocket.childCount == 1)
        {
            currentIndex = 0;
            UpdateCarousel();
        }
    }

    public void ButtonClicked()
    {
        int itemIndex = UpdateCarousel();
        if (itemIndex >= 0 && itemIndex < inventorySystem.inventory.Count)
        {
            // Limpiar listeners previos para evitar duplicaciones
            dropButton.onClick.RemoveAllListeners();
            equipButton.onClick.RemoveAllListeners();
            inspectButton.onClick.RemoveAllListeners();

            // Añadir los nuevos listeners
            dropButton.onClick.AddListener(() => HandleButtonClick("ButtonDrop", itemIndex));
            equipButton.onClick.AddListener(() => HandleButtonClick("ButtonEquip", itemIndex));
            inspectButton.onClick.AddListener(() => HandleButtonClick("ButtonInspect", itemIndex));
        }
        else
        {
            Debug.LogError("Índice de ítem inválido.");
        }
    }


void HandleButtonClick(string buttonName, int itemIndex)
    {

        if (itemIndex >= 0 && itemIndex < inventorySystem.inventory.Count)
        {
            InventoryItemData clickedItem = inventorySystem.inventory[itemIndex].data;
            //Debug.Log("Botón clickeado: " + buttonName);

            // Lógica específica según el botón
            if (buttonName == "ButtonDrop")
            {
                inventorySystem.Remove(clickedItem);
                Instantiate(clickedItem.worldPrefab, new Vector3(playerAction.transform.position.x, 0, (playerAction.transform.position.z + distanceDrop)), Quaternion.Euler(-90, 0, 0));
            }
            else if (buttonName == "ButtonEquip")
            {
                if (clickedItem.worldPrefab != null && itemContainer != null)
                {
                    // Eliminar cualquier objeto existente en el contenedor antes de instanciar el nuevo
                    foreach (Transform child in itemContainer.transform)
                    {
                        Debug.Log("Eliminando hijo existente: " + child.name);
                        Destroy(child.gameObject);
                    }

                    // Instanciar el nuevo objeto
                    GameObject hand = Instantiate(clickedItem.worldPrefab,itemContainer.transform.position,Quaternion.Euler(-90,-135, 0));

                    // Configurar el nuevo objeto como hijo del contenedor
                    if (hand != null)
                    {
                        Debug.Log("Instanciado nuevo objeto: " + hand.name);
                        hand.transform.SetParent(itemContainer.transform);
                    }
                    else
                    {
                        Debug.LogError("Error al instanciar el objeto.");
                    }
                    objectEquip.SetHeldObj(hand);
                    objectEquip.PickUpObject();
                    hand.GetComponent<ItemObject>().enabled = false;
                    inventorySystem.Remove(clickedItem);

                }
                else
                {
                    if (clickedItem.worldPrefab == null)
                        Debug.LogError("El prefab (worldPrefab) es null.");

                    if (itemContainer == null)
                        Debug.LogError("El contenedor (itemContainer) es null.");
                }
            }
            else if (buttonName == "ButtonInspect")
            {
                Debug.Log($"Inspeccionando: {clickedItem.itemName} - {clickedItem.itemDescription}");
            }
            Debug.Log(itemIndex);
        }
        else
        {
            Debug.LogError("Índice de ítem inválido.");
        }
    }
   
}