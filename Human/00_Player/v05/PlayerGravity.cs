using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGravity : MonoBehaviour
{
    public static PlayerGravity Instance;

    private Vector3 _curMove;

    [SerializeField] private float _gravity = -9.8f;
    [SerializeField] private float _groundedGravity = -.5f;
    [SerializeField] private float _fallSpeed = 2.0f;
    [SerializeField] private float JumpAirTimeFac = 3.0f;

    private void Awake()
    {
        if (Instance == null)
        { Instance = this; }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void GravityHandle()
    {
        _curMove = PlayerMovement.CurMove;

        ApplyGravity();

        PlayerMovement.CurMove = _curMove;
    }

    //apply gravity as per state
    public void ApplyGravity()
    {
        
        switch (PlayerState.CurState)
        {
            case "isIdle":
            case "isRun":
            case "isWalk":
                _curMove = Grounded(_curMove, _groundedGravity);
                break;


            case "isJump":
                if (PlayerState.Instance.IsAfterApexFall)
                { _curMove = Fall(_curMove, _gravity, _fallSpeed); }

                else if (PlayerState.Instance.IsJumping)
                { _curMove = Rise(_curMove, _gravity); }

                break;


            case "isFallStep":
            case "isFall":
                _curMove = Fall(_curMove, _gravity, _fallSpeed);
                break;

        }
    }


    public Vector3 Rise(Vector3 CurMove, float Gravity)
    {
        float previousYVelo = CurMove.y;
        float newYVelo = CurMove.y + (Gravity * JumpAirTimeFac * Time.deltaTime);
        float nextYVelo = (previousYVelo + newYVelo) * .5f;
        CurMove.y = nextYVelo;
        return CurMove;
    }

    public Vector3 Fall(Vector3 CurMove, float Gravity, float FallSpeed)
    {
        float previousYVelo = CurMove.y;
        float newYVelo = CurMove.y + (Gravity * JumpAirTimeFac * FallSpeed * Time.deltaTime);
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
