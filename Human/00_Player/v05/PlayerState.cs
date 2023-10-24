using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public static PlayerState Instance;
    public static string CurState { get; private set; }

    public static bool IsPlayerInv;
    public static bool IsPlayerInDialogue;
    public static bool IsPlayerInBuy;
    public static bool IsPlayerInSell;
    public static bool IsPlayerWaterSplash;


    public bool IsJumping;
    public bool IsAfterApexFall;

    
    //data holder for private use only, use: PlayerMovement.Instance.IsGrounded();
    private bool _isGrounded;

    [SerializeField] private float _fallThershold = -4.5f;
    

    private void Awake()
    {
        if (Instance == null)
        { Instance = this; }
        else
        {
            Destroy(gameObject);
            return;
        }

        CurState = "IsIdle";
    }

    void Start()
    {
        
    }

    public void StateHandle()
    {
        _isGrounded = PlayerMovement.Instance.IsGrounded();
        IsJumping = PlayerJumpHandler.Instance.IsJumpingCheck(_isGrounded, PlayerInputManager.IsJumpPressed, IsJumping);
        IsAfterApexFall = PlayerJumpHandler.Instance.IsAfterApex(PlayerMovement.CurMove, IsJumping, PlayerInputManager.IsJumpPressed);

        //Jump
        if (IsJumping)
        { CurState = "isJump"; }

        //Run
        else if (_isGrounded && PlayerInputManager.IsMovePressed && !PlayerInputManager.IsWalkPressed)
        { CurState = "isRun"; }

        //Walk
        else if (_isGrounded && PlayerInputManager.IsWalkPressed && PlayerInputManager.IsMovePressed)
        { CurState = "isWalk"; }

        //FallSmall
        else if (!_isGrounded && !IsJumping && (PlayerMovement.CurMove.y > _fallThershold))
        { CurState = "isFallStep"; }

        //Fall
        else if (!_isGrounded && !IsJumping && (PlayerMovement.CurMove.y < _fallThershold))
        { CurState = "isFall"; }

        //Idle
        else
        { CurState = "isIdle"; }
    }
}
