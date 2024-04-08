using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StateMachine<StateName, StateController> where StateName : Enum where StateController : class
{
    public StateController Controller { get; private set; }
    public State<StateName, StateController> CurrentState { get; private set; }

    [field: SerializeField] public StateName CurrentStateName { get; private set; }
    [field: SerializeField] public StateName PreviousStateName { get; private set; }

    Dictionary<StateName, State<StateName, StateController>> _states = new();

    public StateMachine(StateController controller)
    {
        Controller = controller;
    }

    public void Update()
    {
        CurrentState?.Update();
    }

    public void FixedUpdate()
    {
        CurrentState?.FixedUpdate();
    }

    public void LateUpdate()
    {
        CurrentState?.LateUpdate();
    }

    public void AddState(StateName stateName, State<StateName, StateController> state)
    {
        if (_states.ContainsKey(stateName) == false)
        {
            _states.Add(stateName, state);
        }
    }

    public void DeleteState(StateName stateName)
    {
        if (_states.ContainsKey(stateName) == true)
        {
            _states.Remove(stateName);
        }
    }

    public void ChangeState(StateName stateName, bool first = false)
    {
        if (_states.ContainsKey(stateName) == false)
        {
            return;
        }

        PreviousStateName = CurrentStateName;

        CurrentState?.OnExit();
        CurrentState = _states[stateName];
        CurrentState?.OnEnter();

        CurrentStateName = stateName;
    }
}
