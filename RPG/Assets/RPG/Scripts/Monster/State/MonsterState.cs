using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterState : State<MonsterStateName, MonsterController>
{
    public MonsterState(StateMachine<MonsterStateName, MonsterController> stateMachine) : base(stateMachine)
    {
    }
}
