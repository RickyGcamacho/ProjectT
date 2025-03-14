using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotNotes : MonoBehaviour
{
    [SerializeField]
    private Image itemIcon;

    public void Set(InventoryItem item)
    {
        itemIcon.sprite = item.data.icon;

    }
}
