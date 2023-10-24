using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerBaseState
{
    public PlayerFallState(PlayerStateMachine curContext,
        PlayerStateFactory playerStateFactory)
        : base(curContext, playerStateFactory)
    {
        IsRootState = true;
        InitializeSubState();
    }

    public override void EnterState()
    {
        
        
    }

    public override void UpdateState()
    {

        LateAnim();
        GravityHandle();
        CheckSwitchStates();
        
    }

    public override void ExitState() {

        Ctx.Animator.SetBool(Ctx.IsFallHash, false);
        Ctx.CurMoveY = Ctx.GroundedGravity;
    }

    public override void CheckSwitchStates()
    {
        if (Ctx.CharController.isGrounded)
        {
            SwitchState(Factory.Grounded());
        }
    }

    public override void InitializeSubState()
    {
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

    private void GravityHandle()
    {
        float previousYVelo = Ctx.CurMoveY;
        float newYVelo = Ctx.CurMoveY + (Ctx.Gravity * Ctx.FallSpeed * Time.deltaTime);
        float nextYVelo = Mathf.Max((previousYVelo + newYVelo) * .5f, -20.0f);
        Ctx.CurMoveY = nextYVelo;
    }

    private void LateAnim()
    {
        if (Ctx.CurMoveY < Ctx.FallThershold)
        {
            Ctx.Animator.SetBool(Ctx.IsFallHash, true);
        }
    }

}

