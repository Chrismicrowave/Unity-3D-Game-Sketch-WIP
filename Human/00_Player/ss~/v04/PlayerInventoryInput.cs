using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInventoryInput : MonoBehaviour
{
    PlayerInput _playerInput;

    private bool _isBtnItmPressed;
    private bool _btnItmRequireNewPressed = true;

    
    private void Awake()
    {
        _playerInput = new PlayerInput();


        _playerInput.CharacterControls.ButtonItem.started += OnButtonItemInput;
        _playerInput.CharacterControls.ButtonItem.canceled += OnButtonItemExitInput;
    }

    private void OnEnable()
    {
        _playerInput.CharacterControls.Enable();
    }

    private void OnDisable()
    {
        _playerInput.CharacterControls.Disable();
    }

    private void Update()
    {
        //OnClickShowInv();
        //ShowInv();

        
    }


    ////bypass new input system
    //private void ShowInv()
    //{
    //    bool tmp1 = Keyboard.current[Key.I].wasPressedThisFrame;
    //    bool tmp2 = UtilityFunctions.Instance.ButtonName("button10").wasPressedThisFrame;
    //    bool tmp = tmp1 || tmp2;

    //    if (tmp && !InventoryManager.IsPlayerInv)
    //    {
    //        InventoryManager.IsPlayerInv = true;
    //    }
    //    else if (tmp && InventoryManager.IsPlayerInv)
    //    {
    //        InventoryManager.IsPlayerInv = false;
    //    }
    //}

    
    //ss for v04 player control
    ////new input system
    //private void OnClickShowInv()
    //{
    //    if (_btnItmRequireNewPressed) { return; }
    //    else
    //    {
    //        if (_isBtnItmPressed && !InventoryManager.IsPlayerInv)
    //        {
    //            InventoryManager.IsPlayerInv = true;
    //            UtilityFunctions.Instance.Pause();

    //            _btnItmRequireNewPressed = true;
    //        }
    //        else if (_isBtnItmPressed && InventoryManager.IsPlayerInv)
    //        {
    //            InventoryManager.IsPlayerInv = false;
    //            UtilityFunctions.Instance.Resume();

    //            _btnItmRequireNewPressed = true;
    //        }
    //    }
    //}

    private void OnButtonItemInput(InputAction.CallbackContext context)
    {
        _isBtnItmPressed = context.ReadValueAsButton();
        _btnItmRequireNewPressed = false;
    }

    private void OnButtonItemExitInput(InputAction.CallbackContext context)
    {
        _isBtnItmPressed = context.ReadValueAsButton();
        _btnItmRequireNewPressed = true;
    }

   

}
