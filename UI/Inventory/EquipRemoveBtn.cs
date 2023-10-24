using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipRemoveBtn : MonoBehaviour
{
    [Header("equip SO")]
    [SerializeField] StringValue EquipStringValue;
    [Header("equip Image")]
    [SerializeField] Image SlotImage;
    [Header("equip gameObject")]
    [SerializeField] Transform EquipGO;

    [SerializeField] SignalVar CurEquipSignal;

    void Start()
    {
        transform.gameObject.SetActive(false);
    }

    
    void Update()
    {
        
    }

    public void OnRemoveEquip()
    {
        Destroy(EquipGO.GetChild(0).gameObject);

        //empty Equip SO, sprite
        EquipStringValue.RuntimeValue = "";
        SlotImage.sprite = null;

        //set select cursor back to inv
        InvUIMainPanel.Instance.SetPrimaryBtn();

        // Player/PlayerWeaponControl to receive signal to de-activate script
        CurEquipSignal.Raise();

        transform.gameObject.SetActive(false);
    }
}
