using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeaLoux.Entity
{
    public class AI_HurtState : AI_State
    {
        public AI_HurtState(AI ai, AI_StateMachine stateMachine, AI_Data baseData, EntityData data, string animBoolName) : base(ai, stateMachine, baseData, data, animBoolName)
        {
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

            if (_animFinished)
                _stateMachine.ChangeToPreviousState();
        }
    }
}