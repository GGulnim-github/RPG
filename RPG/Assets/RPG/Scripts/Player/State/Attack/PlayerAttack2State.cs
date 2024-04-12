using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack2State : PlayerAttackState
{
    public PlayerAttack2State(StateMachine<PlayerStateName, PlayerController> stateMachine) : base(stateMachine)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();

        Controller.EquipSword();
        Controller.Animator.Play("WGS_attackA2");
    }

    public override void Update()
    {
        UpdateAttack("WGS_attackA2", PlayerStateName.Attack3);
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
