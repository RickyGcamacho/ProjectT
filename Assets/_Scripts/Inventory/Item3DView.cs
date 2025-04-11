using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item3DView : MonoBehaviour
{
    [SerializeField]private GameObject itemContainer;

   
    public void ItemView(InventoryItemData item)
    {
        
        if (itemContainer != null)
        {
            DestroyView();
        }
        if (item.id == "Key_Duchas" || item.id == "Key_Celdas" || item.id == "Key_Administracion")
        {
            itemContainer.transform.position = new Vector3(1145, 595.5f, 0);
            itemContainer = Instantiate(item.worldPrefab, itemContainer.transform.position, Quaternion.Euler(0, item.worldPrefab.transform.rotation.y, item.worldPrefab.transform.rotation.z));
            itemContainer.transform.localScale = new Vector3(3, 3, 3);
        }
        else
        {
            itemContainer.transform.position = new Vector3(1145, 595.5f, 0);
            itemContainer = Instantiate(item.worldPrefab, itemContainer.transform.position, Quaternion.Euler(-90, item.worldPrefab.transform.rotation.y, item.worldPrefab.transform.rotation.z));
            itemContainer.transform.localScale = new Vector3(3, 3, 3);
        }

        
    }

    public void DestroyView()
    {
        Destroy(itemContainer.gameObject);
    }
}
