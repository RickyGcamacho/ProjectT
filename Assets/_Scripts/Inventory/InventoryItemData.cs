using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Tipo
{
    Saveables,
    Notes,
    Collectables,
    Throw
};

[CreateAssetMenu(fileName = "InventoryItemData", menuName = "Inventory System/Create Item", order = 0)]
public class InventoryItemData : ScriptableObject
{
    public string id,itemName,itemDescription;
    public Tipo tipo;
    public Sprite icon;
    public GameObject worldPrefab; 
}