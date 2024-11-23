using UnityEngine;
using static UnityEditor.Progress;

public class ItemObject : MonoBehaviour
{
    public InventoryItemData itemData;
    [SerializeField] private GameObject inventory;


    public void OnHandlePickUp()
    {
        InventorySystem.Instance.Add(itemData);
        Destroy(gameObject);
    }

   private void OnMouseDown()
    {

        if (gameObject.tag == "Saveables")
        {
            if (inventory.transform.childCount <= 3)
            {
                OnHandlePickUp();
            }
        }
     
    }
}
