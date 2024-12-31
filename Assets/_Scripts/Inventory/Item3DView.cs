using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item3DView : MonoBehaviour
{
    [SerializeField]private InventorySystem inventorySystem;
    [SerializeField]private GameObject itemContainer;
    [SerializeField] private float speed;

   
    public void ItemView(InventoryItemData item)
    {
        
        if (itemContainer != null)
        {
            DestroyView();
        }
        itemContainer.transform.position = new Vector3(1145, 595.5f, 0);
        itemContainer = Instantiate(item.equippedPrefab, itemContainer.transform.position, Quaternion.Euler(-90, item.worldPrefab.transform.rotation.y, item.worldPrefab.transform.rotation.z));
        itemContainer.transform.localScale = new Vector3(3,3,3);

        
    }

    public void DestroyView()
    {
        Destroy(itemContainer.gameObject);
    }
}
