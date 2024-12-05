using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance;
    public List<InventoryItem> inventory;
    public delegate void onInventoryChangedEvent();
    public event onInventoryChangedEvent onInventoryChangedEventCallback;

    private Dictionary<InventoryItemData, InventoryItem> itemDictionary;

    private void Awake()
    {
        inventory = new List<InventoryItem>();
        itemDictionary = new Dictionary<InventoryItemData, InventoryItem> ();

        Instance = this;
    }
 

    public void Add(InventoryItemData itemData)
    {
        Debug.Log($"Agregando nuevo ítem: {itemData.itemName}");

        // Crear una nueva instancia del ítem cada vez que se agrega
        InventoryItem newItem = new InventoryItem(itemData);

        // Agregar el ítem a la lista de inventario
        inventory.Add(newItem);

        // Invocar el evento para actualizar otros sistemas (como la UI)
        onInventoryChangedEventCallback?.Invoke();
    }

    public void Remove(InventoryItemData itemData)
    {
        // Buscar el primer ítem en la lista que coincida con los datos
        InventoryItem itemToRemove = inventory.Find(item => item.data == itemData);

        if (itemToRemove != null)
        {
            inventory.Remove(itemToRemove);
            Debug.Log($"Eliminado ítem: {itemData.itemName}");
            onInventoryChangedEventCallback?.Invoke();
        }
        else
        {
            Debug.LogWarning("No se encontró el ítem para eliminar.");
        }
    }
}
