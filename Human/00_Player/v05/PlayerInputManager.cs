using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using System.Linq;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager Instance;

    PlayerInput _playerInput;
    [SerializeField] private InputSystemUIInputModule UIInput;

    //Movement
    public static Vector2 CurMoveInput;
    public static Vector2 CurWalkStickInput;

    public static bool IsMovePressed { get; private set; }
    public static bool IsWalkPressed { get; private set; }
    public static bool IsJumpPressed { get; private set; }
    [SerializeField] private float _walkThrsMin = -.5f;

    //Item
    //to avoid sticky press
    public static bool IsBtnItmRequireNewPressed { get; set; }
    public static bool IsBtnItmPressed { get; private set; }

    //ButtonA Interact
    //to avoid sticky press
    public static bool IsBtnARequireNewPressed { get; set; }
    public static bool IsBtnAPressed { get; private set; }

    public static bool IsActionRequireNewPressed { get; set; }
    public static bool IsActionPressed { get; private set; }



    private void Awake()
    {
        if (Instance == null)
        { Instance = this; }
        else
        {
            Destroy(gameObject);
            return;
        }

        _playerInput = new PlayerInput();

        //for sticky press
        IsBtnItmRequireNewPressed = true;
        IsBtnARequireNewPressed = true;

        //input callbacks
        //Action
        _playerInput.CharacterControls.Action.started += OnActionInput;
        _playerInput.CharacterControls.Action.canceled += OnActionExitInput;

        //ButtonA Interaction
        _playerInput.CharacterControls.ButtonA.started += OnButtonAInput;
        _playerInput.CharacterControls.ButtonA.canceled += OnButtonAExitInput;

        //Item Inventory
        _playerInput.FunctionInput.ButtonItem.started += OnButtonItemInput;
        _playerInput.FunctionInput.ButtonItem.canceled += OnButtonItemExitInput;

        //move
        _playerInput.CharacterControls.Move.started += OnMoveInput;
        _playerInput.CharacterControls.Move.canceled += OnMoveInputExit;
        _playerInput.CharacterControls.Move.performed += OnMoveInput;

        //walk stick input
        _playerInput.CharacterControls.Walk.started += OnWalkStickInput;
        _playerInput.CharacterControls.Walk.performed += OnWalkStickInput;
        _playerInput.CharacterControls.Walk.canceled += OnWalkStickInputExit;
        //walk button input
        _playerInput.CharacterControls.WalkB.started += OnWalkButtonInput;
        _playerInput.CharacterControls.WalkB.canceled += OnWalkButtonInputExit;

        //jump
        _playerInput.CharacterControls.Jump.started += OnJumpInput;
        _playerInput.CharacterControls.Jump.canceled += OnJumpInputExit;
        
    }


    void OnEnable()
    {
        _playerInput.CharacterControls.Enable();
        _playerInput.FunctionInput.Enable();
        //UI input
        //UIInput.DeactivateModule();

    }

    void OnDisable()
    {
        _playerInput.CharacterControls.Disable();
        _playerInput.FunctionInput.Disable();
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        DialogueInput();
        UIMenuInput();
    }



    //08 Action button
    //05 Button A Interaction
    private void OnActionInput(InputAction.CallbackContext context)
    {
        IsActionPressed = context.ReadValueAsButton();
        IsActionRequireNewPressed = false;
    }

    private void OnActionExitInput(InputAction.CallbackContext context)
    {
        IsActionPressed = context.ReadValueAsButton();
        IsActionRequireNewPressed = true;
    }

    public bool ActionWasPressedOnce()
    {
        if (IsActionRequireNewPressed) { return false; }
        else
        {
            if (IsActionPressed)
            {
                IsActionRequireNewPressed = true;
                return true;
            }
            else { return false; }
        }
    }

    //07 UI input
    private void UIMenuInput()
    {
        if (UIBuyMain.MenuOn || PlayerState.IsPlayerInv) {
            //UIInput.ActivateModule();
            _playerInput.CharacterControls.Disable();
        }
        else {
            //UIInput.DeactivateModule();
            _playerInput.CharacterControls.Enable();
        }
    }

    //06 dialogue movement freeze
    private void DialogueInput()
    {
        if (PlayerState.IsPlayerInDialogue)
        {
            //UIInput.ActivateModule();
            _playerInput.CharacterControls.Move.Disable();
            
        }
        else
        {
            //UIInput.DeactivateModule();
            _playerInput.CharacterControls.Move.Enable();
        }
    }


    //05 Button A Interaction
    private void OnButtonAInput(InputAction.CallbackContext context)
    {
        IsBtnAPressed = context.ReadValueAsButton();
        IsBtnARequireNewPressed = false;
    }

    private void OnButtonAExitInput(InputAction.CallbackContext context)
    {
        IsBtnAPressed = context.ReadValueAsButton();
        IsBtnARequireNewPressed = true;
    }

    public bool BtnAWasPressedOnce()
    {
        if (IsBtnARequireNewPressed) { return false; }
        else
        {
            if (IsBtnAPressed)
            {
                IsBtnARequireNewPressed = true;
                return true;
            }
            else { return false;}
        }
    }

    public bool SubmitPressed()
    {
        if (UIInput.submit.action.WasPressedThisFrame())
        {
            return true;
        }
        else return false;
    }

    //04 Item Inventory
    private void OnButtonItemInput(InputAction.CallbackContext context)
    {
        IsBtnItmPressed = context.ReadValueAsButton();
        IsBtnItmRequireNewPressed = false;
    }

    private void OnButtonItemExitInput(InputAction.CallbackContext context)
    {
        IsBtnItmPressed = context.ReadValueAsButton();
        IsBtnItmRequireNewPressed = true;
    }

    public bool BtnItmWasPressedOnce()
    {
        if (IsBtnItmRequireNewPressed) { return false; }
        else
        {
            if (IsBtnItmPressed)
            {
                IsBtnItmRequireNewPressed = true;
                return true;
            }
            else { return false; }
        }
    }


    //04 for bypass-ing new input system
    public bool IsKeyIPressed()
    {
        return Keyboard.current[Key.I].wasPressedThisFrame;
    }

    public bool IsTheButtonPressed(string BtnName)
    {
        ButtonControl btn = ButtonName(BtnName);
        return btn.wasPressedThisFrame;
    }

    private ButtonControl ButtonName(string BtnName)
    {
        if (Joystick.current != null)
        {
            ButtonControl myButton = Joystick.current.allControls
                .FirstOrDefault(control => control is ButtonControl
                && control.name == BtnName) as ButtonControl;
            return myButton;
        }
        else
        {
            ButtonControl myButton = new ButtonControl();
            Debug.Log("From JoyStickButton ( " + BtnName + " ) - Joystick not found");
            return myButton;
        }
    }




    //03 Walk Stick callback
    private void OnWalkStickInput(InputAction.CallbackContext context)
    {
        CurWalkStickInput = context.ReadValue<Vector2>();
        CheckStickInput();
    }

    private void OnWalkStickInputExit(InputAction.CallbackContext context)
    {
        IsWalkPressed = false;
    }

    private void CheckStickInput()
    {
        IsWalkPressed = false;

        if (Mathf.Log(CurWalkStickInput.magnitude) < _walkThrsMin)
        {
            IsWalkPressed = true;
        }
    }


    //03 Walk Button callback
    private void OnWalkButtonInput(InputAction.CallbackContext context)
    {
        IsWalkPressed = context.ReadValueAsButton();

    }

    private void OnWalkButtonInputExit(InputAction.CallbackContext context)
    {
        IsWalkPressed = false;

    }


    //02 Jump input callback
    private void OnJumpInput(InputAction.CallbackContext context)
    {
        IsJumpPressed = context.ReadValueAsButton();
        PlayerJumpHandler.Instance.InitJumpGravity();
    }

    private void OnJumpInputExit(InputAction.CallbackContext context)
    {
        IsJumpPressed = false;
    }


    //01 Run input callback
    private void OnMoveInput(InputAction.CallbackContext context)
    {
        CurMoveInput = context.ReadValue<Vector2>();
        IsMovePressed = CurMoveInput.x != 0 || CurMoveInput.y != 0;
    }
    private void OnMoveInputExit(InputAction.CallbackContext context)
    {
        IsMovePressed = false;
    }

    //JoyCon UI confirm key conflict quickfix
    public void XOnSelectDisableButtonA()
    {
        _playerInput.CharacterControls.ButtonA.Disable();
    }

    public void XOnSelectEnableButtonA()
    {
        _playerInput.CharacterControls.ButtonA.Enable();
    }

}
