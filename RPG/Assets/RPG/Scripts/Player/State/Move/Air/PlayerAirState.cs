using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(StateMachine<PlayerStateName, PlayerController> stateMachine) : base(stateMachine)
    {
    }
}