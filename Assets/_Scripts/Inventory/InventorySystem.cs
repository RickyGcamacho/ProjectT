using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance;
    public delegate void onInventoryChangedEvent();
    public event onInventoryChangedEvent onInventoryChangedEventCallback;
    public List<InventoryItem> inventory;

    private Dictionary<InventoryItemData, InventoryItem> itemDictionary;

    private void Awake()
    {
        inventory = new List<InventoryItem> ();
        itemDictionary = new Dictionary<InventoryItemData , InventoryItem> ();
        Instance = this;

    }

    public void Add(InventoryItemData itemData)
    {
      
            if (itemDictionary.TryGetValue(itemData, out InventoryItem value))
            {
                Debug.Log("Sumar Stack en item");
                value.AddStack();
                onInventoryChangedEventCallback.Invoke();
            }
            else
            {
                Debug.Log("Agregar un nuevo item");
                InventoryItem newItem = new InventoryItem(itemData);
                inventory.Add(newItem);
                itemDictionary.Add(itemData, newItem);
                onInventoryChangedEventCallback.Invoke();
            }
         
        
    }

    public void Remove(InventoryItemData itemData)
    {
        if (itemDictionary.TryGetValue(itemData, out InventoryItem value))
        {
            value.RemoveFromStack();

            if (value.stackSize == 0)
            {
                inventory.Remove(value);
                itemDictionary.Remove(itemData);
            }
        }
        onInventoryChangedEventCallback.Invoke();
    }
}
