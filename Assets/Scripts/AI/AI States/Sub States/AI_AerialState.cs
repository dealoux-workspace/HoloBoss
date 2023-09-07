using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeaLoux.Entity
{
    public class AI_AerialState : AI_RestState
    {
        protected int dir;

        public AI_AerialState(AI ai, AI_StateMachine stateMachine, AI_Data baseData, EntityData data, string animBoolName) : base(ai, stateMachine, baseData, data, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
            dir = 0;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            _ai.Anim.SetFloat("yVelocity", _ai.Controller.velocity.y);

            _ai.MoveHorizontally(_data.movementSpeed * dir);

            if (_grounded && _ai.Controller.velocity.y < 0.01f)
            {
                ChangeStateSH(_ai.IdleState);
            }
        }

        public void SetDir(int value = 0) => dir = value;
    }
}