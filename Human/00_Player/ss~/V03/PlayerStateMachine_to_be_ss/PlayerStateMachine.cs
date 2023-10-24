using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    //declare reference variables

    PlayerInput _playerInput;
    CharacterController _charController;
    Animator _animator;

    //var
    private Vector2 _curMoveInput;
    private Vector3 _curMove;
    private Vector2 _curWalkStickInput;
    private Vector3 _camRelativeMove;

    private bool _isMovePressed = false;
    private bool _isWalkPressed = false;
    private int _isRunHash;
    [SerializeField] private float _runSpeed = 3.0f;
    [SerializeField] private float _rotFactorPerFrame = 15.0f;

    private float _gravity = -9.8f;
    private float _groundedGravity = -.5f;

    private bool _isJumpPressed = false;
    private bool _isJumping = false;
    private int _isJumpHash;
    private int _isWalkHash;
    private int _isFallHash;
    private float _fallThershold = -3f;
    
    private float _initJumpVelo;
    private bool _isAfterJumpFalling;
    
    [SerializeField] private float _fallSpeed = 2.0f;
    [SerializeField] private float _maxJumpHeight = 1.0f;
    [SerializeField] private float _maxJumpTime = 0.5f;
    [SerializeField] private float _walkSpeed = 1.0f;
    [SerializeField] private float _walkThrsMin = -.5f;

    //state var
    PlayerBaseState _curState;
    PlayerStateFactory _states;


    //setter getter 
    public PlayerBaseState CurState { get { return _curState; } set { _curState = value; } }
    public Animator Animator { get { return _animator; } }
    public CharacterController CharController { get { return _charController; } }

    ////to stop hold down continuous jump, not working with jump frames
    //private bool _requireNewJumpPressed = false;
    //public bool RequireNewJumpPressed { get { return _requireNewJumpPressed; } set { _requireNewJumpPressed = value; } }

    

    public int IsRunHash { get { return _isRunHash; } set { _isRunHash = value; } }
    public int IsWalkHash { get { return _isWalkHash; } set { _isWalkHash = value; } }
    public int IsFallHash { get { return _isFallHash; } set { _isFallHash = value; } }
    public float FallThershold { get { return _fallThershold; } }

    public bool IsMovePressed { get { return _isMovePressed; } }
    public bool IsWalkPressed { get { return _isWalkPressed; } }

    public bool IsJumpPressed { get { return _isJumpPressed; } }
    public int IsJumpHash { get { return _isJumpHash; } }
    public bool IsJumping { set { _isJumping = value; } }
    public float CurMoveY { get { return _curMove.y; } set { _curMove.y = value; } }
    public float CurMoveX { get { return _curMove.x; } set { _curMove.x = value; } }
    public float CurMoveZ { get { return _curMove.z; } set { _curMove.z = value; } }
    public float InitJumpVelo { get { return _initJumpVelo; } }
    public float GroundedGravity { get { return _groundedGravity; } }
    public bool IsAfterJumpFalling {  get{ return _isAfterJumpFalling; } set { _isAfterJumpFalling = value; } }
    public float Gravity { get { return _gravity; } }
    public float FallSpeed { get { return _fallSpeed; } }
    public float WalkSpeed { get { return _walkSpeed; } }

    void Awake()
    {
        //set ref var
        _playerInput = new PlayerInput();
        _charController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();

        //setup state
        _states = new PlayerStateFactory(this);
        _curState = _states.Grounded();
        _curState.EnterState();

        //performance optm
        _isRunHash = Animator.StringToHash("isRun");
        _isJumpHash = Animator.StringToHash("isJump");
        _isWalkHash = Animator.StringToHash("isWalk");
        _isFallHash = Animator.StringToHash("isFall");

        //input callbacks
        //move
        _playerInput.CharacterControls.Move.started += OnMoveInput;
        _playerInput.CharacterControls.Move.canceled += OnMoveInput;
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
        _playerInput.CharacterControls.Jump.canceled += OnJumpInput;

        SetupJumpVar();

    }

    private void Start()
    {
        _charController.Move(_camRelativeMove * Time.deltaTime);
    }

    void OnEnable()
    {
        _playerInput.CharacterControls.Enable();
    }

    void OnDisable()
    {
        _playerInput.CharacterControls.Disable();
    }

    void Update()
    {
        SpeedHandle();

        RotHandle();

        _curState.UpdateStates();

        _camRelativeMove = ConvertToCameraSpace(_curMove);

        _charController.Move(_camRelativeMove * Time.deltaTime);

        
    }


    //--------------------------------------------


    //Jump physics
    private void SetupJumpVar()
    {
        float timeToApex = _maxJumpTime / 2;
        _gravity = (-2 * _maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        _initJumpVelo = (2 * _maxJumpHeight) / timeToApex;
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
        //_requireNewJumpPressed = false; //not working with jump frames

        SetupJumpVar();//to be removed after testing
    }

    //Speed
    private void SpeedHandle()
    {
        if (!_isWalkPressed)
        {
            _curMove.x = _curMoveInput.x * _runSpeed;
            _curMove.z = _curMoveInput.y * _runSpeed;
        }
        else
        {
            _curMove.x = _curMoveInput.x * _walkSpeed;
            _curMove.z = _curMoveInput.y * _walkSpeed;
        }
    }

    //Run input callback
    private void OnMoveInput(InputAction.CallbackContext context)
    {
        _curMoveInput = context.ReadValue<Vector2>();
        
        _isMovePressed = _curMoveInput.x != 0 || _curMoveInput.y != 0;
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

    
    private void CheckStickInput()
    {
        _isWalkPressed = false;

        if (Mathf.Log(_curWalkStickInput.magnitude) < _walkThrsMin)
        {
            _isWalkPressed = true;
        }
    }

    Vector3 ConvertToCameraSpace(Vector3 vectorToRotate)
    {
        float curY = vectorToRotate.y;

        Vector3 camF = Camera.main.transform.forward;
        Vector3 camR = Camera.main.transform.right;

        camF.y = 0;
        camR.y = 0;

        camF = camF.normalized;
        camR = camR.normalized;

        Vector3 camFZ = vectorToRotate.z* camF;
        Vector3 camRX = vectorToRotate.x * camR;

        Vector3 camFinalVector = camFZ + camRX;
        camFinalVector.y = curY;
        return camFinalVector;
    }

}
