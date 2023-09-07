using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeaLoux.Entity
{
    public abstract class AI_DefendState : AI_State
    {
        protected AI_DefendState(AI ai, AI_StateMachine stateMachine, AI_Data baseData, EntityData data, string animBoolName) : base(ai, stateMachine, baseData, data, animBoolName)
        {
        }
    }
}

