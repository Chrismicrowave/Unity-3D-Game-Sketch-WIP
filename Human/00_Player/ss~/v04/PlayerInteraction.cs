using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : PlayerControl
{
    PlayerInput _playerInput;

    private bool _isButtonAPressed;
    private bool _requireNewPressed = true;

    private bool _playerInDia;
    [SerializeField] private float _interactRange = 1f;



    private void Awake()
    {
        _playerInput = new PlayerInput();

        _playerInput.CharacterControls.ButtonA.started += OnButtonAInput;
        _playerInput.CharacterControls.ButtonA.canceled += OnButtonAExitInput;
    }

    void OnEnable()
    {
        _playerInput.CharacterControls.Enable();

    }

    void OnDisable()
    {
        _playerInput.CharacterControls.Disable();
    }



    
    void Start()
    {
        
    }

    
    void Update()
    {
        

    }



    
    private void OnTriggerStay(Collider coll)
    {
        NPCDialogue(coll);
        ItemPickup(coll);
    }



    //Dialogue content Handled by NPCInteractable
    //Camera and Dialogue Logic Handled by DialogueManager

    private void NPCDialogue(Collider coll)
    {
        if (_requireNewPressed) { return; }
        else
        {
            if (_isButtonAPressed)
            {
                if (coll.TryGetComponent(out NPCInteractable npc))
                {
                    npc.Interact();
                    UISound.Instance.ButtonSound();
                    _requireNewPressed = true;
                }
            }
        }
    }

    private void ItemPickup(Collider coll)
    {
        if (_requireNewPressed) { return; }
        else
        {
            if (_isButtonAPressed)
            {
                if (coll.TryGetComponent(out ItemObject item))
                {
                    item.OnPickup();
                    UISound.Instance.ButtonSound();
                    _requireNewPressed = true;
                }
            }
        }
    }


    //input
    private void OnButtonAInput(InputAction.CallbackContext context)
    {
        _isButtonAPressed = context.ReadValueAsButton();
        _requireNewPressed = false;
    }

    private void OnButtonAExitInput(InputAction.CallbackContext context)
    {
        _isButtonAPressed = context.ReadValueAsButton();
        _requireNewPressed = true;
    }

    private void DialogueInput()
    {
        if (DialogueManager.Instance.IsPlayerInDialogue)
        {
            //disable movement other actions
        }
        else
        {
            //re-enable
        }
    }


    
}
