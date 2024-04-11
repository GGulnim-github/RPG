using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerStateMachine : StateMachine<PlayerStateName, PlayerController>
{
    public PlayerStateMachine(PlayerController controller) : base(controller)
    {
        AddState(PlayerStateName.Idle, new PlayerIdleState(this));
        AddState(PlayerStateName.Walk, new PlayerWalkState(this));
        AddState(PlayerStateName.Run, new PlayerRunState(this));
        AddState(PlayerStateName.Dash, new PlayerDashState(this));
        AddState(PlayerStateName.Jump, new PlayerJumpState(this));
        AddState(PlayerStateName.Fall, new PlayerFallState(this));

        AddState(PlayerStateName.Attack1, new PlayerAttack1State(this));
        AddState(PlayerStateName.Attack2, new PlayerAttack2State(this));
        AddState(PlayerStateName.Attack3, new PlayerAttack3State(this));
        AddState(PlayerStateName.Attack4, new PlayerAttack4State(this));
        AddState(PlayerStateName.Attack5, new PlayerAttack5State(this));

        ChangeState(PlayerStateName.Idle, true);
    }
}
