using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeaLoux.AI
{
    public class E1_IdleState : AI_IdleState
    {
        private E1 enemy1;

        public E1_IdleState(AI ai, AI_StateMachine stateMachine, AI_Data aiData, AI_SpecificData aiSData, string animBoolName, E1 _enemy1) : base(ai, stateMachine, aiData, aiSData, animBoolName)
        {
            enemy1 = _enemy1;
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

            if (_idleTimeOver)
            {
                ChangeStateSH(enemy1.MoveState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void Tick()
        {
            base.Tick();
        }
    }
}