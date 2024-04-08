using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine<PlayerStateName, PlayerController>
{
    public PlayerStateMachine(PlayerController controller) : base(controller)
    {
        AddState(PlayerStateName.Idle, new PlayerIdleState(this));
        AddState(PlayerStateName.Walk, new PlayerWalkState(this));
        AddState(PlayerStateName.Run, new PlayerRunState(this));

        ChangeState(PlayerStateName.Idle, true);
    }
}
