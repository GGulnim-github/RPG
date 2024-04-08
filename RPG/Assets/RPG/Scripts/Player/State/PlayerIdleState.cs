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
    }

    public override void Update()
    {
        if (Controller.Inputs.move != Vector2.zero)
        {
            StateMachine.ChangeState(PlayerStateName.Walk);
        }
    }
}
