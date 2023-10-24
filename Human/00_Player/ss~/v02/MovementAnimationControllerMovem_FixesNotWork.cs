using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementAnimationController222222222222 : MonoBehaviour
{
    //declare reference variables

    PlayerInput playerInput;
    CharacterController charController;
    Animator animator;
    [SerializeField] Transform cameraTransform;

    //var
    private Vector2 curMoveInput;
    private Vector3 curMove;
    private Vector3 appliedMove;
    private bool isMovePressed = false;
    private int isRunHash;
    [SerializeField] private float runSpeed = 3.0f;
    [SerializeField] private float rotFactorPerFrame = 15.0f;

    private float gravity = -9.8f;
    private float groundedGravity = -.05f;

    private bool isJumpPressed = false;
    private bool isJumping = false;
    private int isJumpHash;
    private bool isJumpAnim = false;
    private float initJumpVelo;
    private bool isFalling;
    [SerializeField] private float fallSpeed = 2.0f;
    [SerializeField] private float maxJumpHeight = 1.0f;
    [SerializeField] private float maxJumpTime = 0.5f;


    
   


    void Awake()
    {
        //set ref var
        playerInput = new PlayerInput();
        charController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        //performance optm
        isRunHash = Animator.StringToHash("isRun");
        isJumpHash = Animator.StringToHash("isJump");

        //input callbacks
        //move
        playerInput.CharacterControls.Move.started += onMoveInput;
        playerInput.CharacterControls.Move.canceled += onMoveInput;
        //(for joycon)
        playerInput.CharacterControls.Move.performed += onMoveInput;

        //jump
        playerInput.CharacterControls.Jump.started += onJumpInput;
        playerInput.CharacterControls.Jump.canceled += onJumpInput;

        setupJumpVar();

    }

    void OnEnable()
    {
        playerInput.CharacterControls.Enable();
    }

    void OnDisable()
    {
        playerInput.CharacterControls.Disable();
    }


    void Start()
    {
        
    }

    void Update()
    {
        RotHandle();
        AnimHandle();

        curMove.y = groundedGravity;
        appliedMove.x = curMove.x;
        appliedMove.z = curMove.z;

        charController.Move(appliedMove * Time.deltaTime);

        GravityHandle();
        JumpHandle();
        Debug.Log(curMove.y);
        Debug.Log(appliedMove.y);
        Debug.Log("--------");
    }







    //------------------------------------------


    //Jump physics
    private void setupJumpVar()
    {
        float timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initJumpVelo = (2 * maxJumpHeight) / timeToApex;
    }

    //Jump
    private void JumpHandle()
    {
        if (!isJumping && charController.isGrounded && isJumpPressed)
        {
            animator.SetBool(isJumpHash,true);
            isJumpAnim = true;
            isJumping = true;
            appliedMove.y = initJumpVelo;
        }
        else if (!isJumpPressed && isJumping && charController.isGrounded)
        {
            isJumping = false;
        }
    }

    private void onJumpInput(InputAction.CallbackContext context)
    {
        isJumpPressed = context.ReadValueAsButton();
        setupJumpVar();//to be removed after testing
    }

    //movement run walk
    private void onMoveInput(InputAction.CallbackContext context)
    {
        curMoveInput = context.ReadValue<Vector2>();
        curMove.x = curMoveInput.x * runSpeed;
        curMove.z = curMoveInput.y * runSpeed;

        isMovePressed = curMoveInput.x != 0 || curMoveInput.y != 0;
    }

    //roation
    private void RotHandle()
    {
        Vector3 posLookAt;

        posLookAt.x = curMove.x;
        posLookAt.y = 0.0f;
        posLookAt.z = curMove.z;

        Quaternion curRot = transform.rotation;

        if (isMovePressed) {
            Quaternion targetRot = Quaternion.LookRotation(posLookAt);
            transform.rotation = Quaternion.Slerp(curRot, targetRot, rotFactorPerFrame * Time.deltaTime);
        }

    }

    //animation
    private void AnimHandle()
    {
        bool isRunning = animator.GetBool(isRunHash);
        //bool isJumping = animator.GetBool(isJumpHash);

        if (isMovePressed && !isRunning) { animator.SetBool(isRunHash, true); }
        if (!isMovePressed && isRunning) { animator.SetBool(isRunHash, false); }

        //if (isJumpPressed && !isJumping) { animator.SetBool("isJump", true); }
        //if (!isJumpPressed && isJumping) { animator.SetBool("isJump", false); }
    }

    private void GravityHandle()
    {
        isFalling = appliedMove.y <= 0.0f || !isJumpPressed;


        if (charController.isGrounded)
        {
            if (isJumpAnim)
            {
                animator.SetBool(isJumpHash, false);
                isJumpAnim = false;
            }
            appliedMove.y = groundedGravity;
            
        }
        else if(isFalling)
        {
            float previousYVelo = curMove.y;
            curMove.y = Mathf.Max(curMove.y + (gravity * fallSpeed * Time.deltaTime));
            appliedMove.y = (previousYVelo + curMove.y) * .5f;
            
        }
        else
        {
            float previousYVelo = curMove.y;
            curMove.y = curMove.y + (gravity * Time.deltaTime);
            appliedMove.y = (previousYVelo + curMove.y) * .5f;
            
        }


    }
}
