using System;

public abstract class State<StateName, StateController> where StateName : Enum where StateController : class
{
    public StateMachine<StateName, StateController> StateMachine { get; private set; }
    public StateController Controller { get { return StateMachine.Controller; } }

    public State(StateMachine<StateName, StateController> stateMachine)
    {
        StateMachine = stateMachine;
    }

    public virtual void OnEnter() { }
    public virtual void OnExit() { }

    public virtual void Update() { }
    public virtual void FixedUpdate() { }
    public virtual void LateUpdate() { }
}