using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundState
{
    public PlayerIdleState(StateMachine<PlayerStateName, PlayerController> stateMachine) : base(stateMachine)
    {
    }

    public override void OnEnter()
    {
        if (StateMachine.PreviousStateName == PlayerStateName.Dash)
        {
            Controller.Animator.CrossFadeInFixedTime("Dash_ToStandC", 0.1f);
        }
        else if (StateMachine.PreviousStateName == PlayerStateName.Fall)
        {
            Controller.Animator.CrossFadeInFixedTime("Jump_ToStandC", 0.1f);
        }
        else
        {
            Controller.Animator.CrossFadeInFixedTime("StandA@loop", 0.1f);
        }

        Controller.targetSpeed = 0.0f;
    }

    public override bool CheckMoveInput()
    {
        if (Controller.Inputs.move != Vector2.zero)
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
