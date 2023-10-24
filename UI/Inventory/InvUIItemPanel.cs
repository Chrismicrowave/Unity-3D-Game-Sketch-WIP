using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InvUIItemPanel : MonoBehaviour
{
    public GameObject _slotPrefab;
    //public FloatValue PlayerMoney;
    [SerializeField] private TextMeshProUGUI _playermoney;

    
    protected virtual void Start()
    {
        InventoryManager.onInventoryChangedEvent += OnUpdateInventory;
        InventoryManager.onMoneyChangedEvent += OnUpdateMoney;
    }

    public void OnUpdateInventory()
    {
        
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }

        DrawInventory();
    }

    protected virtual void DrawInventory()
    {
        foreach(Item item in InventoryManager.Instance.Inventory)
        {
            AddInventorySlot(item);
        }
    }

    public void AddInventorySlot(Item item)
    {
        GameObject obj = Instantiate(_slotPrefab);
        obj.transform.SetParent(transform, false);

        InvUIItemSlot slot = obj.GetComponent<InvUIItemSlot>();
        slot.Set(item);

    }

    public void OnUpdateMoney()
    {
        
        //_playermoney.text = PlayerMoney.RuntimeValue.ToString("F2");
        _playermoney.text = InventoryManager.PlayerMoney.ToString("F2");
    }
}
