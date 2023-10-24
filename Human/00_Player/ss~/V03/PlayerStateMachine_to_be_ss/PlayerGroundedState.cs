using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(PlayerStateMachine curContext,
        PlayerStateFactory playerStateFactory)
        :base (curContext, playerStateFactory)
    {
        IsRootState = true;
        InitializeSubState();
    }

    public override void EnterState() {
        Ctx.CurMoveY = Ctx.GroundedGravity;
    }

    public override void UpdateState() {
        CheckSwitchStates();
    }

    public override void ExitState() { }

    public override void CheckSwitchStates() {
        if (Ctx.IsJumpPressed)
        {
            SwitchState(Factory.Jump());
        }
        else if (!Ctx.CharController.isGrounded)
        {
            SwitchState(Factory.Fall());
        }
    }

    public override void InitializeSubState() {
        if (!Ctx.IsMovePressed && !Ctx.IsWalkPressed)
        {
            SetSubState(Factory.Idle());
        }else if (Ctx.IsMovePressed && !Ctx.IsWalkPressed)
        {
            SetSubState(Factory.Run());
        }
        else
        {
            SetSubState(Factory.Walk());
        }
    }
}
