using UnityEngine;
using static UnityEditor.Progress;

public class ItemObject : MonoBehaviour
{
    public InventoryItemData itemData;



    public void OnHandlePickUp()
    {
        InventorySystem.Instance.Add(itemData);
        Destroy(gameObject);
    }

   private void OnMouseDown()
    {

            if (gameObject.tag == "Saveables")
            {
                OnHandlePickUp();
            }
        
     
    }
}
