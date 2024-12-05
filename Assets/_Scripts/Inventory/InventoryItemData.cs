using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItemData", menuName = "Inventory System/Create Item", order = 0)]
public class InventoryItemData : ScriptableObject
{
    public string id,itemName,itemDescription;
    public Sprite icon;
    public bool isEquippable; // Si el ítem puede ser equipado
    public GameObject worldPrefab,equippedPrefab; // Prefab cuando está equipado (opcional)
}