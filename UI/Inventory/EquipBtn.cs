using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipBtn : MonoBehaviour
{
    [SerializeField] Image SlotHead;
    [SerializeField] Image SlotHands;
    [SerializeField] Image SlotFeet;

    [SerializeField] StringValue CurWeapon;
    [SerializeField] SignalVar CurWeaponSignal;

    [SerializeField] Transform EquipGO;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEquip()
    {
        if (EquipGO.childCount > 0)
        {
            Destroy(EquipGO.GetChild(0).gameObject);
        }
        

        //set sprite and CurWeapon SO
        SlotHands.sprite = InventoryManager.Instance.CurItem.data.ItemSprite;
        CurWeapon.RuntimeValue = InventoryManager.Instance.CurItem.data.Id;

        //enable x button
        InvUIMainPanel.Instance.XWeaponBtn.SetActive(true);

        // Player/PlayerWeaponControl to receive signal to activate script
        CurWeaponSignal.Raise();


        //Head, hands, feet equipments logic to be added, also in ItemData;
    }
}
