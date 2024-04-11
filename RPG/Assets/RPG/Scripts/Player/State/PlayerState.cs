using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : State<PlayerStateName, PlayerController>
{
    public PlayerState(StateMachine<PlayerStateName, PlayerController> stateMachine) : base(stateMachine)
    {
    }

    public void Rotate(bool useInput = true)
    {
        if (useInput)
        {
            Controller.targetRotation = Mathf.Atan2(Controller.Inputs.moveDirection.x, Controller.Inputs.moveDirection.z) * Mathf.Rad2Deg + Controller.cameraTransform.eulerAngles.y;
        }
  
        float rotation = Mathf.SmoothDampAngle(Controller.transform.eulerAngles.y, Controller.targetRotation, ref Controller.rotationVelocity, Controller.rotationSmoothTime);
        Controller.transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
    }

    public void Move()
    {
        Vector3 targetDirection = Quaternion.Euler(0.0f, Controller.targetRotation, 0.0f) * Vector3.forward;   
        Controller.CharacterController.Move(Controller.targetSpeed * Time.deltaTime * targetDirection.normalized
            + new Vector3(0.0f, Controller.verticalVelocity, 0.0f) * Time.deltaTime);
    }

    public void InAir()
    {
        Controller.verticalVelocity += Controller.gravity * Time.deltaTime;

        Vector3 targetDirection = Quaternion.Euler(0.0f, Controller.targetRotation, 0.0f) * Vector3.forward;
        Controller.CharacterController.Move(Controller.targetSpeed * Time.deltaTime * targetDirection.normalized
            + new Vector3(0.0f, Controller.verticalVelocity, 0.0f) * Time.deltaTime);
    }

    public void UpdateTargetRotation()
    {
        if (Controller.Inputs.move != Vector2.zero)
        {
            Controller.targetRotation = Mathf.Atan2(Controller.Inputs.moveDirection.x, Controller.Inputs.moveDirection.z) * Mathf.Rad2Deg + Controller.cameraTransform.eulerAngles.y;
        }
    }
}
