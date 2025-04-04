using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryUI : MonoBehaviour
{
    public GameObject itemSlotPrefab;
    public Transform saveablesPanel, notesPanel, collectablesPanel; // Paneles de las diferentes pestañas

    private void Start()
    {
        if (InventorySystem.Instance != null)
        {
            InventorySystem.Instance.onInventoryChangedEventCallback += OnUpdateInventory;
            DrawInventory();
        }
        else
        {
            Debug.LogError("❌ InventorySystem.Instance es NULL. ¿Está inicializado?");
        }
    }

    private void OnDestroy()
    {
        if (InventorySystem.Instance != null)
        {
            InventorySystem.Instance.onInventoryChangedEventCallback -= OnUpdateInventory;
        }
    }

    private void OnUpdateInventory()
    {
        // 🔹 Limpiar cada pestaña antes de redibujar
        foreach (Transform t in saveablesPanel)
        {
            Destroy(t.gameObject);
        }
        foreach (Transform t in notesPanel)
        {
            Destroy(t.gameObject);
        }
        foreach (Transform t in collectablesPanel)
        {
            Destroy(t.gameObject);
        }
        DrawInventory();
    }

    private void DrawInventory()
    {
        if (InventorySystem.Instance == null) return;

        foreach (InventoryItem itemPocket in InventorySystem.Instance.inventoryPocket)
        {
            AddInventorySlot(itemPocket);
        }

        foreach (InventoryItem itemNotes in InventorySystem.Instance.inventoryNotes)
        {
            AddInventorySlot(itemNotes);
        }

        foreach (InventoryItem itemCollectables in InventorySystem.Instance.inventoryCollectables)
        {
            AddInventorySlot(itemCollectables);
        }
    }

    private void AddInventorySlot(InventoryItem item)
    {
        GameObject obj = null;

        // 🔹 Determinar en qué pestaña agregar el objeto
        if (item.data.tipo == Tipo.Saveables)
        {
            obj = Instantiate(itemSlotPrefab, saveablesPanel, false);
            ItemSlot slot = obj.GetComponent<ItemSlot>();
            slot.Set(item);
        }
        else if (item.data.tipo == Tipo.Notes)
        {
            obj = Instantiate(itemSlotPrefab, notesPanel, false);
            ItemSlot slot2 = obj.GetComponent<ItemSlot>();
            slot2.Set(item);
        }
        else if (item.data.tipo == Tipo.Collectables)
        {
            obj = Instantiate(itemSlotPrefab, collectablesPanel, false);
            ItemSlot slot3 = obj.GetComponent<ItemSlot>();
            slot3.Set(item);
        }

        if (obj == null)
        {
            Debug.LogError($"❌ No se pudo instanciar el objeto para {item.data.name}");
        }
    }
}