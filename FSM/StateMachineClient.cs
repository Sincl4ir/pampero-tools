using System;
using UnityEngine;

namespace Pampero.FSM
{
    /// <summary>
    /// Base class for objects that use a state machine.
    /// </summary>
    public abstract class StateMachineClient : MonoBehaviour
    {
        protected StateMachine _stateMachine;

        protected virtual void Start()
        {
            _stateMachine = new StateMachine();
            SetUpStateMachine();
        }

        protected virtual void Update()
        {
            _stateMachine?.Update();
        }

        protected virtual void FixedUpdate()
        {
            _stateMachine?.FixedUpdate();
        }

        /// <summary>
        /// Sets up the state machine with transitions and states.
        /// </summary>
        protected abstract void SetUpStateMachine();

        /// <summary>
        /// Adds a transition from one state to another state based on a condition predicate.
        /// </summary>
        protected void At(IState from, IState to, IPredicate condition) => _stateMachine?.AddTransition(from, to, condition);
        /// <summary>
        /// Adds a transition from any state to a specific state based on a condition predicate.
        /// </summary>
        protected void Any(IState to, IPredicate condition) => _stateMachine?.AddAnyTransition(to, condition);
        
        protected bool CanTransitionToState(IState state) => _stateMachine.CanTransitionToState(state);
        protected bool CanTransitionToState(Type stateType) => _stateMachine.CanTransitionToState(stateType);
        /// <summary>
        /// // Returns true if the current state is the same as the given state
        /// </summary>
        public bool IsInState(Type state)
        {
            return _stateMachine?.Current.State.GetType() == state;
        }
    }
}
//EOF.