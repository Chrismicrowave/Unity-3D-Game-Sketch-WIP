using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine curContext,
        PlayerStateFactory playerStateFactory)
        : base(curContext, playerStateFactory)
    {

    }

    public override void EnterState() {
        Ctx.Animator.SetBool(Ctx.IsRunHash, false);
        Ctx.Animator.SetBool(Ctx.IsWalkHash, false);
        Ctx.Animator.SetBool(Ctx.IsFallHash, false);
        Ctx.Animator.SetBool(Ctx.IsJumpHash, false);
        Ctx.CurMoveX = 0;
        Ctx.CurMoveZ = 0;
    }

    public override void UpdateState() {
        CheckSwitchStates();
    }

    public override void ExitState() { }

    public override void CheckSwitchStates() {
        if (Ctx.IsMovePressed && Ctx.IsWalkPressed)
        {
            SwitchState(Factory.Walk());

        }else if (Ctx.IsMovePressed){
            SwitchState(Factory.Run());
        }
    }

    public override void InitializeSubState() { }
}
