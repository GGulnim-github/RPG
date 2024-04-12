using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerState
{
    public PlayerFallState(StateMachine<PlayerStateName, PlayerController> stateMachine) : base(stateMachine)
    {
    }

    public override void OnEnter()
    {
        Controller.Animator.CrossFadeInFixedTime("Jump_Down@loop", 0.1f);
    }

    public override void Update()
    {
        if (Controller.isGrounded)
        {
            if (Controller.Inputs.move == Vector2.zero)
            {
                StateMachine.ChangeState(PlayerStateName.Idle);
                return;
            }
            else
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
        }

        InAir();
    }
}
