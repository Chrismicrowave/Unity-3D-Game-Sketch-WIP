using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(PlayerStateMachine curContext,
        PlayerStateFactory playerStateFactory)
        : base(curContext, playerStateFactory)
    {
        IsRootState = true;
        InitializeSubState();
    }

    public override void EnterState() {
        JumpHandle();
    }

    public override void UpdateState() {
        GravityHandle();
        CheckSwitchStates();
    }

    public override void ExitState() {
        Ctx.Animator.SetBool(Ctx.IsJumpHash, false);
        //if (_ctx.IsJumpPressed)
        //{
        //    _ctx.RequireNewJumpPressed = true;
        //}
    }

    public override void CheckSwitchStates() {
        if (Ctx.CharController.isGrounded) //&& !_ctx.RequireNewJumpPressed)
        {
            SwitchState(Factory.Grounded());
        }
    }

    public override void InitializeSubState() {
        if (!Ctx.IsMovePressed && !Ctx.IsWalkPressed)
        {
            SetSubState(Factory.Idle());
        }
        else if (Ctx.IsMovePressed && !Ctx.IsWalkPressed)
        {
            SetSubState(Factory.Run());
        }
        else
        {
            SetSubState(Factory.Walk());
        }
    }

    private void JumpHandle()
    {
        Ctx.Animator.SetBool(Ctx.IsJumpHash, true);
        Ctx.IsJumping = true;
        Ctx.CurMoveY = Ctx.InitJumpVelo * .5f;
    }

    private void GravityHandle()
    {

        Ctx.IsAfterJumpFalling = Ctx.CurMoveY <= -0.0f || !Ctx.IsJumpPressed;


        if (Ctx.IsAfterJumpFalling)
        {
            float previousYVelo = Ctx.CurMoveY;
            float newYVelo = Ctx.CurMoveY + (Ctx.Gravity * Ctx.FallSpeed * Time.deltaTime);
            float nextYVelo = Mathf.Max((previousYVelo + newYVelo) * .5f, -20.0f);
            Ctx.CurMoveY = nextYVelo;
        }
        else
        {
            float previousYVelo = Ctx.CurMoveY;
            float newYVelo = Ctx.CurMoveY + (Ctx.Gravity * Time.deltaTime);
            float nextYVelo = (previousYVelo + newYVelo) * .5f;
            Ctx.CurMoveY = nextYVelo;
        }
    }
}
