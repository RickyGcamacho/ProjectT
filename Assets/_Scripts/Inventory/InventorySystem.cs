using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance;
    public List<InventoryItem> inventoryPocket, inventoryNotes;
    public delegate void onInventoryChangedEvent();
    public event onInventoryChangedEvent onInventoryChangedEventCallback;

    private Dictionary<InventoryItemData, Tipo> itemDictionary;

    private void Awake()
    {
        inventoryPocket = new List<InventoryItem>();
        inventoryNotes = new List<InventoryItem>();
        itemDictionary = new Dictionary<InventoryItemData, Tipo> ();

        Instance = this;
    }
 

    public void Add(InventoryItemData itemData)
    {
        switch (itemData.tipo)
        {
            case Tipo.Saveables:
                Debug.Log($"Agregando nuevo ítem: {itemData.itemName}");

                // Crear una nueva instancia del ítem cada vez que se agrega
                InventoryItem newItem = new InventoryItem(itemData);

                // Agregar el ítem a la lista de inventario
                inventoryPocket.Add(newItem);

                // Invocar el evento para actualizar otros sistemas (como la UI)
                onInventoryChangedEventCallback?.Invoke();
                break;
                case Tipo.Notes:
                Debug.Log($"Agregando nuevo ítem: {itemData.itemName}");

                // Crear una nueva instancia del ítem cada vez que se agrega
                InventoryItem newItemNotes = new InventoryItem(itemData);

                // Agregar el ítem a la lista de inventario
                inventoryNotes.Add(newItemNotes);

                // Invocar el evento para actualizar otros sistemas (como la UI)
                onInventoryChangedEventCallback?.Invoke();
                break;
            default:
                break;
        }
      

    }

    public void Remove(InventoryItemData itemData)
    {
        // Buscar el primer ítem en la lista que coincida con los datos
        InventoryItem itemToRemovePocket = inventoryPocket.Find(item => item.data == itemData);
        InventoryItem itemToRemoveNotes = inventoryNotes.Find(item => item.data == itemData);
        if (itemToRemovePocket != null || itemToRemoveNotes != null)
        {
            switch (itemData.tipo)
            {
                case Tipo.Saveables:
                inventoryPocket.Remove(itemToRemovePocket);
                Debug.Log($"Eliminado ítem: {itemData.itemName}");
                onInventoryChangedEventCallback?.Invoke();
                break;
            case Tipo.Notes:
                inventoryNotes.Remove(itemToRemoveNotes);
                Debug.Log($"Eliminado ítem: {itemData.itemName}");
                onInventoryChangedEventCallback?.Invoke();
                break;
            default:
                Debug.LogWarning("No se encontró el ítem para eliminar.");
                break;
            }

        }
    }
}
