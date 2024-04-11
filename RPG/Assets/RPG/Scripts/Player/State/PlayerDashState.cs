using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(StateMachine<PlayerStateName, PlayerController> stateMachine) : base(stateMachine)
    {
    }

    public override void OnEnter()
    {
        Controller.Animator.CrossFadeInFixedTime("Dash_Front@loop", 0.1f);
        Controller.targetSpeed = Controller.dashSpeed;
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

        if (Controller.Inputs.move == Vector2.zero)
        {
            StateMachine.ChangeState(PlayerStateName.Idle);
            return;
        }
        else
        {
            if (Controller.Inputs.dash == false)
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

        Rotate();
        Move();
    }
}
