using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeaLoux.Entity
{
    public abstract class AI_BasicActionState : AI_State
    {
        protected bool isActionDone;

        protected AI_BasicActionState(AI ai, AI_StateMachine stateMachine, AI_Data baseData, EntityData data, string animBoolName) : base(ai, stateMachine, baseData, data, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();

            isActionDone = false;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (isActionDone)
            {
                if (_grounded && _ai.Controller.velocity.y < 0.01f)
                {
                    ChangeStateSH(_ai.IdleState);
                }

                else
                {
                    ChangeStateSH(_ai.AerialState);
                }
            }
        }
    }
}