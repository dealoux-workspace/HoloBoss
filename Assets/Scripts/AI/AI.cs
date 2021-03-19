using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeaLoux.AI
{
    public class AI : MonoBehaviour
    {
        #region State Variables
        public AI_StateMachine StateMachine { get; private set; }

        #endregion

        #region Components
        public Rigidbody2D Rb { get; private set; }
        public Animator Anim { get; private set; }
        public GameObject AliveGO { get; private set; }
        public BoxCollider2D Coll { get; private set; }
        #endregion

        #region Check Transforms
        [SerializeField]
        private Transform groundCheck;
        [SerializeField]
        private Transform wallCheck;
        [SerializeField]
        private Transform ledgeCheck;
        [SerializeField]
        private Transform playerCheck;
        #endregion

        #region Other Variables
        [SerializeField]
        protected Data.AI_Data aIData;
        public int FacingDir { get; private set; }
        public Vector2 CurrVelocity { get; private set; }
        protected bool _stunned;
        protected bool _dead;
        private float currHealth;
        private float currStunResistance;
        private float lastDamageTime;
        private Vector2 workspace;
        #endregion

        #region Unity Callback Functions
        public virtual void Awake()
        {
            StateMachine = gameObject.AddComponent<AI_StateMachine>();
            FacingDir = 1;
            currHealth = aIData.maxHealth;
            currStunResistance = aIData.stunResistance;

            AliveGO = transform.Find("Alive").gameObject;
            Rb = AliveGO.GetComponent<Rigidbody2D>();
            Anim = AliveGO.GetComponent<Animator>();
            //atsm = AliveGO.GetComponent<AnimationToStatemachine>();
        }

        public virtual void Start() { }

        public virtual void Update()
        {
            StateMachine.CurrState.LogicUpdate();
        }

        public virtual void FixedUpdate()
        {
            CurrVelocity = Rb.velocity;
            StateMachine.CurrState.PhysicsUpdate();
        }
        #endregion

        #region Movement Functions
        // horizontal velocity
        public void SetVelocityX(float velocity)
        {
            workspace.Set(velocity * FacingDir, CurrVelocity.y);
            Rb.velocity = workspace;
            CurrVelocity = workspace;
        }

        // vertical velocity
        public void SetVelocityY(float velocity)
        {
            workspace.Set(CurrVelocity.x, velocity);
            Rb.velocity = workspace;
            CurrVelocity = workspace;
        }

        // angled velocity
        public void SetVelocity(float velocity, Vector2 angle, int direction)
        {
            angle.Normalize();

            workspace.Set(velocity * direction * angle.x, angle.y * velocity);
            Rb.velocity = workspace;
            CurrVelocity = workspace;
        }

        // dash velocity
        public void SetVelocityD(float velocity, Vector2 direction)
        {
            workspace = velocity * direction;
            Rb.velocity = workspace;
            CurrVelocity = workspace;
        }
        #endregion

        #region Check Functions
        public bool Grounded() => Physics2D.OverlapCircle(groundCheck.position, aIData.groundCheckRadius, aIData.whatIsGround);
        public bool LedgeReached() => Physics2D.Raycast(ledgeCheck.position, Vector2.down, aIData.ledgeCheckDistance, aIData.whatIsGround);
        public bool WallReached() => Physics2D.Raycast(wallCheck.position, AliveGO.transform.right, aIData.wallCheckDistance, aIData.whatIsGround);
        #endregion

        #region Other Functions
        public void Flip()
        {
            FacingDir *= -1;
            AliveGO.transform.Rotate(0.0f, 180.0f, 0.0f);
        }

        public virtual void OnDrawGizmos()
        {
            Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * FacingDir * aIData.wallCheckDistance));
            Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * aIData.ledgeCheckDistance));
        }
        #endregion
    }
}