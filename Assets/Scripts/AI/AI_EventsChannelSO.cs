using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace DeaLoux.Entity
{
    using CoreSystems.ScriptableObjects;

    [CreateAssetMenu(menuName = "Events/AI Events Channel")]
    public class AI_EventsChannelSO : DescriptionBaseSO
    {
        Dictionary<AI_EventTypes, UnityAction> events = new Dictionary<AI_EventTypes, UnityAction>();

        public void Subscribe(AI_EventTypes type, UnityAction a)
        {
            events[type] += a;
        }

        public void RaiseEvent(AI_EventTypes type)
        {
            events[type].Invoke();
        }
    }

    public enum AI_EventTypes
    {
        OnMoving,
        OnWallReached,
        OnDoneIdling,
        OnAttack,
        OnDoneAttacking,
        OnDefend,
        OnDoneDefending,
    }
}