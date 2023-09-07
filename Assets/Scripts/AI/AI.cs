using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using DeaLoux.CoreSystems.Collision;
using DeaLoux.CoreSystems.Controller;

namespace DeaLoux.Entity
{
    public class AI : Entity
    {
        #region State Variables
        public AI_StateMachine StateMachine { get; private set; }
        public AI_IdleState IdleState { get; protected set; }
        public AI_AerialState AerialState { get; protected set; }
        public AI_MoveState MoveState { get; protected set; }
        public AI_HurtState HurtState { get; protected set; }

        protected Queue<AI_State> stateSequence;
        #endregion

        #region Components
        public BoxCollider2D Coll { get; private set; }
        public HurtBox HurtBox { get; private set; }

        #endregion

        #region Check Transforms
        [SerializeField]
        protected Transform playerPos;
        #endregion

        #region Other Variables
        [SerializeField]
        protected AI_Data baseData;
        #endregion

        #region Unity Callback Functions
        protected override void Awake()
        {
            base.Awake();

            StateMachine = gameObject.AddComponent<AI_StateMachine>();
            stateSequence = new Queue<AI_State>();
        }
        protected override void Start()
        {
            base.Start();
        }

        protected override void Update()
        {
            StateMachine.CurrState.LogicUpdate();
            base.Update();
        }
        #endregion

        #region Movement Functions
        public override void KnockBack(Vector2 target, float damage = 0)
        {
            base.KnockBack(target, damage);

            if (data.iframe <= 0)
            {
                TakeDamage(damage);
                data.knockbackDir = target * data.knockbackDistance;
                StateMachine.ChangeState(HurtState, true);
            }
        }

        protected override void GravityCheck()
        {
            if (Grounded())
            {
                Controller.velocity.y = 0f;
            }
        }

        #endregion

        #region Check Functions
        public bool LeftWallTouched() => Controller.collisionState.sideLeft;
        public bool RightWallTouched() => Controller.collisionState.sideRight;
        public override bool WallTouched() => RightWallTouched() || LeftWallTouched();
        #endregion

        #region Animation Event Functions
        // Handing attack
        public void PrimAttack() => data.slot1.DoAttack(hitPoint);
        public void ChargedPrimAttack() => data.slot1.DoHeavyAttack(hitPoint);
        public void SecAttack() => data.slot2.DoAttack(hitPoint);
        public void ChargedSecAttack() => data.slot2.DoHeavyAttack(hitPoint);

        // Handing hitpoint's position, rotation
        protected void GetHitPoint(float angle, float offset)
        {
            hitPoint.gameObject.transform.position = hitPoint.pos[angle] * offset + transform.position;
            hitPoint.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }

        #endregion

        #region Other Functions
        protected RelativePos Decide()
        {
            Vector2 dis = playerPos.position - transform.position;

            if (data.facingDir != (int)Mathf.Sign(dis.x))
                Flip();

            //Debug.Log(dis);

            RelativePos result;

            // The player is within melee range to the entity
            if (Mathf.Abs(dis.x) < 4f)
            {
                if (dis.y > 1)
                {
                    result = RelativePos.close_above;
                }
                else if (dis.y < -1)
                {
                    result = RelativePos.close_below;
                }
                else
                {
                    result = RelativePos.close_onLevel;
                }
            }
            // The player is far away from the entity
            else
            {
                if (dis.y > 1)
                {
                    result = RelativePos.far_above;
                }
                else if (dis.y < -1)
                {
                    result = RelativePos.far_below;
                }
                else
                {
                    result = RelativePos.far_onLevel;
                }
            }

            return result;
        }

        public bool EmptySequence() => stateSequence.Count == 0;

        public AI_State NextStateInSequence() => stateSequence.Dequeue();
        #endregion
    }

    public enum RelativePos
    {
        close_onLevel,
        close_above,
        close_below,
        far_onLevel,
        far_above,
        far_below
    }
}