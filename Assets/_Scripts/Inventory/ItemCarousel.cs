using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ItemCarousel : MonoBehaviour
{
    public RectTransform contentPanel; // Panel que contiene los ítems
    public InventorySystem inventorySystem;
    public Item3DView itemView;
    public Color highlightedColor = Color.yellow; // Color del ítem seleccionado
    public Color normalColor = Color.white;       // Color normal de los ítems
    public Text itemName, itemDescription;
    public Button inspectButton;
    public Button equipButton;
    public Button dropButton;

    private int currentIndex = 0; // Índice del ítem seleccionado
    private const int visibleItems = 3; // Siempre mostrar 3 ítems

    void Start()
    {
        UpdateCarousel();
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
    }

    public void MoveUp()
    {
        int itemCount = contentPanel.childCount;

        // Si estamos en el primer ítem, saltamos al último
        currentIndex = (currentIndex - 1 + itemCount) % itemCount;

        UpdateCarousel();
    }

    public void MoveDown()
    {
        int itemCount = contentPanel.childCount;

        // Si estamos en el último ítem, saltamos al primero
        currentIndex = (currentIndex + 1) % itemCount;

        UpdateCarousel();
    }

    private void UpdateCarousel()
    {
        int itemCount = contentPanel.childCount;

        // Iterar sobre los ítems
        for (int i = 0; i < itemCount; i++)
        {
            Transform item = contentPanel.GetChild(i);
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
                    ButtonClicked(i);

                    item.localScale = Vector3.one * 1.2f; // Escalar el ítem seleccionado
                    if (itemImage != null)
                        itemImage.color = highlightedColor;


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
        newItem.transform.SetParent(contentPanel);
        newItem.transform.localScale = Vector3.one;

        // Si es el primer ítem, inicializar el carrusel
        if (contentPanel.childCount == 1)
        {
            currentIndex = 0;
            UpdateCarousel();
        }
    }

    public void ButtonClicked(int itemIndex)
    {
        if (itemIndex >= 0 && itemIndex < inventorySystem.inventory.Count)
        {
            InventoryItemData clickedItem = inventorySystem.inventory[itemIndex].data;

            // Ejecutar acciones según el ítem clickeado
            Debug.Log($"Ítem seleccionado: {clickedItem.itemName}");

            // Opciones basadas en botones
            InspectItem(clickedItem);
            EquipItem(clickedItem);
            DropItem(clickedItem);
        }
        else
        {
            Debug.LogError("Índice de ítem inválido.");
        }
    }
    private void InspectItem(InventoryItemData item)
    {
        Debug.Log($"Inspeccionando: {item.itemName} - {item.itemDescription}");
    }

    private void EquipItem(InventoryItemData item)
    {
        Debug.Log($"Equipando: {item.itemName}");
    }

    private void DropItem(InventoryItemData item)
    {
        inventorySystem.Remove(item);
    }
}