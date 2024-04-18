using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackState : MonsterState
{
    float _targetAngle;

    float _currentStateTime;

    public MonsterAttackState(StateMachine<MonsterStateName, MonsterController> stateMachine) : base(stateMachine)
    {
    }

    public override void OnEnter()
    {
        Controller.Animator.CrossFadeInFixedTime("Attack", 0.1f);

        _currentStateTime = 0;

        Controller.NavMeshAgent.stoppingDistance = 0f;
        Controller.NavMeshAgent.destination = Controller.transform.position;

        Vector3 targetDirection = Controller.target.transform.position - Controller.transform.position;
        _targetAngle = Mathf.Atan2(targetDirection.x, targetDirection.z) * Mathf.Rad2Deg;
    }


    public override void Update()
    {
        _currentStateTime += Time.deltaTime;

        if (!Controller.target)
        {
            StateMachine.ChangeState(MonsterStateName.Move);
            return;
        }

        float currentAngle = Controller.transform.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(currentAngle, _targetAngle, ref Controller.rotationVelocity, Controller.rotationSmoothTime);
        Controller.transform.rotation = Quaternion.Euler(0, angle, 0);

        if (_currentStateTime > Controller.attackDelay)
        {
            StateMachine.ChangeState(MonsterStateName.Chase);
            return;
        }
    }
}
