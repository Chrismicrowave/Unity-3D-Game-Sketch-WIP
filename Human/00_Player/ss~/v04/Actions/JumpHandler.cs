using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpHandler 
{
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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


    public Vector3 Jump(Vector3 CurMove, float InitJumpVelo,bool IsJumpPressed,bool IsGrounded,bool IsJumping )
    {
        if (IsJumpPressed && IsGrounded && IsJumping)
        {
            CurMove.y = InitJumpVelo;
            return CurMove;
        }
        else { return CurMove; }

    }


    public bool IsAfterApex(Vector3 CurMove, bool IsJumping, bool IsJumpPressed) {

        bool _isAfterJumpFall = CurMove.y <= -0.0f && IsJumping && !IsJumpPressed;
        return _isAfterJumpFall;
    }

}
