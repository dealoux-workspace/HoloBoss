using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeaLoux.AI
{
    public class AI_IdleState : AI_State
    {
        public bool _idleTimeOver { get; protected set; }
        public bool _flipAfterIdle { get; protected set; }
        public float _idleTime { get; protected set; }

        public AI_IdleState(AI ai, AI_StateMachine stateMachine, AI_Data aiData, AI_SpecificData aiSData, string animBoolName) : base(ai, stateMachine, aiData, aiSData, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();

            _idleTimeOver = false;
            SetRandomIdleTime();
        }

        public override void Exit()
        {
            base.Exit();

            if (_flipAfterIdle)
            {
                _ai.Flip();
            }
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (Time.time >= _startTime + _idleTime)
            {
                _idleTimeOver = true;
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            _ai.SetVelocityX(0.0f);
        }

        public override void Tick()
        {
            base.Tick();
        }

        private void SetRandomIdleTime()
        {
            _idleTime = Random.Range(_aiSData.minIdleTime, _aiSData.maxIdleTime);
        }

        public void Flip() => _flipAfterIdle = true;
    }
}