using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeaLoux.Entity
{
    public abstract class AI_RestState : AI_State
    {
        public float _restTime { get; protected set; }
        public bool _restTimeOver { get; protected set; }

        protected AI_RestState(AI ai, AI_StateMachine stateMachine, AI_Data baseData, EntityData data, string animBoolName) : base(ai, stateMachine, baseData, data, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();

            _restTimeOver = false;
            SetRandomIdleTime();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (Time.time >= _startTime + _restTime)
            {
                _restTimeOver = true;
                //RaiseEvent(AI_EventTypes.OnDoneIdling);
            }
        }

        void SetRandomIdleTime()
        {
            _restTime = Random.Range(_baseData.minIdleTime, _baseData.maxIdleTime);
        }
    }
}

