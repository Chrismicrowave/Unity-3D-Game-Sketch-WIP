using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using Newtonsoft.Json;

public class LoadingInv : MonoBehaviour
{
    public static LoadingInv Instance;


    private List<Item> _allItems;

    //for loading Json
    public Dictionary<string, int> _savedInvDict;

    //output
    public List<Item> LoadedInventory { get; private set; }

    //*******************************************
    //** to add to init whenever new items **
    //*******************************************
    [SerializeField] private ItemData BubbleTeaData;
    private Item BubbleTea;

    [SerializeField] private ItemData WaterBottleData;
    private Item WaterBottle;

    [SerializeField] private ItemData NewsPaperData;
    private Item NewsPaper;



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



        //*******************************************
        //** to add to init whenever new items **
        //*******************************************
        BubbleTea = new Item(BubbleTeaData);
        WaterBottle = new Item(WaterBottleData);
        NewsPaper = new Item(NewsPaperData);

        _allItems = new List<Item>() { BubbleTea, WaterBottle, NewsPaper };



        //init
        _savedInvDict = new Dictionary<string, int>();
        LoadedInventory = new List<Item>();

    }

    private void Start()
    {
        
    }

    public List<Item> LoadSavedInv()
    {
        //Debug.Log("[LoadingInv] Start Loading..");

        string loadedJson = File.ReadAllText("Assets/DataStorage/playerInventory.json");

        if (loadedJson == "{}" || loadedJson == "" || loadedJson == null)
        {
            Debug.Log("[LoadingInv] - Json null");
            LoadedInventory = new List<Item>();
            return LoadedInventory;
        }

        _savedInvDict = JsonConvert.DeserializeObject<Dictionary<string, int>>(loadedJson);

        //parsing json to list<item> and output
        //loop thru saved Json
        foreach (KeyValuePair<string, int> pair in _savedInvDict)
        {
            //check against all items
            foreach (Item i in _allItems)
            {
                if (pair.Key == i.data.Id)
                {
                    //if matched, take number held,
                    //add to output LoadedInventory to InvManager

                    i.stackSize = pair.Value;
                    LoadedInventory.Add(i);
                }
            }

        }

        //Debug.Log("..Inv Loaded [LoadingInv]");

        return LoadedInventory;

    }

}
