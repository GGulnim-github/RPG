using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerState
{
    public PlayerRunState(StateMachine<PlayerStateName, PlayerController> stateMachine) : base(stateMachine)
    {
    }

    public override void OnEnter()
    {
        Controller.Animator.CrossFadeInFixedTime("RunA_front@loop", 0.1f);
        
        Controller.targetSpeed = Controller.runSpeed;
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
            }
        } 

        Rotate();
        Move();
    }
}
