using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerState
{
    public PlayerRunState(StateMachine<PlayerStateName, PlayerController> stateMachine) : base(stateMachine)
    {
    }
}
