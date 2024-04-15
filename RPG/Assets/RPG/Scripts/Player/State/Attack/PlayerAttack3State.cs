using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack3State : PlayerAttackState
{
    public PlayerAttack3State(StateMachine<PlayerStateName, PlayerController> stateMachine) : base(stateMachine)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();

        Controller.EquipSword();
        Controller.Animator.CrossFadeInFixedTime("WGS_attackA3", 0.1f);

        Controller.Attack.offset = new Vector3(0f, 0.5f, 0f);
        Controller.Attack.radius = 1.2f;
        Controller.Attack.damage = Controller.Stat.currentAttack * 3;
    }

    public override void Update()
    {
        UpdateLastAttack("WGS_attackA3");
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
