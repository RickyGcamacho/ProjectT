using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventory;
    [SerializeField]private bool see;
    // Start is called before the first frame update
    void Start()
    {
        inventory.SetActive(false);   
    }

    // Update is called once per frame
    void Update()
    {
        if (see == false)
        {
            if (Input.GetKey(KeyCode.I))
            {
                inventory.SetActive(true);
                see = true;
            }
        }
        else
        {
            inventory.SetActive(false);
            see = false;
        }
    }
}
