using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

namespace DeaLoux.AI
{
    public abstract class AI_State : CoreSystems.Patterns.IState
    {
        protected AI_StateMachine _stateMachine;
        protected AI _ai;
        protected AI_Data _aiData;
        protected AI_SpecificData _aiSData;

        public float _startTime { get; protected set; }

        protected string _animBoolName;

        public AI_State(AI ai, AI_StateMachine stateMachine, AI_Data aiData, AI_SpecificData aiSData, string animBoolName)
        {
            _ai = ai;
            _stateMachine = stateMachine;
            _animBoolName = animBoolName;
            _aiSData = aiSData;
            _aiData = aiData;
        }

        public virtual void Enter()
        {
            _startTime = Time.time;
            _ai.Anim.SetBool(_animBoolName, true);

            Tick();
        }

        public virtual void Exit()
        {
            _ai.Anim.SetBool(_animBoolName, false);
        }
        public virtual void LogicUpdate()
        {

        }

        public virtual void PhysicsUpdate()
        {
            Tick();
        }

        public virtual void Tick()
        {

        }

        public virtual void ChangeStateSH(AI_State state) => _stateMachine.ChangeState(state);
    }
}

