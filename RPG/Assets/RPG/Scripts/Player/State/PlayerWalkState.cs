using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerWalkState : PlayerState
{
    public PlayerWalkState(StateMachine<PlayerStateName, PlayerController> stateMachine) : base(stateMachine)
    {
    }

    public override void OnEnter()
    {
        Controller.Animator.CrossFadeInFixedTime("Walk_front@loop", 0.1f);
        Controller.targetSpeed = Controller.walkSpeed;
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

        if (Controller.Inputs.move == Vector2.zero)
        {
            StateMachine.ChangeState(PlayerStateName.Idle);
            return;
        }
        else
        {
            if (Controller.Inputs.isWalk == false)
            {
                StateMachine.ChangeState(PlayerStateName.Run);
                return;
            }
        }

        Rotate();
        Move();
    }
}
