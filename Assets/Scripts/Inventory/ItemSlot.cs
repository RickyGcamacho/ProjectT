using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private GameObject stackObj;
    [SerializeField] private TextMeshProUGUI stackNumber;

    public void Set(InventoryItem item)
    {
        itemIcon.sprite = item.data.itemIcon;

        if (item.stackSize <= 1)
        {
            stackObj.SetActive(false);
            return;
        }
        stackNumber.text = item.stackSize.ToString();
    }
}
