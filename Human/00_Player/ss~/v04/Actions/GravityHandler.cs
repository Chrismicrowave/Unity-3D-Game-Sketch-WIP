using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityHandler
{
    

    public Vector3 Rise(Vector3 CurMove,float Gravity)
    {
        float previousYVelo = CurMove.y;
        float newYVelo = CurMove.y + (Gravity * Time.deltaTime);
        float nextYVelo = (previousYVelo + newYVelo) * .5f;
        CurMove.y = nextYVelo;
        return CurMove;
    }

    public Vector3 Fall(Vector3 CurMove, float Gravity, float FallSpeed)
    {
        float previousYVelo = CurMove.y;
        float newYVelo = CurMove.y + (Gravity * FallSpeed * Time.deltaTime);
        float nextYVelo = Mathf.Max((previousYVelo + newYVelo) * .5f, -20.0f);
        CurMove.y = nextYVelo;
        return CurMove;
    }

    public Vector3 Grounded(Vector3 CurMove, float GroundedGravity)
    {
        CurMove.y = GroundedGravity;
        return CurMove;
    }
}
