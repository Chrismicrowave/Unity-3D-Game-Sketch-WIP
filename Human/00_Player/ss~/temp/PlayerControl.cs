using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerControl : MonoBehaviour
{
    PlayerInput _playerInput;
    CharacterController _charController;
    Animator _animator;


    //States or actions related
    GravityHandler _gravityHandler;
    JumpHandler _jumpAction;


    private Vector2 _curMoveInput;
    private Vector3 _curMove;
    private Vector2 _curWalkStickInput;
    private Vector3 _camRelativeMove;

    [SerializeField] private float _runSpeed = 3.0f;
    [SerializeField] private float _rotFactorPerFrame = 15.0f;
    private float _gravity = -9.8f;
    private float _groundedGravity = -.5f;
    [SerializeField] private float _fallSpeed = 2.0f;
    [SerializeField] private float _maxJumpHeight = 1.0f;
    [SerializeField] private float _maxJumpTime = 0.5f;
    [SerializeField] private float _walkSpeed = 1.0f;
    [SerializeField] private float _walkThrsMin = -.5f;
    //private float _fallThershold = -3f;
    private float _initJumpVelo;

    private string _curState = "isIdle";
    private string _lastState;

    //private bool _isIdle;
    //private bool _isAfterJumpFall;
    //private bool _isFall;
    private bool _isMovePressed = false;
    private bool _isWalkPressed = false;
    private bool _isJumpPressed = false;
    private bool _isJumping = false;
    
    private bool _isGrounded;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _charController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();

        
        _jumpAction = new JumpHandler
            (_curMove, _initJumpVelo, _isJumpPressed, _isGrounded, _isJumping);

        _gravityHandler = new GravityHandler
            (_curMove, _gravity ,_groundedGravity, _fallSpeed);


        //input callbacks
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

        SetupJumpVar();



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
        _charController.Move(_camRelativeMove * Time.deltaTime);
        _curState = "isIdle";
    }


    void Update()
    {
        //idle move and turn
        SpeedHandle();
        
        RotHandle();
        StateHandle();
       
        AnimHandle(_curState);

        Debug.Log(_curMove);
        Actions();

        Debug.Log(_curMove);
        GravityHandle();

        

        //Camera
        _camRelativeMove = ConvertToCameraSpace(_curMove);
        _charController.Move(_camRelativeMove * Time.deltaTime);


        Debug.Log("----------------");
    }


    //--------------------------------------------
    private void StateHandle()
    {
        _isGrounded = _charController.isGrounded;
        _isJumping = _jumpAction.IsJumpingCheck();

        //Jump
        if (_isJumping)
        {_curState = "isJump";}

        //Run
        else if (_isGrounded && _isMovePressed && !_isWalkPressed)
        {_curState = "isRun";}

        //Walk
        else if (_isGrounded && _isWalkPressed && _isMovePressed)
        {_curState = "isWalk";}

        //Fall
        else if (!_isGrounded && !_isJumping)
        {_curState = "isFall";}

        //Idle
        else
        {_curState = "isIdle";}

    }

    private void AnimHandle(string _curState)
    {
        if (_lastState == _curState) { return; }

        _animator.Play(_curState);
        _lastState = _curState;
    }

    private void Actions()
    {
        //initial jump velo/gravity till apex
        _curMove = _jumpAction.Jump();
    }

    private void GravityHandle()
    {
        

        switch (_curState)
        {
            case "isIdle":
                _curMove.y = _gravityHandler.GravityGrounded();
                break;

            case "isRun":
                _curMove.y = _gravityHandler.GravityGrounded();
                break;

            case "isWalk":
                _curMove.y = _gravityHandler.GravityGrounded();
                break;

            case "isJump":
                //After jump apex gravity
                bool _isAfterApexFall = _jumpAction.IsAfterApexCheck();

                if (_isAfterApexFall)  { _curMove.y = _gravityHandler.GravityFall(); }
                else if (_isJumping) { _curMove.y = _gravityHandler.GravityRise(); }

                break;

            case "isFall":
                _curMove.y = _gravityHandler.GravityFall();
                break;
        }

    }

    //Jump physics
    private void SetupJumpVar()
    {
        float timeToApex = _maxJumpTime / 2;
        _gravity = (-2 * _maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        _initJumpVelo = (2 * _maxJumpHeight) / timeToApex * .5f;

    }

    //private void GravityRise(Vector3 CurMove)
    //{
    //    float previousYVelo2 = CurMove.y;
    //    float newYVelo2 = CurMove.y + (_gravity * Time.deltaTime);
    //    float nextYVelo2 = Mathf.Max((previousYVelo2 + newYVelo2) * .5f, -20.0f);
    //    CurMove.y = nextYVelo2;
    //}

    //private void GravityFall(Vector3 CurMove)
    //{
    //    float previousYVelo2 = CurMove.y;
    //    float newYVelo2 = CurMove.y + (_gravity * _fallSpeed * Time.deltaTime);
    //    float nextYVelo2 = Mathf.Max((previousYVelo2 + newYVelo2) * .5f, -20.0f);
    //    CurMove.y = nextYVelo2;
    //}

    //private void GravityGrounded(Vector3 CurMove,float GroundedGravity)
    //{
    //    CurMove.y = GroundedGravity;
    //}



    //Walk Stick callback
    private void OnWalkStickInput(InputAction.CallbackContext context)
    {
        _curWalkStickInput = context.ReadValue<Vector2>();
        CheckStickInput();
    }

    private void OnWalkStickInputExit(InputAction.CallbackContext context)
    {
        _isWalkPressed = false;
    }

    private void CheckStickInput()
    {
        _isWalkPressed = false;

        if (Mathf.Log(_curWalkStickInput.magnitude) < _walkThrsMin)
        {
            _isWalkPressed = true;
        }
    }



    //Walk Button callback
    private void OnWalkButtonInput(InputAction.CallbackContext context)
    {
        _isWalkPressed = context.ReadValueAsButton();

    }

    private void OnWalkButtonInputExit(InputAction.CallbackContext context)
    {
        _isWalkPressed = false;

    }




    //Jump input callback
    private void OnJumpInput(InputAction.CallbackContext context)
    {
        _isJumpPressed = context.ReadValueAsButton();
        SetupJumpVar();//to be removed after testing

    }
    private void OnJumpInputExit(InputAction.CallbackContext context)
    {
        _isJumpPressed = false;
    }


    //Speed
    private void SpeedHandle()
    {

        if (!_isWalkPressed && _isMovePressed)
        {
            _curMove.x = _curMoveInput.x * _runSpeed;
            _curMove.z = _curMoveInput.y * _runSpeed;
        }
        else if (_isWalkPressed && _isMovePressed)
        {
            _curMove.x = _curMoveInput.x * _walkSpeed;
            _curMove.z = _curMoveInput.y * _walkSpeed;
        }
        else
        {
            _curMove.x = 0;
            _curMove.z = 0;
        }


    }



    //Run input callback
    private void OnMoveInput(InputAction.CallbackContext context)
    {
        _curMoveInput = context.ReadValue<Vector2>();
        _isMovePressed = _curMoveInput.x != 0 || _curMoveInput.y != 0;
    }
    private void OnMoveInputExit(InputAction.CallbackContext context)
    {
        _isMovePressed = false;
    }




    //roation
    private void RotHandle()
    {

        Vector3 posLookAt;

        posLookAt.x = _camRelativeMove.x;
        posLookAt.y = 0.0f;
        posLookAt.z = _camRelativeMove.z;



        Quaternion curRot = transform.rotation;

        if (_isMovePressed && posLookAt != Vector3.zero)
        {
            Quaternion targetRot = Quaternion.LookRotation(posLookAt);
            transform.rotation = Quaternion.Slerp(curRot, targetRot, _rotFactorPerFrame * Time.deltaTime);
        }

    }

    //move relative to camera
    Vector3 ConvertToCameraSpace(Vector3 vectorToRotate)
    {
        float curY = vectorToRotate.y;

        Vector3 camF = Camera.main.transform.forward;
        Vector3 camR = Camera.main.transform.right;

        camF.y = 0;
        camR.y = 0;

        camF = camF.normalized;
        camR = camR.normalized;

        Vector3 camFZ = vectorToRotate.z * camF;
        Vector3 camRX = vectorToRotate.x * camR;

        Vector3 camFinalVector = camFZ + camRX;
        camFinalVector.y = curY;
        return camFinalVector;
    }


}
