using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class MonsterMoveState : MonsterState
{
    public MonsterMoveState(StateMachine<MonsterStateName, MonsterController> stateMachine) : base(stateMachine)
    {
    }

    public override void OnEnter()
    {
        Controller.Animator.CrossFadeInFixedTime("Move", 0.1f);

        Controller.target = null;

        Controller.NavMeshAgent.stoppingDistance = 0f;
        Vector2 movePos = Random.insideUnitCircle * Controller.moveRadius;
        Controller.NavMeshAgent.destination = Controller.firstSpawnPos + new Vector3(movePos.x, 0f, movePos.y);
        Controller.lastDestination = Controller.NavMeshAgent.destination;

        Controller.hud.gameObject.SetActive(false);
    }

    public override void Update()
    {
        if (Controller.FindTarget() == true)
        {
            StateMachine.ChangeState(MonsterStateName.Chase);
            return;
        }

        if (Controller.NavMeshAgent.remainingDistance <= Controller.NavMeshAgent.stoppingDistance)
        {
            if (!Controller.NavMeshAgent.hasPath || Controller.NavMeshAgent.velocity.sqrMagnitude == 0f)
            {
                StateMachine.ChangeState(MonsterStateName.Idle);
                return;
            }
        }
    }
}
