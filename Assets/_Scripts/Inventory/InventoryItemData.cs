using UnityEngine;

[CreateAssetMenu(fileName = "Inventory Item Data", menuName = "Inventory System/Create Item",order = 0)]
public class InventoryItemData : ScriptableObject
{
    public string itemName,id;
    public Sprite itemIcon;
    public GameObject itemPrefab;
}
