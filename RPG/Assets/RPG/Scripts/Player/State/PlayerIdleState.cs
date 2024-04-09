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
        Controller.Animator.CrossFadeInFixedTime("StandA@loop", 0.1f);
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

        if (Controller.Inputs.move != Vector2.zero)
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

        Move();
    }
}
