using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DeaLoux.Entity
{
    public abstract class AI_State : CoreSystems.Patterns.IState
    {
        protected AI_StateMachine _stateMachine;
        protected AI _ai;
        protected AI_Data _baseData;
        protected EntityData _data;
        protected bool _animFinished;

        protected bool _grounded;
        protected bool _wallDetected;
        protected bool _ledgeDetected;

        public float _startTime { get; protected set; }

        protected string _animBoolName;

        public AI_State(AI ai, AI_StateMachine stateMachine, AI_Data baseData, EntityData data, string animBoolName)
        {
            _ai = ai;
            _stateMachine = stateMachine;
            _animBoolName = animBoolName;
            _data = data;
            _baseData = baseData;
        }

        public virtual void Enter()
        {
            _startTime = Time.time;
            _ai.Anim.SetBool(_animBoolName, true);
            _animFinished = false;
        }

        public virtual void Exit()
        {
            _ai.Anim.SetBool(_animBoolName, false);
        }

        public virtual void LogicUpdate()
        {
            Checks();
        }

        protected void Checks()
        {
            _wallDetected = _ai.WallTouched();
            //_ledgeDetected = _ai.LedgeReached();
            _grounded = _ai.Grounded();
        }

        protected void ChangeStateSH(AI_State state, bool savePrevious = false) => _stateMachine.ChangeState(state, savePrevious);
        protected void RaiseEvent(AI_EventTypes type) => _baseData.EventChannel.RaiseEvent(type);
        protected void Subscribe(AI_EventTypes type, UnityAction a) => _baseData.EventChannel.Subscribe(type, a);
        public void AnimFinishTrigger() => _animFinished = true;
    }
}

