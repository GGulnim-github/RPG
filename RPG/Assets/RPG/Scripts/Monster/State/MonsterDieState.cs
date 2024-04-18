using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDieState : MonsterState
{
    public MonsterDieState(StateMachine<MonsterStateName, MonsterController> stateMachine) : base(stateMachine)
    {
    }

    public override void OnEnter()
    {
        Controller.Animator.CrossFadeInFixedTime("Die", 0.1f);
    }
}
