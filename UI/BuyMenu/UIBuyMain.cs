using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class UIBuyMain : MonoBehaviour
{
    public static UIBuyMain Instance;
    public static bool MenuOn;
    

    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _npcItemGrid;

    Animator _anim;

    [Header("Detail Info Slot")]
    [SerializeField] TextMeshProUGUI DetailSlotDescr;
    [SerializeField] Image DetailSlotSprite;
    [SerializeField] TextMeshProUGUI DetailSlotName;

    [SerializeField] TextMeshProUGUI PriceTxt;
    [SerializeField] TextMeshProUGUI NumBuyTxt;
    [SerializeField] TextMeshProUGUI TotalPriceTxt;
    [SerializeField] TextMeshProUGUI PlayerMoneyTxt;

    public Item CurItem;

    public int NumBuy = 1;
    public float ItemPrice;
    public float TotalPrice;

    [SerializeField] private SignalVar onNPCStockChanged;


    //public static List<Item> CurNPCStock;
    public ItemListSO CurNPCStockSO;

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

        _anim = GetComponent<Animator>();

        //CurNPCStock = new List<Item>();
        CurNPCStockSO.RuntimeValue = CurNPCStockSO.initialValue;

    }

    void Start()
    {
        
    }

    void Update()
    {
        ShowBuyMenu();
    }


    //getting player selection data from InvUIItemSlot OnSelect()
    public void SetupDetailSlot()
    {
        DetailSlotName.text = CurItem.data.name;
        DetailSlotSprite.sprite = CurItem.data.ItemSprite;
        DetailSlotDescr.text = CurItem.data.ItemDescripton;
        PriceTxt.text = CurItem.data.Price.ToString("F2");

        NumBuy = 1;
        ItemPrice = CurItem.data.Price;
        
    }

    


    public void ShowBuyMenu()
    {
        if (PlayerState.IsPlayerInBuy)
        {
            if (!_menu.activeSelf) { _menu.SetActive(true);}
        }
        else {
            if (_menu.activeSelf) { _menu.SetActive(false);}
        }


        if (DialogueManager.DialogLastSen && PlayerState.IsPlayerInBuy)
        {
            if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Close"))
            {
                _anim.Play("Open");
                MenuOn = true;

                PlayerMoneyTxt.text = InventoryManager.PlayerMoney.ToString("F2");
                SelectNPCItem();

                UtilityFunctions.Instance.Pause();
            }
            
        }
        else 
        {
            if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Open"))
            {
                _anim.Play("Close");
                MenuOn = false;

                UtilityFunctions.Instance.Resume();
            }
            
        }
    }

    //UI onClick event
    public void ExitBuyMenuState()
    {
        PlayerState.IsPlayerInBuy = false;
        DialogueManager.DialogLastSen = false;
        DialogueManager.Instance.EndDialogue();

        //move cursor away from close btn and clear npc seller inv
        SelectNPCItem();
        _npcItemGrid.GetComponent<UINPCStockPanel>().ClearNPCInvSlotOnExit();


    }

    public void SelectNPCItem()
    {
        InvUIItemSlot slot = _npcItemGrid.transform.GetChild(0).GetComponent<InvUIItemSlot>();

        //set up detail slot
        slot.OnSelect();

        //cursor select
        slot.primaryButton.Select();
            
    }


    public void OnClickNumAdd()
    {
        NumBuy += 1;
        if (NumBuy > 99) { NumBuy = 99; }
        UpdateBuyUI();
    }

    public void OnClickNumMinus()
    {
        NumBuy -= 1;
        if (NumBuy < 1){ NumBuy = 1;}
        UpdateBuyUI();
    }

    public void UpdateBuyUI()
    {
        TotalPrice = ItemPrice * NumBuy;

        NumBuyTxt.text = NumBuy.ToString();
        TotalPriceTxt.text = TotalPrice.ToString("F2");
        
    }

    public void OnClickBuy()
    {
        if(TotalPrice < InventoryManager.PlayerMoney)
        {
            int numBuy = NumBuy;
            Item curItem = CurItem;

            //deduct money
            InventoryManager.PlayerMoney -= TotalPrice;
            InventoryManager.Instance.OnMoneyChanged();
            PlayerMoneyTxt.text = InventoryManager.PlayerMoney.ToString("F2");

            //add item to inv
            InventoryManager.Instance.AddMultiple(numBuy, curItem.data);
            InventoryManager.Instance.OnInventoryChanged();

            //update CurNPCStock
            Remove(numBuy, curItem);


            // Save CurNpcStock to coll NPC
            // call coll NpcType to update stock if name match with UINPCStockPanel
            // 
            // BuyMenu/NPCStock/ItemGird to receive signal
            // to call UINPCStockPanel to update npc stock UI grid
            onNPCStockChanged.Raise();
            
        }
        else
        {
            //show not enough money
        }
    }

    public void Remove(int num, Item itm)
    {
        
        foreach (Item i in CurNPCStockSO.RuntimeValue)
        {
            
            if (i.data.Id == itm.data.Id)
            {
                i.stackSize -= num;
                return;
            }
        }
    }
}
