using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using Newtonsoft.Json;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    //for quick between ItemData-Item two classes ref
    //should combine?
    private Dictionary<ItemData, Item> _itemDataPair;

    //runtime inventory
    public List<Item> Inventory { get; private set; }

    //for save into Json
    public Dictionary<string, int> _playerSavedInventory;


    public static bool IsPlayerInv;

    public event Action onInventoryChangedEvent;



    private void Awake()
    {
        if (Instance == null)
        { Instance = this; }
        else
        {
            Destroy(gameObject);
            return;
        }
        //DontDestroyOnLoad(gameObject);

        Inventory = new List<Item>();
        _itemDataPair = new Dictionary<ItemData, Item>();
        _playerSavedInventory = new Dictionary<string, int>();
        

    }

    private void Start()
    {
        LoadInv();

    }


    private void Update()
    {
        //if (Inventory.Count != 0)

        //{
        //    Debug.Log("11111 "+Inventory[0].data.ItemDisplayName + " : " + Inventory[0].stackSize);
        //    foreach (KeyValuePair<string, int> pair in _playerSavedInventory)
        //    {
        //        Debug.Log("222 "+pair.Key + " : " + pair.Value);
        //    }
        //}

    }

    public void Add(ItemData ItemDataIn)
    {
        if(_itemDataPair.TryGetValue(ItemDataIn, out Item item))
        {
            item.AddToStack();

            _playerSavedInventory[item.data.Id] = item.stackSize;
            SaveInv();

            onInventoryChangedEvent();

        }
        else
        {
            Item newItem = new Item(ItemDataIn);
            Inventory.Add(newItem);
            _itemDataPair.Add(ItemDataIn, newItem);

            _playerSavedInventory.Add(item.data.Id, item.stackSize);
            SaveInv();

            onInventoryChangedEvent();
        }

    }

    public void Remove(ItemData ItemDataIn)
    {
        if (_itemDataPair.TryGetValue(ItemDataIn, out Item item))
        {
            item.RemoveFromStack();
            _playerSavedInventory[item.data.Id] -= 1;

            if (item.stackSize == 0)
            {
                Inventory.Remove(item);
                _itemDataPair.Remove(ItemDataIn);

                _playerSavedInventory.Remove(item.data.Id);

            }

            SaveInv();
            onInventoryChangedEvent();

        }

    }

    //save and load
   

    public void SaveInv()
    {
        string json = JsonConvert.SerializeObject(_playerSavedInventory);
        File.WriteAllText("Assets/DataStorage/playerInventory.json", json);

        Debug.Log("saved");
    }

    private void LoadInv()
    {
        //get loaded List<item> from LoadSavedInv
        Inventory = LoadingInv.Instance.LoadSavedInv();

        //update pair dict
        foreach (Item i in Inventory)
        {
            _itemDataPair.Add(i.data, i);
        }

        //invoke for init in UI
        onInventoryChangedEvent();
    }

  
}
