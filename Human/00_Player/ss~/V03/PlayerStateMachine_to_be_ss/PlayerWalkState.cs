using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
    public PlayerWalkState(PlayerStateMachine curContext,
        PlayerStateFactory playerStateFactory)
        : base(curContext, playerStateFactory)
    {

    }


    public override void EnterState() {
        Ctx.Animator.SetBool(Ctx.IsRunHash, true);
        Ctx.Animator.SetBool(Ctx.IsWalkHash, true);
    }

    public override void UpdateState() {
        CheckSwitchStates();
        //Ctx.CurMoveX = Ctx.CurMoveX * Ctx.WalkSpeed;
        //Ctx.CurMoveZ = Ctx.CurMoveZ * Ctx.WalkSpeed;
    }

    public override void ExitState() {
        Ctx.Animator.SetBool(Ctx.IsWalkHash, false);
    }

    public override void CheckSwitchStates() {
        if (!Ctx.IsMovePressed)
        {
            SwitchState(Factory.Idle());
        }
        else if (Ctx.IsMovePressed && !Ctx.IsWalkPressed)
        {
            SwitchState(Factory.Run());
        }
    }

    public override void InitializeSubState() { }

    
}
