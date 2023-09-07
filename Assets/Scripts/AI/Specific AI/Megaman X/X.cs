using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace DeaLoux.Entity
{
    public class X : AI
    {
        #region State Variables
        public X_ShootState ShootState { get; private set; }
        public X_JumpState JumpState { get; private set; }
        #endregion

        protected override void Awake()
        {
            base.Awake();

            IdleState = new X_IdleState(this, StateMachine, baseData, data, "idle", this);
            MoveState = new X_MoveState(this, StateMachine, baseData, data, "move", this);
            JumpState = new X_JumpState(this, StateMachine, baseData, data, "aerial", this);
            ShootState = new X_ShootState(this, StateMachine, baseData, data, "idleShoot", this);

            AerialState = new AI_AerialState(this, StateMachine, baseData, data, "aerial");
            HurtState = new AI_HurtState(this, StateMachine, baseData, data, "hurt");
        }

        protected override void Start()
        {
            base.Start();

            StateMachine.Initialize(IdleState);
        }

        public void GetSequence()
        {
            //Debug.Log(stateSequence);
            switch (Decide())
            {
                case RelativePos.far_onLevel:
                    stateSequence.Enqueue(ShootState);
                    stateSequence.Enqueue(ShootState);
                    break;

                case RelativePos.close_onLevel:
                    int jumpDir = WallTouched() ? data.facingDir : -data.facingDir;
                    AerialState.SetDir(jumpDir);
                    stateSequence.Enqueue(JumpState);
                    stateSequence.Enqueue(ShootState);
                    break;

                default:
                    stateSequence.Enqueue(IdleState);
                    break;
            }
        }

        // Animation trigger
        protected override void AnimFinishTrigger() => StateMachine.CurrState.AnimFinishTrigger();

        // Handing hitpoint's position, rotation
        public override void DefaultHitPoint() => base.DefaultHitPoint();
        protected override void DefaultHitPointWall() => base.DefaultHitPointWall();
        protected override void UpHitPoint() => base.UpHitPoint();
        protected override void DownHitPoint() => base.DownHitPoint();
        protected override void DownDiagHitPoint() => base.DownDiagHitPoint();

        protected override void GetHitPoint() => base.GetHitPoint();
    }
}