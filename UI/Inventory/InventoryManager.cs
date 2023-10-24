using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using Newtonsoft.Json;

public class InventoryManager: MonoBehaviour
{
    public static InventoryManager Instance;

    //>>Items
    //for quick ref ItemData-In -> Item 
    //should combine?
    private Dictionary<ItemData, Item> _itemDataPair;

    //runtime inventory
    public List<Item> Inventory { get; private set; }

    //for save into Json
    public Dictionary<string, int> _playerSavedInventory;

    //>>Money
    public static float PlayerMoney = 0f;
    //public FloatValue PlayerMoney;

    public Item CurItem;

    public static event Action onInventoryChangedEvent;
    public static event Action onMoneyChangedEvent;

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
        InvUIMainPanel.Instance.InvMenu.SetActive(false);
    }

    private void OnApplicationQuit()
    {
        //InvUIMainPanel.Instance.InvMenu.SetActive(true);
        SaveInv();
    }

    private void Update()
    {
       

    }

    public void AddMultiple(int count,ItemData itemData)
    {
        for (int i = 0; i < count; i++)
        {
            Add(itemData);
        }
    }

    public void Add(ItemData ItemDataIn)
    {
        //for quick ref "ItemData"In -> "Item " to check if "Item" exist
        if (_itemDataPair.TryGetValue(ItemDataIn, out Item item))
        {
            item.AddToStack();


            //SaveInv();
            onInventoryChangedEvent();

        }
        else
        {
            //for quick ref to create "Item" <- "ItemData" In, if "Item" NOT exist
            Item newItem = new Item(ItemDataIn);
            Inventory.Add(newItem);
            _itemDataPair.Add(ItemDataIn, newItem);


            //SaveInv();
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

                //SaveInv();
            }

            onInventoryChangedEvent();

        }

    }

    //save and load
   

    public void SaveInv()
    {
        foreach (Item i in Inventory)
        {
            string id = i.data.Id;
            int num = i.stackSize;

            if (_playerSavedInventory.ContainsKey(i.data.Id))
            {
                _playerSavedInventory[i.data.Id] = num;
            }
            else
            {
                _playerSavedInventory.Add(id, num);
            }
                    
        }

        string json = JsonConvert.SerializeObject(_playerSavedInventory);
        File.WriteAllText("Assets/DataStorage/playerInventory.json", json);

        //money
        string jsonMoney = JsonConvert.SerializeObject(PlayerMoney);
        File.WriteAllText("Assets/DataStorage/playerMoney.json", jsonMoney);

        //Debug.Log("[Inventory Manager] Inv Dict & Money Json saved");
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

        //money
        string jsonMoney = System.IO.File.ReadAllText("Assets/DataStorage/playerMoney.json");
        PlayerMoney = JsonConvert.DeserializeObject<float>(jsonMoney);

        //invoke for init in UI
        onInventoryChangedEvent();
        onMoneyChangedEvent();
    }

    public void OnMoneyChanged()
    {
        onMoneyChangedEvent();
    }

    public void OnInventoryChanged()
    {
        onInventoryChangedEvent();
    }
}
