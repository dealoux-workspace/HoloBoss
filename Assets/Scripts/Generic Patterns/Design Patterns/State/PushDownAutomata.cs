using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeaLoux.CoreSystems.Patterns
{
    public abstract class PushDownAutomata<T> : MonoBehaviour where T : IState
    {
        private Stack<T> States;

        private void Awake()
        {
            States = new Stack<T>();
        }

        public T CurrState => States.Peek();

        public void Initialize(T startingState)
        {
            States.Push(startingState);
            CurrState.Enter();
        }

        public void ChangeState(T newState, bool savePrevious = false)
        {
            CurrState.Exit();
            if (!savePrevious)
                States.Pop();

            States.Push(newState);
            CurrState.Enter();
        }

        public void ChangeToPreviousState(T backUpState = default)
        {
            if (States.Count > 1)
            {
                CurrState.Exit();
                States.Pop();
            }
            else
                States.Push(backUpState);

            try
            {
                CurrState.Enter();
            }
            catch
            {
                Debug.LogWarning("No previous state available");
            }

        }
    }
}