using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public static PlayerInteraction Instance;

    [SerializeField] private float _interactRange = 1f;


    private void Awake()
    {
        if (Instance == null)
        { Instance = this; }
        else
        {
            Destroy(gameObject);
            return;
        }

    }

    
    void Start()
    {
        
    }

    
    void Update()
    {
        //Debug.Log(PlayerState.IsPlayerInBuy);
        //Debug.Log("last sen: "+DialogueManager.DialogLastSen);
        //Debug.Log(UIBuyMain.Instance.CurNPCStockSO.RuntimeValue[0].data.Id);
    }



    //1.Dialogue content Handled by <NPCInteractable>
    //2.Dialogue Camera and Dialogue Logic Handled by <DialogueManager>
    //3.Go to "Menu Type NPC" Menu after Dialogue Handled by <DialogueManager>
  

    public void NPCTalk(Collider coll)
    {

        if (coll.TryGetComponent(out NPCInteractable npc))
        {
            if (PlayerInputManager.Instance.BtnAWasPressedOnce())
            {
                npc.Interact();
                
                //load stock and set state
                PlayerNearNPCBuyMenuType(coll);

                UISound.Instance.ButtonSound();

            }

        }
        
    }

    public void PlayerNearNPCBuyMenuType(Collider coll)
    {
        if (coll.TryGetComponent(out NPCBuyMenuType npcBuyMenuType))
        {
            PlayerState.IsPlayerInBuy = true;
            npcBuyMenuType.PlayerNearMe = true;

            //when last sentence load stock to ui buyMenu
            if (DialogueManager.DialogLastSen)
            {
                npcBuyMenuType.LoadStockToUI();

            }

            UINPCStockPanel.CurNameNPCBuyMenuType = coll.name;
        }
    }


    //Item
    public void ItemPickup(Collider coll)
    {
        
        if (coll.TryGetComponent(out ItemObject item))
        {
            if (PlayerInputManager.Instance.BtnAWasPressedOnce())
            {
                item.OnPickup();
                UISound.Instance.ButtonSound();
            }
        }
       
    }


}
