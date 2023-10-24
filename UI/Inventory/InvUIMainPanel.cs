using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InvUIMainPanel : MonoBehaviour
{
    public static InvUIMainPanel Instance;

    Animator _anim;

    [SerializeField] StringValue CurWeapon;

    [SerializeField] public GameObject InvMenu;
    [SerializeField] public GameObject ItemGrid;

    [SerializeField] InvUIBar HungerBar;
    [SerializeField] InvUIBar ThirstBar;

    [Header("Detail Info Slot")]
    [SerializeField] TextMeshProUGUI DetailSlotDescr;
    [SerializeField] Image DetailSlotSprite;
    [SerializeField] TextMeshProUGUI DetailSlotName;

    [SerializeField] Button UseBtn;
    [SerializeField] Button EquipBtn;
    [SerializeField] Button DropBtn;

    [SerializeField] public GameObject XHeadBtn;
    [SerializeField] public GameObject XWeaponBtn;
    [SerializeField] public GameObject XFeetBtn;

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

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OnClickShowInv();
    }


    //02 DetialSlot
    public void SetupDetailSlot(Item CurItem)
    {
        DetailSlotName.text = CurItem.data.name;
        DetailSlotSprite.sprite = CurItem.data.ItemSprite;
        DetailSlotDescr.text = CurItem.data.ItemDescripton;

    }

    public void SetupDetailBtns(Item CurItem)
    {
        UseBtn.interactable = false;
        EquipBtn.interactable = false;
        DropBtn.interactable = false;

        if (CurItem.data.Usable) { UseBtn.interactable = true;}
        if (CurItem.data.Equippable) { EquipBtn.interactable = true; }
        if (CurItem.data.Dropable) { DropBtn.interactable = true; }
    }

    
    //01 inv state bool UI on off 
    private void OnClickShowInv()
    {
        if (PlayerInputManager.Instance.BtnItmWasPressedOnce())
        {
            
            if (!PlayerState.IsPlayerInv)
            {
                PlayerState.IsPlayerInv = true;
                InvMenu.SetActive(true);

                UpdateBars();
                SetPrimaryBtn();
                SetXEquipBtn();

                _anim.Play("Open");
                UtilityFunctions.Instance.Pause();

            }
            else if (PlayerState.IsPlayerInv)
            {
                PlayerState.IsPlayerInv = false;
                InvMenu.SetActive(false);
                _anim.Play("Close");
                UtilityFunctions.Instance.Resume();
            }
        }

    }

    void UpdateBars()
    {
        HungerBar.UpdateBar();
        ThirstBar.UpdateBar();
    }

    public void SetPrimaryBtn()
    {
        InvUIItemSlot itemSlot = ItemGrid.transform.GetChild(0).GetComponent<InvUIItemSlot>();
        itemSlot.primaryButton.Select();
        itemSlot.OnSelect();
    }

    void SetXEquipBtn()
    {
        if (CurWeapon.RuntimeValue != "")
        {
            XWeaponBtn.SetActive(true);
        }
    }

    ////01 UI on off
    ////bypass new input system - NOT USED
    //private void ShowInv()
    //{
    //    bool tmp1 = PlayerInputManager.Instance.IsKeyIPressed();
    //    bool tmp2 = PlayerInputManager.Instance.IsTheButtonPressed("button10");
    //    bool tmp = tmp1 || tmp2;

    //    if (tmp && !PlayerState.IsPlayerInv)
    //    {
    //        PlayerState.IsPlayerInv = true;
    //        UtilityFunctions.Instance.Pause();

    //    }
    //    else if (tmp && PlayerState.IsPlayerInv)
    //    {
    //        PlayerState.IsPlayerInv = false;
    //        UtilityFunctions.Instance.Resume();
    //    }
    //}

    

}
