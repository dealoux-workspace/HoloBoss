using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeaLoux.Entity
{
    public class X_ShootState : AI_AttackState
    {
        private X _x;

        public X_ShootState(AI ai, AI_StateMachine stateMachine, AI_Data baseData, EntityData data, string animBoolName, X x) : base(ai, stateMachine, baseData, data, animBoolName)
        {
            _x = x;
        }

        public override void Enter()
        {
            base.Enter();
            _x.DefaultHitPoint();
            _x.PrimAttack();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if(_animFinished)
                ChangeStateSH(_x.IdleState);
        }
    }
}