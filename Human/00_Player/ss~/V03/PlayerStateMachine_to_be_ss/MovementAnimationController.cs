using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementAnimationController : MonoBehaviour
{
    //declare reference variables

    PlayerInput _playerInput;
    CharacterController _charController;
    Animator _animator;

    //var
    private Vector2 _curMoveInput;
    private Vector3 _curMove;
    private bool _isMovePressed = false;
    private int _isRunHash;
    [SerializeField] private float _runSpeed = 3.0f;
    [SerializeField] private float _rotFactorPerFrame = 15.0f;

    private float _gravity = -9.8f;
    private float _groundedGravity = -.05f;

    private bool _isJumpPressed = false;
    private bool _isJumping = false;
    private int _isJumpHash;
    private bool _isJumpAnim = false;
    private float _initJumpVelo;
    private bool _isFalling;
    [SerializeField] private float _fallSpeed = 2.0f;
    [SerializeField] private float _maxJumpHeight = 1.0f;
    [SerializeField] private float _maxJumpTime = 0.5f;


    
   


    void Awake()
    {
        //set ref var
        _playerInput = new PlayerInput();
        _charController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();

        //performance optm
        _isRunHash = Animator.StringToHash("isRun");
        _isJumpHash = Animator.StringToHash("isJump");

        //input callbacks
        //move
        _playerInput.CharacterControls.Move.started += onMoveInput;
        _playerInput.CharacterControls.Move.canceled += onMoveInput;
        //(for joycon)
        _playerInput.CharacterControls.Move.performed += onMoveInput;

        //jump
        _playerInput.CharacterControls.Jump.started += onJumpInput;
        _playerInput.CharacterControls.Jump.canceled += onJumpInput;

        setupJumpVar();

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
        RotHandle();
        AnimHandle();

        _charController.Move(_curMove * Time.deltaTime);

        GravityHandle();
        JumpHandle();

        //Debug.Log(curMove.y);
    }







    //------------------------------------------


    //Jump physics
    private void setupJumpVar()
    {
        float timeToApex = _maxJumpTime / 2;
        _gravity = (-2 * _maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        _initJumpVelo = (2 * _maxJumpHeight) / timeToApex;
    }

    //Jump
    private void JumpHandle()
    {
        if (!_isJumping && _charController.isGrounded && _isJumpPressed)
        {
            _animator.SetBool(_isJumpHash,true);
            _isJumpAnim = true;
            _isJumping = true;
            _curMove.y = _initJumpVelo * .5f;
        }
        else if (!_isJumpPressed && _isJumping && _charController.isGrounded)
        {
            _isJumping = false;
        }
    }

    private void onJumpInput(InputAction.CallbackContext context)
    {
        _isJumpPressed = context.ReadValueAsButton();
        setupJumpVar();//to be removed after testing
    }

    //movement run walk
    private void onMoveInput(InputAction.CallbackContext context)
    {
        _curMoveInput = context.ReadValue<Vector2>();
        _curMove.x = _curMoveInput.x * _runSpeed;
        _curMove.z = _curMoveInput.y * _runSpeed;

        _isMovePressed = _curMoveInput.x != 0 || _curMoveInput.y != 0;
    }

    //roation
    private void RotHandle()
    {
        Vector3 posLookAt;

        posLookAt.x = _curMove.x;
        posLookAt.y = 0.0f;
        posLookAt.z = _curMove.z;

        Quaternion curRot = transform.rotation;

        if (_isMovePressed) {
            Quaternion targetRot = Quaternion.LookRotation(posLookAt);
            transform.rotation = Quaternion.Slerp(curRot, targetRot, _rotFactorPerFrame * Time.deltaTime);
        }

    }

    //animation
    private void AnimHandle()
    {
        bool isRunning = _animator.GetBool(_isRunHash);
        //bool isJumping = animator.GetBool(isJumpHash);

        if (_isMovePressed && !isRunning) { _animator.SetBool(_isRunHash, true); }
        if (!_isMovePressed && isRunning) { _animator.SetBool(_isRunHash, false); }

        //if (isJumpPressed && !isJumping) { animator.SetBool("isJump", true); }
        //if (!isJumpPressed && isJumping) { animator.SetBool("isJump", false); }
    }

    private void GravityHandle()
    {
        _isFalling = _curMove.y <= -0.0f || !_isJumpPressed;


        if (_charController.isGrounded)
        {
            if (_isJumpAnim)
            {
                _animator.SetBool(_isJumpHash, false);
                _isJumpAnim = false;
            }
            _curMove.y = _groundedGravity;
            
        }
        else if(_isFalling)
        {
            float previousYVelo = _curMove.y;
            float newYVelo = _curMove.y + (_gravity * _fallSpeed * Time.deltaTime);
            float nextYVelo = Mathf.Max((previousYVelo + newYVelo)*.5f , -20.0f);
            _curMove.y = nextYVelo;
        }
        else
        {
            float previousYVelo = _curMove.y;
            float newYVelo = _curMove.y + (_gravity * Time.deltaTime);
            float nextYVelo = (previousYVelo + newYVelo) * .5f;
            _curMove.y = nextYVelo;
        }


    }
}
