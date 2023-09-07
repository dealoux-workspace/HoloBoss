using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeaLoux.Entity
{
    public class X_IdleState : AI_IdleState
    {
        private X _x;

        public X_IdleState(AI ai, AI_StateMachine stateMachine, AI_Data baseData, EntityData data, string animBoolName, X x) : base(ai, stateMachine, baseData, data, animBoolName)
        {
            _x = x;
        }

        public override void Enter()
        {
            base.Enter();

            if(_x.EmptySequence())
            {
                _x.GetSequence();
            }
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (_restTimeOver)
            {
                ChangeStateSH(_x.NextStateInSequence());
            }
        }

    }
}