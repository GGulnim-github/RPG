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
    }

    public override void Update()
    {
        if (Controller.Inputs.move == Vector2.zero)
        {
            StateMachine.ChangeState(PlayerStateName.Idle);
        }

        Rotate();
        Move();
    }

    void Rotate()
    {
        Controller.targetRotation = Mathf.Atan2(Controller.Inputs.moveDirection.x, Controller.Inputs.moveDirection.z) * Mathf.Rad2Deg + Controller.cameraTarget.eulerAngles.y;
        float rotation = Mathf.SmoothDampAngle(Controller.transform.eulerAngles.y, Controller.targetRotation, ref Controller.rotationVelocity, Controller.rotationSmoothTime);
        Controller.transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
    }

    void Move()
    {
        Vector3 targetDirection = Quaternion.Euler(0.0f, Controller.targetRotation, 0.0f) * Vector3.forward;
        Controller.CharacterController.Move(Controller.AnimatorDeltaPosition);
    }
}
