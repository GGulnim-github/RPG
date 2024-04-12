using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerGroundState
{
    public PlayerRunState(StateMachine<PlayerStateName, PlayerController> stateMachine) : base(stateMachine)
    {
    }

    public override void OnEnter()
    {
        Controller.Animator.CrossFadeInFixedTime("RunA_front@loop", 0.1f);        
        Controller.targetSpeed = Controller.runSpeed;
    }

    public override bool CheckMoveInput()
    {
        if (Controller.Inputs.move == Vector2.zero)
        {
            StateMachine.ChangeState(PlayerStateName.Idle);
            return true;
        }
        else
        {
            if (Controller.Inputs.dash == true)
            {
                StateMachine.ChangeState(PlayerStateName.Dash);
                return true;
            }
            else
            {
                if (Controller.Inputs.isWalk == true)
                {
                    StateMachine.ChangeState(PlayerStateName.Walk);
                    return true;
                }
            }
        }

        return false;
    }
}
