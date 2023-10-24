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
    JumpHandler _jump;


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
    [SerializeField] private float _fallThershold = -4.5f;
    private float _initJumpVelo;

    private string _curState = "isIdle";
    private string _lastState;
    private string _lastStateAu;

    //private bool _isIdle;
    //private bool _isAfterJumpFall;
    //private bool _isFall;
    private bool _isMovePressed = false;
    private bool _isWalkPressed = false;
    private bool _isJumpPressed = false;
    private bool _isJumping = false;
    private bool _isFallAnimEnd = false;
    private bool _stateChanged = false;
    private bool _isGrounded;
    private bool _isAfterApexFall;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _charController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();

        

        _jump = new JumpHandler();
        _gravityHandler = new GravityHandler();


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


        //jump physics
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
        
    }


    void Update()
    {
        Debug.Log(_curState);
        Debug.Log(_curMove.y);
        Debug.Log(_gravity);

        //idle move and turn
        SpeedHandle();
        RotHandle();
        StateHandle();


        AnimHandle();

        AnimAudio();

        //AudioHandle(_curState);


        Actions();
        GravityHandle();

        //Camera
        _camRelativeMove = ConvertToCameraSpace(_curMove);
        _charController.Move(_camRelativeMove * Time.deltaTime);

        //Debug.Log(_curState);
        //Debug.Log(_curMove.y);
        //Debug.Log("grounded: " + _isGrounded);
        //Debug.Log("isJumping: " + _isJumping);
        //Debug.Log("isApex: " + _isAfterApexFall);
        //Debug.Log("----------------");
    }


    //--------------------------------------------

    //State
    private void StateHandle()
    {
        _isGrounded = _charController.isGrounded;
        _isJumping = _jump.IsJumpingCheck(_isGrounded, _isJumpPressed, _isJumping);
        _isAfterApexFall = _jump.IsAfterApex(_curMove, _isJumping,_isJumpPressed);

        //Jump
        if (_isJumping)
        { _curState = "isJump"; }

        //Run
        else if (_isGrounded && _isMovePressed && !_isWalkPressed)
        { _curState = "isRun"; }

        //Walk
        else if (_isGrounded && _isWalkPressed && _isMovePressed)
        { _curState = "isWalk"; }

        //FallSmall
        else if (!_isGrounded && !_isJumping && (_curMove.y > _fallThershold))
        { _curState = "isFallStep"; }

        //Fall
        else if (!_isGrounded && !_isJumping && (_curMove.y < _fallThershold))
        { _curState = "isFall"; }

        //Idle
        else
        { _curState = "isIdle"; }
    }

        

    //Animation
    private void AnimHandle()
    {
        if (_lastState == _curState) { return; }


        if (_curState == "isFallStep")
        {
            _animator.Play(_lastState);
        }
        else
        {
            _animator.Play(_curState);
        }

        _lastState = _curState;

    }

    

    //Audio
    private void AnimAudio()
    {
        if (_lastStateAu == _curState)
        { return; }
        
        switch (_curState)
        {
            case "isIdle":

                AnimEffect.Instance.StopAudio();

                if (_lastStateAu == "isJump" || _lastStateAu == "isFall")
                {AnimEffect.Instance.IsLandedAudio();}
                
                _lastStateAu = _curState;
                break;

            case "isRun":

                if (_lastStateAu == "isJump" || _lastStateAu == "isFall")
                {
                    StartCoroutine(LandedThenRun());//Landed sound play first
                    _lastStateAu = _curState;
                    break;
                }
                else
                {
                    AnimEffect.Instance.IsRunAudio();
                    _lastStateAu = _curState;
                    break;
                }

                
            case "isWalk":

                if (_lastStateAu == "isJump" || _lastStateAu == "isFall")
                {
                    StartCoroutine(LandedThenWalk());//Landed sound play first
                    _lastStateAu = _curState;
                    break;
                }
                else
                {
                    AnimEffect.Instance.IsWalkAudio();
                    _lastStateAu = _curState;
                    break;
                }

            case "isJump":
                _lastStateAu = _curState;
                break;

            case "isFallStep":
                _lastStateAu = _curState;
                break;

            case "isFall":
                AnimEffect.Instance.StopAudio();
                _lastStateAu = _curState;
                break;
            
        }
    }

    //Chracter Actions
    private void Actions()
    {
        //initial jump velo/gravity till apex
        _curMove = _jump.Jump(_curMove,_initJumpVelo,_isJumpPressed,_isGrounded,_isJumping);
    }

    //Gravity
    private void GravityHandle()
    {
        

        switch (_curState)
        {
            case "isIdle":
            case "isRun":
            case "isWalk":
                _curMove = _gravityHandler.Grounded(_curMove,_groundedGravity);
                break;

            //case "isRun":
            //    _curMove = _gravityHandler.Grounded(_curMove, _groundedGravity);
            //    break;

            //case "isWalk":
            //    _curMove = _gravityHandler.Grounded(_curMove, _groundedGravity);
            //    break;

            case "isJump":
                
                if (_isAfterApexFall)
                { _curMove = _gravityHandler.Fall(_curMove,_gravity,_fallSpeed); }

                else if (_isJumping)
                { _curMove = _gravityHandler.Rise(_curMove,_gravity); }

                break;

            case "isFallStep":
            case "isFall":
                _curMove = _gravityHandler.Fall(_curMove, _gravity, _fallSpeed);
                break;

            //case "isFall":
            //    _curMove = _gravityHandler.Fall(_curMove, _gravity, _fallSpeed);
            //    break;
        }

    }


    //Non update main

    IEnumerator LandedThenRun()
    {
        AnimEffect.Instance.IsLandedAudio();
        yield return new WaitForSeconds(.3f);

        AnimEffect.Instance.IsRunAudio();
    }

    IEnumerator LandedThenWalk()
    {
        AnimEffect.Instance.IsLandedAudio();
        yield return new WaitForSeconds(.3f);

        AnimEffect.Instance.IsWalkAudio();
    }


    //Jump physics
    private void SetupJumpVar()
    {
        float timeToApex = _maxJumpTime / 2;
        _gravity = (-2 * _maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        _initJumpVelo = (2 * _maxJumpHeight) / timeToApex * .5f;

    }

    

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
        SetupJumpVar();//init gravity

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
