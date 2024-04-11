using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack5State : PlayerState
{
    public PlayerAttack5State(StateMachine<PlayerStateName, PlayerController> stateMachine) : base(stateMachine)
    {
    }

    public override void OnEnter()
    {
        Controller.Animator.CrossFadeInFixedTime("WGS_attackA5", 0.1f);
        Controller.upperChestSword.SetActive(false);
        Controller.handSword.SetActive(true);

        Controller.Inputs.attack = false;
        Controller.isAttack = true;

        UpdateTargetRotation();
    }

    public override void Update()
    {
        if (Controller.Animator.GetCurrentAnimatorStateInfo(0).IsName("WGS_attackA5"))
        {
            float animationNormalizedTime = Controller.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            if (animationNormalizedTime > 0.99f)
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
        }

        Rotate(false);
    }

    public override void OnExit()
    {
        Controller.upperChestSword.SetActive(true);
        Controller.handSword.SetActive(false);
        Controller.isAttack = false;
    }
}
