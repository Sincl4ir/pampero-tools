using System;
using System.Collections.Generic;

namespace Pampero.FSM
{
    public class StateMachine
    {
        public StateNode Current => _current;

        private StateNode _current;
        private Dictionary<Type, StateNode> _nodes = new();
        private HashSet<ITransition> _anyTransitions = new();

        public void Update()
        {
            var transition = GetTransition();
            if (transition != null)
            {
                ChangeState(transition.TargetState);
            }

            _current.State?.Update();
        }

        public void FixedUpdate()
        {
            _current.State?.FixedUpdate();
        }

        public void SetState(IState state)
        {
            _current = _nodes[state.GetType()];
            _current.State?.OnEnter();
        }

        private void ChangeState(IState state)
        {
            if (state == _current.State) { return; }

            var previousState = _current.State;
            var nextState = _nodes[state.GetType()].State;

            previousState?.OnExit();
            nextState?.OnEnter();
            _current = _nodes[state.GetType()];
        }

        private ITransition GetTransition()
        {
            foreach (var transition in _anyTransitions)
            {
                if (!transition.Condition.Evaluate()) { continue; }
                return transition;
            }

            foreach (var transition in _current.Transitions)
            {
                if (!transition.Condition.Evaluate()) { continue; }
                return transition;
            }

            return null;
        }

        public void AddTransition(IState from, IState to, IPredicate condition)
        {
            GetOrAddNode(from).AddTransition(GetOrAddNode(to).State, condition);
        }

        public void AddAnyTransition(IState to, IPredicate condition)
        {
            _anyTransitions.Add(new Transition(GetOrAddNode(to).State, condition));
        }

        private StateNode GetOrAddNode(IState state)
        {
            var node = _nodes.GetValueOrDefault(state.GetType());

            if (node == null)
            {
                node = new StateNode(state);
                _nodes.Add(state.GetType(), node);
            }

            return node;
        }
        
        public bool CanTransitionToState(IState targetState)
        {
            // Check any-state transitions
            foreach (var transition in _anyTransitions)
            {
                if (transition.TargetState.GetType() == targetState.GetType())
                    return true;
            }

            // Check current state transitions
            if (_current != null)
            {
                foreach (var transition in _current.Transitions)
                {
                    if (transition.TargetState.GetType() == targetState.GetType())
                        return true;
                }
            }

            return false;
        }
        
        public bool CanTransitionToState(Type targetStateType)
        {
            // Check any-state transitions
            foreach (var transition in _anyTransitions)
            {
                if (transition.TargetState.GetType() == targetStateType)
                    return true;
            }
            
            // Check current state transitions
            if (_current != null)
            {
                foreach (var transition in _current.Transitions)
                {
                    if (transition.TargetState.GetType() == targetStateType)
                        return true;
                }
            }

            return false;
        }

        public class StateNode
        {
            public IState State { get; }
            public HashSet<ITransition> Transitions { get; }

            public StateNode(IState state)
            {
                State = state;
                Transitions = new HashSet<ITransition>();
            }

            public void AddTransition(IState to, IPredicate condition)
            {
                Transitions.Add(new Transition(to, condition));
            }
        }
    }
}
//EOF.