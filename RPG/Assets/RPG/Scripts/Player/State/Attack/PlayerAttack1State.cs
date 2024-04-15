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

        Controller.Attack.offset = new Vector3(0f, 0.5f, 0.65f);
        Controller.Attack.radius = 0.65f;
        Controller.Attack.damage = Controller.Stat.currentAttack;
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
