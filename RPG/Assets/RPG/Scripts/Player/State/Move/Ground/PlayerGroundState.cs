using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundState : PlayerState
{
    public PlayerGroundState(StateMachine<PlayerStateName, PlayerController> stateMachine) : base(stateMachine)
    {
    }

    public override void Update()
    {
        if (CheckGround() == false) return;
        if (CheckAttack() == true) return;
        if (CheckMoveInput() == true) return;

        Controller.UpdateTargetRotation();
    }

    public bool CheckGround()
    {
        if (Controller.isGrounded)
        {
            if (Controller.Inputs.jump == true)
            {
                StateMachine.ChangeState(PlayerStateName.Jump);
                return false;
            }
        }
        else
        {
            StateMachine.ChangeState(PlayerStateName.Fall);
            return false;
        }

        return true;
    }

    public bool CheckAttack()
    {
        if (Controller.Inputs.attack == true)
        {
            StateMachine.ChangeState(PlayerStateName.Attack1);
            return true;
        }

        return false;
    }

    public virtual bool CheckMoveInput()
    {
        return true;
    }
}
