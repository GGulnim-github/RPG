using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MonsterStateMachine : StateMachine<MonsterStateName, MonsterController>
{
    public MonsterStateMachine(MonsterController controller) : base(controller)
    {
        AddState(MonsterStateName.Idle, new MonsterIdleState(this));
        AddState(MonsterStateName.Move, new MonsterMoveState(this));
        AddState(MonsterStateName.Chase, new MonsterChaseState(this));
        AddState(MonsterStateName.Attack, new MonsterAttackState(this));
        AddState(MonsterStateName.Damage, new MonsterDamageState(this));
        AddState(MonsterStateName.Die, new MonsterDieState(this));

        ChangeState(MonsterStateName.Idle);
    }
}
