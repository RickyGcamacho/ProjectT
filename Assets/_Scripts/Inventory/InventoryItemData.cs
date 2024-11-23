using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory Item Data",menuName ="Inventory System/ Create Item",order = 0)]
public class InventoryItemData : ScriptableObject
{
    public string id, description, itemName;
    public Sprite icon;
    public GameObject itemPrefab;
}
