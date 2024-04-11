using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
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
        else if (StateMachine.PreviousStateName == PlayerStateName.Attack5)
        {
            Controller.Animator.CrossFadeInFixedTime("WGS_attackA5toStand", 0.1f);
        }
        else
        {
            Controller.Animator.CrossFadeInFixedTime("StandA@loop", 0.1f);
        }

        Controller.targetSpeed = 0.0f;
    }

    public override void Update()
    {
        if (Controller.isGrounded)
        {
            if (Controller.Inputs.jump == true)
            {
                StateMachine.ChangeState(PlayerStateName.Jump);
                return;
            }
        }
        else
        {
            StateMachine.ChangeState(PlayerStateName.Fall);
            return;
        }

        if (Controller.Inputs.attack == true)
        {
            StateMachine.ChangeState(PlayerStateName.Attack1);
            return;
        }

        if (Controller.Inputs.move != Vector2.zero)
        {
            if (Controller.Inputs.dash == true)
            {
                StateMachine.ChangeState(PlayerStateName.Dash);
                return;
            }
            else
            {
                if (Controller.Inputs.isWalk == true)
                {
                    StateMachine.ChangeState(PlayerStateName.Walk);
                    return;
                }
                else
                {
                    StateMachine.ChangeState(PlayerStateName.Run);
                    return;
                }
            }
        }

        Rotate(false);
        Move();
    }
}
