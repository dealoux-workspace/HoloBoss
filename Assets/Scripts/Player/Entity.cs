using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace DeaLoux.Entity
{
    using CoreSystems.Collision;
    using DeaLoux.CoreSystems.Controller;

    public class Entity : MonoBehaviour
    {
        #region Components
        public Animator Anim { get; private set; }
        public SpriteRenderer Sprite { get; private set; }
        #endregion

        #region Raycast Physics
        protected float gravity;
        public virtual CharacterController2D Controller { get; private set; }
        #endregion

        #region Other Variables
        public EntityData data;
        public HitBox hitPoint;
        public virtual event Action<HitBox> AttackDelegate;
        public virtual event Action<HitBox> HeavyAttackDelegate;
        #endregion

        #region Unity Callback Functions
        protected virtual void Awake()
        {
            // Data init
            data.facingDir = 1;
            data.hp = 10;
            data.iframe = 0f;
            gravity = -(2 * data.maxJumpHeight) / Mathf.Pow(data.timeToJumpApex, 2);
            data.maxJumpVelocity = Mathf.Abs(gravity) * data.timeToJumpApex;
            data.minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * data.minJumpHeight);
        }

        protected virtual void Start()
        {
            // Entity's components init
            Anim = GetComponent<Animator>();
            Sprite = GetComponent<SpriteRenderer>();
            Controller = GetComponent<CharacterController2D>();
            Controller.OnTriggerEnterEvent += OnTriggerCollidingEvent;
            //Controller.OnTriggerStayEvent += OnTriggerCollidingEvent;

            AttackDelegate += data.slot1.DoAttack;
            HeavyAttackDelegate += data.slot1.DoHeavyAttack;
        }

        protected virtual void Update()
        {
            GravityCheck();
            HandleIframe();
        }

        protected virtual IEnumerator Coroutine(float sec = 0) { yield return new WaitForSeconds(sec); }
        public virtual void Wait(float sec) => StartCoroutine(Coroutine(sec));

        protected virtual void OnTriggerCollidingEvent(Collider2D collider)
        {
        }

        protected virtual void OnCollisionEnter2D(Collision2D collision)
        {
        }
        #endregion

        #region Raycast Physics Functions / Movement Functions
        public virtual void MoveHorizontally(float targetX)
        {
            HandleMovement(targetX, Controller.velocity.y);
            Anim.SetFloat("xVelocity", Mathf.Abs(Controller.velocity.x));
        }
        public virtual void MoveVertically(float targetY)
        {
            HandleMovement(Controller.velocity.x, targetY);
            Anim.SetFloat("yVelocity", Controller.velocity.y);
        }
        public virtual void HandleMovement(float targetX, float targetY)
        {
            Vector3 targetMovement = new Vector3(targetX, targetY) ;
            targetMovement += .5f * targetMovement * Time.deltaTime;
            targetMovement.y += gravity * Time.deltaTime;
            Controller.Move(targetMovement * Time.deltaTime);
        }
        public virtual void HandleMovement(Vector2 target) => HandleMovement(target.x, target.y);

        public virtual void TakeDamage(float damage = 0)
        {
            if (data.iframe <= 0)
            {
                data.hp -= damage;
                data.iframe = data.IframeDuration;
            }
        }
        public virtual void KnockBack(float damage = 0)
        {
            if (damage > 0)
                TakeDamage(damage);
        }

        public virtual void KnockBack(Vector2 target, float damage = 0)
        {
        }

        protected virtual IEnumerator Flash()
        {
            while(data.iframe > 0)
            {
                Sprite.color = data.flashColour;
                yield return new WaitForSeconds(data.flashDuration);
                Sprite.color = data.transparent;
                yield return new WaitForSeconds(data.flashDuration);
                data.iframe -= Time.deltaTime;
            }
        }

        protected virtual void HandleIframe()
        {
            if (data.iframe > 0)
            {
                StartCoroutine(Flash());
            }
        }

        protected virtual void GravityCheck()
        {
        }
        #endregion

        #region Check Functions

        public virtual bool Grounded() => Controller.IsGrounded;
        public virtual bool WallTouched() => data.facingDir == 1 ? Controller.collisionState.sideRight : Controller.collisionState.sideLeft;

        public virtual void ShouldFlip(int xInput)
        {
            if (xInput != 0 && data.facingDir != xInput)
                Flip();
        }
        #endregion

        #region Animation Event Functions
        // Animation trigger
        protected virtual void AnimTrigger() { }
        protected virtual void AnimFinishTrigger() { }

        // Handing attack
        protected virtual void HandleAttack() => AttackDelegate?.Invoke(hitPoint);
        protected virtual void HandleChargedAttack() => HeavyAttackDelegate?.Invoke(hitPoint);

        public virtual void ResetAttackDelegates() => AttackDelegate = HeavyAttackDelegate = null;

        // Handing hitpoint's position, rotation
        public virtual void DefaultHitPoint() => GetHitPoint(data.facingDir == 1 ? 0 : 180);
        protected virtual void DefaultHitPointWall() => GetHitPoint(data.facingDir == 1 ? 180 : 0);
        protected virtual void UpHitPoint() => GetHitPoint(90);
        protected virtual void DownHitPoint() => GetHitPoint(-90);
        protected virtual void DownDiagHitPoint() => GetHitPoint(data.facingDir == 1 ? -45 : -135);

        protected virtual void GetHitPoint() => GetHitPoint(data.LookAngle);

        protected virtual void GetHitPoint(float angle)
        {
        }
        #endregion

        #region Other Functions
        public virtual void Flip()
        {
            data.facingDir *= -1;
            transform.Rotate(0.0f, 180f, 0.0f);
            //transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        #endregion
    }
}

