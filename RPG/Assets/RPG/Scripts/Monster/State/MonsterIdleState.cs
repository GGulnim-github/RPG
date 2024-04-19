using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterIdleState : MonsterState
{
    float _currentStateTime;
    float _exitTime;

    public MonsterIdleState(StateMachine<MonsterStateName, MonsterController> stateMachine) : base(stateMachine)
    {
    }

    public override void OnEnter()
    {
        if (StateMachine.PreviousStateName == MonsterStateName.Die)
        {
            Controller.Animator.Play("Idle");
        }
        else
        {
            Controller.Animator.CrossFadeInFixedTime("Idle", 0.1f);
        }

        Controller.target = null;

        _currentStateTime = 0;
        _exitTime = Random.Range(Controller.idleExitMinTime, Controller.idleExitMaxTime);

        Controller.NavMeshAgent.stoppingDistance = 0f;
        Controller.NavMeshAgent.destination = Controller.transform.position;
        
        Controller.hud.gameObject.SetActive(false);
    }

    public override void Update()
    {
        _currentStateTime += Time.deltaTime;

        if (Controller.FindTarget() == true)
        {
            StateMachine.ChangeState(MonsterStateName.Chase);
            return;
        }

        if (_currentStateTime > _exitTime)
        {
            StateMachine.ChangeState(MonsterStateName.Move);
            return;
        }
    }
}
