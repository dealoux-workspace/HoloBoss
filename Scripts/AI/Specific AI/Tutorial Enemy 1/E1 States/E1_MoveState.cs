using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeaLoux.AI
{
    public class E1_MoveState : AI_MoveState
    {
        private E1 enemy1;

        public E1_MoveState(AI ai, AI_StateMachine stateMachine, AI_Data aiData, AI_SpecificData aiSData, string animBoolName, E1 _enemy1) : base(ai, stateMachine, aiData, aiSData, animBoolName)
        {
            enemy1 = _enemy1;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            _ai.SetVelocityX(_aiSData.movementSpeed);

            if (_wallDetected || !_ledgeDetected)
            {
                enemy1.IdleState.Flip();
                ChangeStateSH(enemy1.IdleState);
            }
        }
    }
}