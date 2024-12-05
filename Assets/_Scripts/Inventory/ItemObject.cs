using System;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemObject : MonoBehaviour
{
    public InventoryItemData itemData;
    public InventoryItem itemsData;
    public Action OnPickedUp; // Evento que notifica que el objeto ha sido recogido

    public void OnHandlePickUp()
    {
        Debug.Log($"OnHandlePickUp called for {gameObject.name}"); // Confirmar que el método se ejecuta
        InventorySystem.Instance.Add(itemData); // Agrega el ítem al inventario
        OnPickedUp?.Invoke(); // Llama al evento para notificar al SpawnManager
        Destroy(gameObject); // Elimina el objeto del mundo
        Debug.Log($"GameObject {gameObject.name} destroyed.");
    }
}