using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace DeaLoux.AI
{
    public class E1 : AI
    {
        #region State Variables
        public E1_IdleState IdleState { get; private set; }
        public E1_MoveState MoveState { get; private set; }
        #endregion

        #region Other Variables
        [SerializeField]
        Data.AI_SpecificData e1Data;
        #endregion

        public override void Awake()
        {
            base.Awake();

            IdleState = new E1_IdleState(this, StateMachine, aIData, e1Data, "idle", this);
            MoveState = new E1_MoveState(this, StateMachine, aIData, e1Data, "move", this);

            /*
            StateMachine.AddTransition(IdleState, MoveState, () => Time.time >= IdleState._startTime + IdleState._idleTime);
            StateMachine.AddTransition(MoveState, IdleState, () => WallReached() || !LedgeReached());
            StateMachine.SetState(MoveState); */

            StateMachine.Initialize(MoveState);
        }
    }
}