using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    public PlayerAttackState(StateMachine<PlayerStateName, PlayerController> stateMachine) : base(stateMachine)
    {
    }

    public override void OnEnter()
    {
        Controller.isAttack = true;
        Controller.EquipSword();
        UpdateTargetRotation();
    }

    public override void OnExit()
    {
        Controller.Inputs.attack = false;
        Controller.isAttack = false;
        Controller.UnequipSword();
    }

    public void UpdateAttack(string animationName, PlayerStateName nextState)
    {
        if (Controller.Animator.GetCurrentAnimatorStateInfo(0).IsName(animationName))
        {
            float animationNormalizedTime = Controller.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            
            if (animationNormalizedTime < 0.5f)
            {
                Controller.Inputs.attack = false;
            }
            else if (animationNormalizedTime > 0.99f)
            {
                if (Controller.Inputs.attack == true)
                {
                    StateMachine.ChangeState(nextState);
                }
                else
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
        }

        Rotate(false);
    }

    public void UpdateLastAttack(string animationName)
    {
        if (Controller.Animator.GetCurrentAnimatorStateInfo(0).IsName(animationName))
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
}
