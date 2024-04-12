using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerGroundState
{
    public PlayerDashState(StateMachine<PlayerStateName, PlayerController> stateMachine) : base(stateMachine)
    {
    }

    public override void OnEnter()
    {
        Controller.Animator.CrossFadeInFixedTime("Dash_Front@loop", 0.1f);
        Controller.targetSpeed = Controller.dashSpeed;
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
            if (Controller.Inputs.dash == false)
            {
                if (Controller.Inputs.isWalk == true)
                {
                    StateMachine.ChangeState(PlayerStateName.Walk);
                    return true;
                }
                else
                {
                    StateMachine.ChangeState(PlayerStateName.Run);
                    return true;
                }
            }
        }

        return false;
    }
}
