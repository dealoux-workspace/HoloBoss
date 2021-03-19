using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeaLoux.AI
{
    public class AI_MoveState : AI_State
    {
        protected bool _wallDetected;
        protected bool _ledgeDetected;

        public AI_MoveState(AI ai, AI_StateMachine stateMachine, AI_Data aiData, AI_SpecificData aiSData, string animBoolName) : base(ai, stateMachine, aiData, aiSData, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();

            _wallDetected = _ai.WallReached();
            _ledgeDetected = _ai.LedgeReached();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            _wallDetected = _ai.WallReached();
            _ledgeDetected = _ai.LedgeReached();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            _ai.SetVelocityX(_aiSData.movementSpeed);
        }

        public override void Tick()
        {
            base.Tick();
        }
    }
}