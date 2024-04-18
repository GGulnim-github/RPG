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
        Controller.Animator.CrossFadeInFixedTime("Damage", 0.1f);
    }
}
