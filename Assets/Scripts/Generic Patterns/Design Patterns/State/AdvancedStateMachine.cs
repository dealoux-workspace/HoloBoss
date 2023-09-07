using System;
using System.Collections.Generic;
using UnityEngine;

namespace DeaLoux.CoreSystems.Patterns
{
    public class AdvancedStateMachine : MonoBehaviour
    {
        private IState currState;

        private Dictionary<Type, List<Transition>> transitions = new Dictionary<Type, List<Transition>>();
        private List<Transition> currTransitions = new List<Transition>();
        private List<Transition> anyTransitions = new List<Transition>();

        private static List<Transition> emptyTransitions = new List<Transition>(0);

        public void LogicUpdate()
        {
            var transitions = GetTransitions();

            if (transitions != null)
            {
                SetState(transitions.To);
            }

            currState?.LogicUpdate();
        }

        public void SetState(IState state)
        {
            if (state == currState) return;

            currState?.Exit();
            currState = state;

            transitions.TryGetValue(currState.GetType(), out currTransitions);

            if (currTransitions == null)
            {
                currTransitions = emptyTransitions;
            }

            currState.Enter();
        }

        public void AddTransition(IState from, IState to, Func<bool> predicate)
        {
            if (transitions.TryGetValue(from.GetType(), out var _transitions) == false)
            {
                _transitions = new List<Transition>();
                transitions[from.GetType()] = _transitions;
            }

            _transitions.Add(new Transition(to, predicate));
        }

        public void AddAnyTransition(IState state, Func<bool> predicate)
        {
            anyTransitions.Add(new Transition(state, predicate));
        }

        private class Transition
        {
            public Func<bool> Condition { get; }
            public IState To { get; }

            public Transition(IState to, Func<bool> condition)
            {
                To = to;
                Condition = condition;
            }
        }

        private Transition GetTransitions()
        {
            foreach (var transition in anyTransitions)
                if (transition.Condition())
                    return transition;

            foreach (var transition in currTransitions)
                if (transition.Condition())
                    return transition;

            return null;
        }
    }

    /*
    public class AdvancedStateMachine<T> : MonoBehaviour where T : IState
    {
        private T currState;

        private Dictionary<Type, List<Transition>> transitions = new Dictionary<Type, List<Transition>>();
        private List<Transition> currTransitions = new List<Transition>();
        private List<Transition> anyTransitions = new List<Transition>();

        private static List<Transition> emptyTransitions = new List<Transition>(0);

        public void LogicUpdate()
        {
            var transitions = GetTransitions();

            if (transitions != null)
            {
                SetState(transitions.To);
            }

            currState?.LogicUpdate();
        }

        public void PhysicsUpdate()
        {
            var transitions = GetTransitions();

            if (transitions != null)
            {
                SetState(transitions.To);
            }

            currState?.PhysicsUpdate();
        }

        public void SetState(T state)
        {
            if (Comparer<T>.Default.Compare(state, currState) == 0) return;

            currState?.Exit();
            currState = state;

            transitions.TryGetValue(currState.GetType(), out currTransitions);

            if (currTransitions == null)
            {
                currTransitions = emptyTransitions;
            }

            currState.Enter();
        }

        public void AddTransition(T from, T to, Func<bool> predicate)
        {
            if (transitions.TryGetValue(from.GetType(), out var _transitions) == false)
            {
                _transitions = new List<Transition>();
                transitions[from.GetType()] = _transitions;
            }

            _transitions.Add(new Transition(to, predicate));
        }

        public void AddAnyTransition(T state, Func<bool> predicate)
        {
            anyTransitions.Add(new Transition(state, predicate));
        }

        private class Transition
        {
            public Func<bool> Condition { get; }
            public T To { get; }

            public Transition(T to, Func<bool> condition)
            {
                To = to;
                Condition = condition;
            }
        }

        private Transition GetTransitions()
        {
            foreach (var transition in anyTransitions)
                if (transition.Condition())
                    return transition;

            foreach (var transition in currTransitions)
                if (transition.Condition())
                    return transition;

            return null;
        }
    } */
}

