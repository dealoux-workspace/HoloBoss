using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeaLoux.Entity
{
    public class AI_IdleState : AI_RestState
    {
        public bool _flipAfterIdle { get; protected set; }

        public AI_IdleState(AI ai, AI_StateMachine stateMachine, AI_Data baseData, EntityData data, string animBoolName) : base(ai, stateMachine, baseData, data, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();

            if (_flipAfterIdle)
            {
                _flipAfterIdle = false;
                _ai.Flip();
            }
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }

        public void Flip() => _flipAfterIdle = true;
    }
}