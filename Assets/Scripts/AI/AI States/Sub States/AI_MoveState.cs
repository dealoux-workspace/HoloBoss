using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeaLoux.Entity
{
    public class AI_MoveState : AI_State
    {
        public AI_MoveState(AI ai, AI_StateMachine stateMachine, AI_Data baseData, EntityData data, string animBoolName) : base(ai, stateMachine, baseData, data, animBoolName)
        {
        }
    }
}