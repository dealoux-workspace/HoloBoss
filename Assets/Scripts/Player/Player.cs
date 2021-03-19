using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using DeaLoux.CoreSystems.Collision;

namespace DeaLoux.Player
{
    public class Player : MonoBehaviour
    {
        #region State Variables
        // Generic States
        public PlayerStateMachine StateMachine { get; private set; }
        public PlayerIdleState IdleState { get; private set; }
        public PlayerMoveState MoveState { get; private set; }
        public PlayerJumpState JumpState { get; private set; }
        public PlayerDashState DashState { get; private set; }
        public PlayerDamagedState DamagedState { get; private set; }
        public PlayerAerialState AerialState { get; private set; }
        public PlayerLandingState LandingState { get; private set; }
        public PlayerWallSlideState WallSlideState { get; private set; }
        public PlayerWallJumpState WallJumpState { get; private set; }
        public PlayerWallLeapState WallLeapState { get; private set; }

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
        public Animator Anim { get; private set; }
        public PlayerInputHandler InputHandler { get; private set; }
        #endregion

        #region Raycast Physics
        private float gravity;
        public CharacterController2D PlayerController { get; private set; }
        #endregion

        #region Other Variables
        [SerializeField]
        Data.PlayerData data;
        [SerializeField]
        Transform DirectionIndicator;
        public HitBox hitPoint;
        public event Action<HitBox> AttackDelegate;
        public event Action<HitBox> ChargedAttackDelegate;
        #endregion

        #region Unity Callback Functions
        private void Awake()
        {
            // StateMachine init
            StateMachine = gameObject.AddComponent<PlayerStateMachine>();
            IdleState = new PlayerIdleState(this, StateMachine, data, "idle");
            MoveState = new PlayerMoveState(this, StateMachine, data, "move");
            JumpState = new PlayerJumpState(this, StateMachine, data, "aerial");
            DashState = new PlayerDashState(this, StateMachine, data, "dash");
            DamagedState = new PlayerDamagedState(this, StateMachine, data, "damaged");
            AerialState = new PlayerAerialState(this, StateMachine, data, "aerial");
            LandingState = new PlayerLandingState(this, StateMachine, data, "landing");
            WallSlideState = new PlayerWallSlideState(this, StateMachine, data, "wallSlide");
            WallJumpState = new PlayerWallJumpState(this, StateMachine, data, "aerial");
            WallLeapState = new PlayerWallLeapState(this, StateMachine, data, "aerial");

            AtkChargedState = new PlayerAtkChargedState(this, StateMachine, data, "automata");

            // Primary Attack
            PrimAtkIdle4State = new PlayerPrimAtkIdle4State(this, StateMachine, data, "primAtkIdle4Way");
            PrimAtkIdle8State = new PlayerAtkIdle8State(this, StateMachine, data, "primAtkIdle8Way");
            PrimAtkMoveState = new PlayerAtkMoveState(this, StateMachine, data, "primAtkMove");
            PrimAtkDashState = new PlayerAtkDashState(this, StateMachine, data, "primAtkDash");
            PrimAtkAerial4State = new PlayerAtkAerial4State(this, StateMachine, data, "primAtkAerial4Way");
            PrimAtkAerial8State = new PlayerAtkAerial8State(this, StateMachine, data, "primAtkAerial8Way");
            PrimAtkWallChargedState = new PlayerAtkWallChargedState(this, StateMachine, data, "primAtkWallCharged");
            PrimAtkWallState = new PlayerAtkWallState(this, StateMachine, data, "primAtkWall");

            // Secondary Attack
            SecAtkIdle4State = new PlayerSecAtkIdle4State(this, StateMachine, data, "secAtkIdle4Way");
            SecAtkIdle8State = new PlayerAtkIdle8State(this, StateMachine, data, "secAtkIdle8Way");
            SecAtkMoveState = new PlayerAtkMoveState(this, StateMachine, data, "secAtkMove");
            SecAtkDashState = new PlayerAtkDashState(this, StateMachine, data, "secAtkDash");
            SecAtkAerial4State = new PlayerAtkAerial4State(this, StateMachine, data, "secAtkAerial4Way");
            SecAtkAerial8State = new PlayerAtkAerial8State(this, StateMachine, data, "secAtkAerial8Way");
            SecAtkWallChargedState = new PlayerAtkWallChargedState(this, StateMachine, data, "secAtkWallCharged");
            SecAtkWallState = new PlayerAtkWallState(this, StateMachine, data, "secAtkWall");

            ChargedAttackDelegate += data.slot1.DoChargedAttack;
        }

