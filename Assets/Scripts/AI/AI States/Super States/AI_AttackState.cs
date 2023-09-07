using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeaLoux.Entity
{
    public abstract class AI_AttackState : AI_State
    {
        protected AI_AttackState(AI ai, AI_StateMachine stateMachine, AI_Data baseData, EntityData data, string animBoolName) : base(ai, stateMachine, baseData, data, animBoolName)
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
        }
    }
}

