using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : State<PlayerStateName, PlayerController>
{
    public PlayerState(StateMachine<PlayerStateName, PlayerController> stateMachine) : base(stateMachine)
    {
    }
}
