using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeaLoux.Entity
{
    public class X_JumpState : AI_BasicActionState
    {
        private X _x;

        public X_JumpState(AI ai, AI_StateMachine stateMachine, AI_Data baseData, EntityData data, string animBoolName, X x) : base(ai, stateMachine, baseData, data, animBoolName)
        {
            _x = x;
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            //_x.AerialState.Jumping();
            _x.MoveVertically(_data.maxJumpVelocity);
            isActionDone = true;
        }
    }
}