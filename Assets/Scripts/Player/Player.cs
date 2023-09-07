using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace DeaLoux.Entity
{
    using CoreSystems.Collision;
    using DeaLoux.CoreSystems.Controller;

    public class Player : Entity
    {
        #region State Variables
        // Generic States
        public PlayerStateMachine StateMachine { get; private set; }
        public PlayerIdleState IdleState { get; private set; }
        public PlayerMoveState MoveState { get; private set; }
        public PlayerJumpState JumpState { get; private set; }
        public PlayerDashState DashState { get; private set; }
        public PlayerHurtState HurtState { get; private set; }
        public PlayerAerialState AerialState { get; private set; }
        public PlayerLandingState LandingState { get; private set; }
        public PlayerWallSlideState WallSlideState { get; private set; }
        public PlayerWallJumpState WallJumpState { get; private set; }

        public PlayerAtkChargedState AtkChargedState { get; private set; }

        // Primary Attack
        public PlayerPrimAtkIdle4State PrimAtkIdle4State { get; private set; }
        public PlayerAtkIdle8State PrimAtkIdle8State { get; private set; }
        public PlayerAtkWallChargedState PrimAtkWallChargedState { get; private set; }
        public PlayerAtkMoveState PrimAtkMoveState { get; private set; }
        public PlayerAtkDashState PrimAtkDashState { get; private set; }
        public PlayerAtkAerial4State PrimAtkAerial4State { get; private set; }
        public PlayerAtkAerial8State PrimAtkAerial8State { get; private set; }
        public PlayerAtkWallState PrimAtkWallState { get; private set; }

        // Secondary Attack
        public PlayerSecAtkIdle4State SecAtkIdle4State { get; private set; }
        public PlayerAtkIdle8State SecAtkIdle8State { get; private set; }
        public PlayerAtkWallChargedState SecAtkWallChargedState { get; private set; }
        public PlayerAtkMoveState SecAtkMoveState { get; private set; }
        public PlayerAtkDashState SecAtkDashState { get; private set; }
        public PlayerAtkAerial4State SecAtkAerial4State { get; private set; }
        public PlayerAtkAerial8State SecAtkAerial8State { get; private set; }
        public PlayerAtkWallState SecAtkWallState { get; private set; }
        #endregion

        #region Components
        public PlayerInputHandler InputHandler { get; private set; }
        #endregion

        #region Other Variables
        [SerializeField]
        protected PlayerData playerData;

        [SerializeField]
        Transform DirectionIndicator;
        #endregion

        #region Unity Callback Functions
        protected override void Awake()
        {
            base.Awake();

            // StateMachine init
            StateMachine = gameObject.AddComponent<PlayerStateMachine>();
            IdleState = new PlayerIdleState(this, StateMachine, data, playerData, "idle");
            MoveState = new PlayerMoveState(this, StateMachine, data, playerData, "move");
            JumpState = new PlayerJumpState(this, StateMachine, data, playerData, "aerial");
            DashState = new PlayerDashState(this, StateMachine, data, playerData, "dash");
            HurtState = new PlayerHurtState(this, StateMachine, data, playerData, "hurt");
            AerialState = new PlayerAerialState(this, StateMachine, data, playerData, "aerial");
            LandingState = new PlayerLandingState(this, StateMachine, data, playerData, "landing");
            WallSlideState = new PlayerWallSlideState(this, StateMachine, data, playerData, "wallSlide");
            WallJumpState = new PlayerWallJumpState(this, StateMachine, data, playerData, "wallJump");

            AtkChargedState = new PlayerAtkChargedState(this, StateMachine, data, playerData, "automata");

            // Primary Attack
            PrimAtkIdle4State = new PlayerPrimAtkIdle4State(this, StateMachine, data, playerData, "primAtkIdle4Way");
            PrimAtkIdle8State = new PlayerAtkIdle8State(this, StateMachine, data, playerData, "primAtkIdle8Way");
            PrimAtkMoveState = new PlayerAtkMoveState(this, StateMachine, data, playerData, "primAtkMove");
            PrimAtkDashState = new PlayerAtkDashState(this, StateMachine, data, playerData, "primAtkDash");
            PrimAtkAerial4State = new PlayerAtkAerial4State(this, StateMachine, data, playerData, "primAtkAerial4Way");
            PrimAtkAerial8State = new PlayerAtkAerial8State(this, StateMachine, data, playerData, "primAtkAerial8Way");
            PrimAtkWallChargedState = new PlayerAtkWallChargedState(this, StateMachine, data, playerData, "primAtkWallCharged");
            PrimAtkWallState = new PlayerAtkWallState(this, StateMachine, data, playerData, "primAtkWall");

            // Secondary Attack
            SecAtkIdle4State = new PlayerSecAtkIdle4State(this, StateMachine, data, playerData, "secAtkIdle4Way");
            SecAtkIdle8State = new PlayerAtkIdle8State(this, StateMachine, data, playerData, "secAtkIdle8Way");
            SecAtkMoveState = new PlayerAtkMoveState(this, StateMachine, data, playerData, "secAtkMove");
            SecAtkDashState = new PlayerAtkDashState(this, StateMachine, data, playerData, "secAtkDash");
            SecAtkAerial4State = new PlayerAtkAerial4State(this, StateMachine, data, playerData, "secAtkAerial4Way");
            SecAtkAerial8State = new PlayerAtkAerial8State(this, StateMachine, data, playerData, "secAtkAerial8Way");
            SecAtkWallChargedState = new PlayerAtkWallChargedState(this, StateMachine, data, playerData, "secAtkWallCharged");
            SecAtkWallState = new PlayerAtkWallState(this, StateMachine, data, playerData, "secAtkWall");
        }

        protected override void Start()
        {
            base.Start();
            InputHandler = GetComponent<PlayerInputHandler>();
            StateMachine.Initialize(IdleState);
        }

        protected override void Update()
        {
            StateMachine.CurrState.LogicUpdate();
            base.Update();
            HandleDirIndicator();
        }


        protected override void OnTriggerCollidingEvent(Collider2D collider)
        {
            //ContactPoint2D[] contacts = new ContactPoint2D[2];

            switch (collider.gameObject.tag)
            {
                case "Enemy":
                    //collider.GetContacts(contacts);

                    Vector2 distance = collider.Distance(Controller.boxCollider).normal;
                    KnockBack(distance);
                    break;
            }
        }

        /*protected override void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log(collision);
            switch (collision.gameObject.tag)
            {
                case "Enemy":
                    StateMachine.ChangeState(DamagedState);
                    break;
            }
        }*/
        #endregion

        #region Raycast Physics Functions / Movement Functions
        public override void KnockBack(float damage = 0)
        {
            base.KnockBack(damage);
            MoveHorizontally(-data.facingDir * data.knockbackDistance);
        }
        public override void KnockBack(Vector2 target, float damage = 0)
        {
            base.KnockBack(target, damage);

            if (data.iframe <= 0)
            {
                TakeDamage(damage);
                data.knockbackDir = target * data.knockbackDistance;
                StateMachine.ChangeState(HurtState);
            }
        }

        protected override void GravityCheck()
        {
            if (Grounded() || StateMachine.CurrState is PlayerAtkChargedState || StateMachine.CurrState is PlayerHurtState)
            {
                Controller.velocity.y = 0f;
            }
        }
        #endregion

        #region Animation Event Functions
        // Animation trigger
        protected override void AnimTrigger() => StateMachine.CurrState.AnimTrigger();
        protected override void AnimFinishTrigger() => StateMachine.CurrState.AnimFinishTrigger();

        // Handing hitpoint's position, rotation
        public override void DefaultHitPoint() => base.DefaultHitPoint();
        protected override void DefaultHitPointWall() => base.DefaultHitPointWall();
        protected override void UpHitPoint() => base.UpHitPoint();
        protected override void DownHitPoint() => base.DownHitPoint();
        protected override void DownDiagHitPoint() => base.DownDiagHitPoint();

        protected override void GetHitPoint() => base.GetHitPoint();

        protected override void GetHitPoint(float angle)
        {
            base.GetHitPoint(angle);
            hitPoint.gameObject.transform.position = hitPoint.pos[angle] * (InputHandler.SecAtkInput ? data.slot2.weaponOffset : data.slot1.weaponOffset) + transform.position;
            hitPoint.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
        #endregion

        #region Other Functions
        void HandleDirIndicator()
        {
            DirectionIndicator.rotation = Quaternion.Euler(0, 0, data.LookAngle - 45f);
        }
        #endregion
    }
}

