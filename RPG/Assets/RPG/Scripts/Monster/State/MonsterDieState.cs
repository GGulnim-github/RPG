using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDieState : MonsterState
{
    float _currentStateTime;

    public MonsterDieState(StateMachine<MonsterStateName, MonsterController> stateMachine) : base(stateMachine)
    {
    }

    public override void OnEnter()
    {
        Controller.Animator.CrossFadeInFixedTime("Die", 0.1f);
        Controller.hud.gameObject.SetActive(false);
        Controller.Collider.isTrigger = true;

        _currentStateTime = 0;
    }

    public override void Update()
    {
        _currentStateTime += Time.deltaTime;

        if (_currentStateTime > Controller.reviveTime)
        {
            Controller.Revive();
        }
    }
}
