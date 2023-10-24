using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InvUIItemSlot : MonoBehaviour
{
    //item slot
    [SerializeField] Image Icon;
    [SerializeField] GameObject StackBG;
    [SerializeField] TextMeshProUGUI NumberHeld;
    [SerializeField] GameObject Highlight;

    //data holder
    private Image _detailSprite;
    private string _displayName;
    private string _descr;
    private float _price;
    private bool _isUse;
    private bool _isEquip;
    private bool _isDrop;

    private Item _slotItem;

    private GameObject[] HLs;

    public Button primaryButton;

    private void Awake()
    {
        _detailSprite = GetComponent<Image>();

    }


    public void Set(Item item)
    {
        _slotItem = item;

        //data holder
        _detailSprite.sprite = item.data.DetailSprite;
        _displayName = item.data.ItemDisplayName;
        _descr = item.data.ItemDescripton;


        //set up player item panel slot
        //sprite
        Icon.sprite = item.data.ItemSprite;

        //num
        if (item.stackSize <= 1)
        {
            StackBG.SetActive(false);
            return;
        }

        NumberHeld.text = item.stackSize.ToString();


        //primaryButton.Select();
    }

    
    //detail info slot
    public void OnSelect()
    {
        //Inventory detail setup
        InventoryManager.Instance.CurItem = _slotItem;
        InvUIMainPanel.Instance.SetupDetailSlot(_slotItem);
        InvUIMainPanel.Instance.SetupDetailBtns(_slotItem);

        //BuyMenu detail setup
        UIBuyMain.Instance.CurItem = _slotItem;
        UIBuyMain.Instance.SetupDetailSlot();
        UIBuyMain.Instance.UpdateBuyUI();

        SetHighlight();

    }


    //setHL
    private void SetHighlight()
    {
        //setup selected highlight
        HLs = GameObject.FindGameObjectsWithTag("ItemHighlight");
        foreach (GameObject obj in HLs)
        {
            if (obj.activeSelf)
            {
                obj.SetActive(false);
                break;
            }
        }

        Highlight.SetActive(true);
    }

    //public void OnPointerEnter()
    //{
    //    InvUIMainPanel.Instance.SetupDetailSlot(_displayName, _detailSprite.sprite, _descr);
    //    UIBuyMain.Instance.SetupDetailSlot(_displayName, _detailSprite.sprite, _descr, _price);

    //    HLs = GameObject.FindGameObjectsWithTag("ItemHighlight");
    //    foreach (GameObject obj in HLs)
    //    {
    //        if (obj.activeSelf)
    //        {
    //            obj.SetActive(false);
    //            break;
    //        }
    //    }

    //    Highlight.SetActive(true);
    //}

    //public void OnPointerExit()
    //{
    //    HLs = GameObject.FindGameObjectsWithTag("ItemHighlight");
    //    foreach (GameObject obj in HLs)
    //    {
    //        if (obj.activeSelf)
    //        {
    //            obj.SetActive(false);
    //            break;
    //        }
    //    }
    //}
}
