using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpHandler
{
    private bool _isGrounded;
    private Vector3 _curMove;
    private bool _isJumpPressed;
    private bool _isJumping;
    private float _initJumpVelo;


    //required var from PlayerController for later ref
    public JumpHandler(Vector3 CurMove, float InitJumpVelo, bool IsJumpPressed, bool IsGrounded, bool IsJumping)
    {
        _curMove = CurMove;
        _isGrounded = IsGrounded;
        _isJumpPressed = IsJumpPressed;
        _isJumping = IsJumping;
        _initJumpVelo = InitJumpVelo;
    }


    public bool IsJumpingCheck()
    {
        if (!_isJumping && _isGrounded && _isJumpPressed)//start
        {
            _isJumping = true;
            return _isJumping;
        }
        else if (_isJumping && !_isGrounded)//airtime
        {
            _isJumping = true;
            return _isJumping;
        }
        else if (_isJumping && _isGrounded)//touch down
        {
            _isJumping = false;
            return _isJumping;
        }
        else
        {
            return _isJumping;
        }
    }


    public Vector3 Jump()
    {
        Debug.LogFormat("in: {0}",_curMove);

        if (!(_isJumpPressed && _isGrounded && _isJumping))
        {
            return _curMove;
            Debug.LogFormat("out: {0}", _curMove);
        }
        else {
            _curMove.y = _initJumpVelo;
            return _curMove;
            Debug.LogFormat("out2222: {0}", _curMove);
        }

        return _curMove;
        Debug.LogFormat("out3333: {0}", _curMove);

    }


    public bool IsAfterApexCheck() {

        bool _isAfterJumpFall = _curMove.y <= -0.0f && _isJumping;
        return _isAfterJumpFall;
    }

}
