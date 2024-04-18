using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterChaseState : MonsterState
{
    public MonsterChaseState(StateMachine<MonsterStateName, MonsterController> stateMachine) : base(stateMachine)
    {
    }

    public override void OnEnter()
    {
        Controller.Animator.CrossFadeInFixedTime("Chase", 0.1f);

        Controller.NavMeshAgent.stoppingDistance = Controller.attackDistance;
        Controller.NavMeshAgent.destination = Controller.target.transform.position;
    }

    public override void Update()
    {
        if (!Controller.target)
        {
            StateMachine.ChangeState(MonsterStateName.Move);
            return;
        }

        if (Controller.IsPointWithinRadius(Controller.spawnPos, Controller.chaseRadius, Controller.target.transform.position) == false)
        {
            StateMachine.ChangeState(MonsterStateName.Move);
            return;
        }

        if (Controller.NavMeshAgent.remainingDistance <= Controller.NavMeshAgent.stoppingDistance)
        {
            if (Controller.NavMeshAgent.velocity.sqrMagnitude == 0f)
            {
                StateMachine.ChangeState(MonsterStateName.Attack);
                return;
            }
        }

        Controller.NavMeshAgent.destination = Controller.target.transform.position;
    }
}
