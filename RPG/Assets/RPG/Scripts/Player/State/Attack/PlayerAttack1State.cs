using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack1State : PlayerAttackState
{
    public PlayerAttack1State(StateMachine<PlayerStateName, PlayerController> stateMachine) : base(stateMachine)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();

        Controller.EquipSword();
        Controller.Animator.CrossFadeInFixedTime("WGS_attackA1", 0.1f);


    }

    public override void Update()
    {
        UpdateAttack("WGS_attackA1", PlayerStateName.Attack2);
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
