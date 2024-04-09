using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(StateMachine<PlayerStateName, PlayerController> stateMachine) : base(stateMachine)
    {
    }

    public override void OnEnter()
    {
        Controller.Animator.CrossFadeInFixedTime("Jump_StandToUp", 0.1f);
        Controller.Inputs.jump = false;
        Controller.verticalVelocity = Mathf.Sqrt(Controller.jumpHeight * -2f * Controller.gravity);
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

        //Rotate(false);
        InAir();
    }
}
