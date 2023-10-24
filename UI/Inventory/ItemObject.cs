using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public ItemData ItemData;


    public void OnPickup()
    {
        InventoryManager.Instance.Add(ItemData);
        Destroy(gameObject);
    }

}
