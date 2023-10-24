using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New ItemData", menuName = "Inventory/Items")]
public class ItemData : ScriptableObject
{
    public string Id;
    public string ItemDisplayName;
    public string ItemDescripton;
    public float Price;

    //[Header("Sprite format must be SQUARE")]
    public Sprite ItemSprite;

    //[Header("Sprite format must be SQUARE")]
    public Sprite DetailSprite;

    public GameObject Prefab;


    public bool Usable;
    public bool Dropable;
    public bool Equippable;
    public bool Unique;

    

    //public UnityEvent UseEvent;
    //public UnityEvent DropEvent;
    //public UnityEvent EquipEvent;

    //public void Use()
    //{
    //    Debug.Log("Using Item");
    //    UseEvent.Invoke();
    //}

    //public void Drop()
    //{
    //    Debug.Log("Dropping Item");
    //    DropEvent.Invoke();
    //}

    //public void Equip()
    //{
    //    Debug.Log("Equiping Item");
    //    EquipEvent.Invoke();
    //}
}
