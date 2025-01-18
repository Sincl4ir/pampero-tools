namespace Pampero.FSM
{ 
    /// <summary>
    /// Base class for a state in a state machine.
    /// </summary>
    public abstract class BaseState : IState
    {
        public virtual void OnEnter() { }
        public virtual void Update() { }
        public virtual void FixedUpdate() { }
        public virtual void OnExit() { }
    }
}
//EOF.