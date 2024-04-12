using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerWalkState : PlayerGroundState
{
    public PlayerWalkState(StateMachine<PlayerStateName, PlayerController> stateMachine) : base(stateMachine)
    {
    }

    public override void OnEnter()
    {
        Controller.Animator.CrossFadeInFixedTime("Walk_front@loop", 0.1f);
        Controller.targetSpeed = Controller.walkSpeed;
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
                if (Controller.Inputs.isWalk == false)
                {
                    StateMachine.ChangeState(PlayerStateName.Run);
                    return true;
                }
            }
        }

        return false;
    }
}
