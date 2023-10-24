using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpHandler : MonoBehaviour
{
    public static PlayerJumpHandler Instance;

    
    [SerializeField] private float _maxJumpHeight = 1.0f;
    [SerializeField] private float _maxJumpTime = 0.5f;

    private float _initJumpVelo;

    private void Awake()
    {
        if (Instance == null)
        { Instance = this; }
        else
        {
            Destroy(gameObject);
            return;
        }

        SetupJumpVar();
    }

    void Start()
    {

    }


    public bool IsJumpingCheck(bool IsGrounded, bool IsJumpPressed, bool IsJumping)
    {
        if (!IsJumping && IsGrounded && IsJumpPressed)//start
        {
            IsJumping = true;
            return IsJumping;
        }
        else if (IsJumping && !IsGrounded)//airtime
        {
            IsJumping = true;
            return IsJumping;
        }
        else if (IsJumping && IsGrounded)//touch down
        {
            IsJumping = false;
            return IsJumping;
        }
        else
        {
            return IsJumping;
        }
    }

  
    public bool IsAfterApex(Vector3 CurMove, bool IsJumping, bool IsJumpPressed)
    {

        bool _isAfterJumpFall = CurMove.y <= -0.0f && IsJumping && !IsJumpPressed;
        return _isAfterJumpFall;
    }


    //Jump physics
    private void SetupJumpVar()
    {
        float timeToApex = _maxJumpTime / 2;
        //_gravity = (-2 * _maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        _initJumpVelo = (2 * _maxJumpHeight) / timeToApex * .5f;
    }

    public void InitJumpGravity()
    {
        float timeToApex = _maxJumpTime / 2;
        PlayerMovement.CurMove.y = (-2 * _maxJumpHeight) / Mathf.Pow(timeToApex, 2);
    }

    //Jump
    public bool IsReadyJump()
    {
        bool IsJumpPressed = PlayerInputManager.IsJumpPressed;
        bool IsGrounded = PlayerMovement.Instance.IsGrounded();
        bool IsJumping = PlayerState.Instance.IsJumping;

        //Debug.Log(IsJumpPressed + "," + IsGrounded + "," + IsJumping);

        if (IsJumpPressed && IsGrounded && IsJumping) { return true; }
        else { return false; }
    }

    public void Jump()
    {
        PlayerMovement.CurMove.y = _initJumpVelo;
    }

}

