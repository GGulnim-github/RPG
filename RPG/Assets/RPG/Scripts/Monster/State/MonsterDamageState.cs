using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDamageState : MonsterState
{
    public MonsterDamageState(StateMachine<MonsterStateName, MonsterController> stateMachine) : base(stateMachine)
    {
    }

    public override void OnEnter()
    {
        Controller.Animator.Play("Damage");
    }

    public override void Update()
    {
        if (Controller.Animator.GetCurrentAnimatorStateInfo(0).IsName("Damage"))
        {
            float animationNormalizedTime = Controller.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            if (animationNormalizedTime > 0.99f)
            {
                StateMachine.ChangeState(MonsterStateName.Chase);
            }
        }
    }
}
