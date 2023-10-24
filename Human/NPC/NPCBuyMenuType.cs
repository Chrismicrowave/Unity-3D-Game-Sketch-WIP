using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NPCBuyMenuType : MonoBehaviour
{
    [Header("Ensure List num identical acrossed")]
    [SerializeField] private List<ItemData> _npcStockList;
    [SerializeField] private List<int> _npcStockCount;

    //for quick ref ItemData-In -> Item 
    //should combine?
    private Dictionary<ItemData, Item> _itemDataPair;

    [System.NonSerialized]
    public List<Item> NPCStock;

    

    public bool PlayerNearMe;
    public bool LoadedToUIGird;

    [Header ("SignalVar SO")]
    public SignalVar LoadStockSignal;


    private void Awake()
    {
        NPCStock = new List<Item>();
        _itemDataPair = new Dictionary<ItemData, Item>();

        LoadInitStock();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }


    public void LoadStockToUI()
    {
        UIBuyMain.Instance.CurNPCStockSO.RuntimeValue = NPCStock;
        //UIBuyMain.CurNPCStock = NPCStock;

        // BuyMenu/NPCStock/ItemGird to receive signal
        LoadStockSignal.Raise();


        LoadedToUIGird = true;
    }


    private void LoadInitStock()
    {
        
        //load stock to list
        for (int i = 0; i < _npcStockList.Count; i++)
        {

            Item itm = new Item(_npcStockList[i]);
            itm.stackSize = _npcStockCount[i];

            NPCStock.Add(itm);
        }

        //update pair dict
        foreach (Item i in NPCStock)
        {
            _itemDataPair.Add(i.data, i);
        }

        //Debug.Log(this.name + " - Stock Loaded");
        
    }

    public void RemoveMultiple(int count, ItemData itemData)
    {
        for (int i = 0; i < count; i++)
        {
            Remove(itemData);
        }
    }

    public void Remove(ItemData ItemDataIn)
    {
        if (_itemDataPair.TryGetValue(ItemDataIn, out Item item))
        {
            item.RemoveFromStack();

            if (item.stackSize == 0)
            {
               NPCStock.Remove(item);
                _itemDataPair.Remove(ItemDataIn);

                //SaveInv();
            }

            //onStockChangedEvent();

        }

    }

    public void OnSignalUdpateStock()
    {
        if (UINPCStockPanel.CurNameNPCBuyMenuType == this.name)
        {
            //NPCStock = UIBuyMain.CurNPCStock;
            NPCStock = UIBuyMain.Instance.CurNPCStockSO.RuntimeValue;
            Debug.Log(this.name + " Stock Updated");
        }
    }


}
