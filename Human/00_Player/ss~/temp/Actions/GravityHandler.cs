using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityHandler
{
    private Vector3 _curMove;
    private float _gravity;
    private float _groundedGravity;
    private float _fallSpeed;

    public GravityHandler(Vector3 CurMove, float Gravity, float GroundedGravity, float FallSpeed)
    {
        _curMove = CurMove;
        _gravity = Gravity;
        _groundedGravity = GroundedGravity;
        _fallSpeed = FallSpeed;
    }

    public float GravityRise()
    {
        float previousYVelo = _curMove.y;
        float newYVelo = _curMove.y + (_gravity * Time.deltaTime);
        float nextYVelo = (previousYVelo + newYVelo) * .5f;
        _curMove.y = nextYVelo;
        return _curMove.y;
    }

    public float GravityFall()
    {
        
        float previousYVelo = _curMove.y;
        float newYVelo = _curMove.y + (_gravity * _fallSpeed * Time.deltaTime);
        float nextYVelo = Mathf.Max((previousYVelo + newYVelo) * .5f, -20.0f);
        _curMove.y = nextYVelo;
        return _curMove.y;
    }

    public float GravityGrounded()
    {
        _curMove.y = _groundedGravity;
        return _curMove.y;
    }
}
