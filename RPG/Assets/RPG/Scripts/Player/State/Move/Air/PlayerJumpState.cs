using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAirState
{
    public PlayerJumpState(StateMachine<PlayerStateName, PlayerController> stateMachine) : base(stateMachine)
    {
    }

    public override void OnEnter()
    {
        Controller.Animator.CrossFadeInFixedTime("Jump_Fly@loop", 0.1f);
        
        Controller.Inputs.jump = false;
        Controller.verticalVelocity = Mathf.Sqrt(Controller.jumpHeight * -2f * Controller.gravity);
    }

    public override void Update()
    {
        if (Controller.verticalVelocity < 0)
        {
            StateMachine.ChangeState(PlayerStateName.Fall);
            return;
        }
    }
}