        private void Start()
        {
            // Player's components init
            Anim = GetComponent<Animator>();
            InputHandler = GetComponent<PlayerInputHandler>();
            PlayerController = GetComponent<CharacterController2D>();
            PlayerController.OnTriggerEnterEvent += OnTriggerCollidingEvent;
            PlayerController.OnTriggerStayEvent += OnTriggerCollidingEvent;

            // Player's data init
            data.FacingDir = 1;
            gravity = -(2 * data.maxJumpHeight) / Mathf.Pow(data.timeToJumpApex, 2);
            data.maxJumpVelocity = Mathf.Abs(gravity) * data.timeToJumpApex;
            data.minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * data.minJumpHeight);

            // StateMachine init
            StateMachine.Initialize(IdleState);
            AttackDelegate += data.slot1.DoAttack;
        }

        private void Update()
        {
            HandleHitPoint();

            StateMachine.CurrState.LogicUpdate();

            GravityCheck();
        }

        private void FixedUpdate()
        {
            StateMachine.CurrState.PhysicsUpdate();
        }

        private void OnTriggerCollidingEvent(Collider2D collision)
        {
            ContactPoint2D[] contacts = new ContactPoint2D[2];

            //Debug.Log(collision);
            switch (collision.gameObject.tag)
            {
                case "Enemy":
                    collision.GetContacts(contacts);
                    Debug.Log(contacts[0].point);

                    StateMachine.ChangeState(DamagedState);
                    break;
            }
        }

        #endregion

        #region Raycast Physics Functions / Movement Functions
        public void MoveHorizontally(float targetX)
        {
            HandleMovement(targetX, PlayerController.velocity.y);
            Anim.SetFloat("xVelocity", Mathf.Abs(PlayerController.velocity.x));
        }
        public void MoveVertically(float targetY)
        {
            HandleMovement(PlayerController.velocity.x, targetY);
            Anim.SetFloat("yVelocity", PlayerController.velocity.y);
        }
        public void HandleMovement(float targetX, float targetY)
        {
            Vector3 targetMovement = new Vector3(targetX, targetY);
            targetMovement.y += gravity * Time.deltaTime;
            PlayerController.Move(targetMovement * Time.deltaTime);
        }

        void GravityCheck()
        {
            if (Grounded() || StateMachine.CurrState is PlayerAtkChargedState || StateMachine.CurrState is PlayerDamagedState)
            {
                PlayerController.velocity.y = 0f;
            }
        }
        #endregion

        #region Check Functions

        public bool Grounded() => PlayerController.IsGrounded;
        public bool WallTouched() => data.FacingDir == 1 ? PlayerController.collisionState.sideRight : PlayerController.collisionState.sideLeft;

        public void ShouldFlip(int xInput)
        {
            if (xInput != 0 && data.FacingDir != xInput)
                Flip();
        }
        #endregion

        #region Animation Event Functions
        void AnimTrigger() => StateMachine.CurrState.AnimTrigger();

        void AnimFinishTrigger() => StateMachine.CurrState.AnimFinishTrigger();

        void HandleAttack() => AttackDelegate?.Invoke(hitPoint);

        public void ResetAttackDelegate() => AttackDelegate = null;

        void HandleChargedAttack() => ChargedAttackDelegate?.Invoke(hitPoint);

        public void ResetChargedAttackDelegate() => ChargedAttackDelegate = null;
        #endregion

        #region Other Functions
        private void HandleHitPoint()
        {
            DirectionIndicator.rotation = Quaternion.Euler(0, 0, data.LookAngle - 45f);
        }

        public void DefaultHitPoint() => GetHitPoint(data.FacingDir == 1 ? 0 : 180);

        public void GroundedDownDiag() => GetHitPoint(data.FacingDir == 1 ? -45 : -135);

        public void GetHitPoint() => GetHitPoint(data.LookAngle);

        void GetHitPoint(float angle)
        {
            hitPoint.gameObject.transform.position = hitPoint.pos[angle] * (InputHandler.AtkToggleInput ? data.slot2.weaponOffset : data.slot1.weaponOffset) + transform.position;
            hitPoint.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }

        public void Flip()
        {
            data.FacingDir *= -1;
            transform.Rotate(0.0f, 180f, 0.0f);
            //transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        #endregion
    }
}

