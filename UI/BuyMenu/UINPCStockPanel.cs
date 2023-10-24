using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINPCStockPanel : InvUIItemPanel
{
   
    public static UINPCStockPanel Instance;
    public static string CurNameNPCBuyMenuType;



    private void Awake()
    {
        
    }

    protected override void Start()
    {
        
    }

    private void Update()
    {
        
    }

    protected override void DrawInventory()
    {
        foreach (Item item in UIBuyMain.Instance.CurNPCStockSO.RuntimeValue)
        {
            AddInventorySlot(item);
        }
    }

    public void ClearNPCInvSlotOnExit()
    {
        foreach (Transform npcItem in transform)
        {
            Destroy(npcItem.gameObject);
        }
    }
    //public bool CheckStock(int num, Item itm)
    //{
        
    //    foreach (Item i in UIBuyMain.CurNPCStock)
    //    {
            
    //        if (i.data.Id == itm.data.Id)
    //        {
                
    //            if (i.stackSize > num)
    //            {
    //                return true;
    //            }
    //            else{return false;}
    //        }else{return false;}
    //    }
    //    return false;
    //}

    

   
}
