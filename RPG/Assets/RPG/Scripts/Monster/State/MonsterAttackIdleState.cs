using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackIdleState : MonsterState
{
    float _currentStateTime;

    public MonsterAttackIdleState(StateMachine<MonsterStateName, MonsterController> stateMachine) : base(stateMachine)
    {
    }

    public override void OnEnter()
    {
        Controller.Animator.CrossFadeInFixedTime("AttackIdle", 0.1f);

        _currentStateTime = 0;
    }

    public override void Update()
    {
        _currentStateTime += Time.deltaTime;

        if (_currentStateTime > Controller.attackDelay)
        {
            StateMachine.ChangeState(MonsterStateName.Chase);
            return;
        }
    }
}
