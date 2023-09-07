using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeaLoux.Entity
{
    public class X_MoveState : AI_MoveState
    {
        private X _x;

        public X_MoveState(AI ai, AI_StateMachine stateMachine, AI_Data baseData, EntityData data, string animBoolName, X x) : base(ai, stateMachine, baseData, data, animBoolName)
        {
            _x = x;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            _x.MoveHorizontally(_data.movementSpeed * _data.facingDir);

            if (_wallDetected || !_ledgeDetected)
            {
                _x.IdleState.Flip();
                ChangeStateSH(_x.IdleState);
            }
        }
    }
}