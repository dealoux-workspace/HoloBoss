using DeaLoux.Equipment;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

namespace DeaLoux.Entity
{
    public class PlayerInputHandler : MonoBehaviour
    {
        public Vector2 RawMovementInput { get; private set; }
        public int NormInputX { get; private set; }
        public int NormInputY { get; private set; }
        public bool JumpInput { get; private set; }
        public bool DashInput { get; private set; }
        public bool PrimAtkInput { get; private set; }
        public bool SecAtkInput { get; private set; }
        public bool PrimAtkCharged { get; private set; }
        public bool SecAtkCharged { get; private set; }
        public bool MenuInput { get; private set; }
        public bool CycleInput { get; private set; }

        [SerializeField]
        EntityData data;
        [SerializeField]
        PlayerData playerData;
        [SerializeField]
        GameObject primChargingFX;
        [SerializeField]
        GameObject secChargingFX;
        Animator anim;
        Player player;

        float jumpInputStartTime;

        float primAtkInputStartTime;
        float primChargingStartTime;
        bool holdingPrimAtkInput;

        float secAtkInputStartTime;
        float secChargingStartTime;
        bool holdingSecAtkInput;

        private void Start()
        {
            anim = GetComponent<Animator>();
            player = GetComponent<Player>();
        }

        private void Update()
        {
            JumpInputHoldTime();
            PrimAtkInputHoldTime();
            SecAtkInputHoldTime();
            //SecAtkInputHoldTime();
            LookDirCheck();
        }

        public void OnMoveInput(InputAction.CallbackContext context)
        {
            RawMovementInput = context.ReadValue<Vector2>();
            SetLookDir(Vector2Int.RoundToInt(RawMovementInput.normalized));

            NormInputX = Mathf.RoundToInt(RawMovementInput.x);
            NormInputY = Mathf.RoundToInt(RawMovementInput.y);
            //Debug.Log(RawMovementInput);
        }

        public void OnJumpInput(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                JumpInput = true;
                jumpInputStartTime = Time.time;
            }
            else if (context.canceled)
            {
                JumpInput = false;
            }
        }

        public void OnDashInput(InputAction.CallbackContext context)
        {
            DashInput = !context.canceled;
        }
        public void OnPrimAtkInput(InputAction.CallbackContext context)
        {
            //Debug.Log(context);

            if (context.started)
            {
                SwitchSlot(data.slot1);
                primAtkInputStartTime = Time.time;
                PrimAtkInput = true;
            }

            else if (context.performed)
                primChargingStartTime = Time.time;

            else if (context.canceled)
            {
                if (holdingPrimAtkInput && Time.time > primChargingStartTime + playerData.minChargeTime)
                {
                    PrimAtkCharged = true;
                }

                PrimAtkInput = false;
            }

            holdingPrimAtkInput = context.performed;
            primChargingFX.SetActive(holdingPrimAtkInput);
        }

        public void OnSecAtkInput(InputAction.CallbackContext context)
        {
            //Debug.Log(context);

            if (context.started)
            {
                SwitchSlot(data.slot2);
                secAtkInputStartTime = Time.time;
                SecAtkInput = true;
            }

            else if (context.performed)
                secChargingStartTime = Time.time;

            else if (context.canceled)
            {
                if (holdingSecAtkInput && Time.time > secChargingStartTime + playerData.minChargeTime)
                {
                    SecAtkCharged = true;
                }

                SecAtkInput = false;
            }

            holdingSecAtkInput = context.performed;
            secChargingFX.SetActive(holdingSecAtkInput);
        }

        public void OnMenuInput(InputAction.CallbackContext context)
        {
            MenuInput = context.performed;
        }

        public void OnCycleInput(InputAction.CallbackContext context)
        {
            CycleInput = context.performed;
        }

        public void TickJumpInput() => JumpInput = false;
        public void TickDashInput() => DashInput = false;
        public void TickMenuInput() => MenuInput = false;
        public void TickCycleInput() => CycleInput = false;

        public void TickPrimAtkInput() => PrimAtkInput = false;
        public void TickSecAtkInput() => SecAtkInput = false;
        public void TickPrimAtkCharged() => PrimAtkCharged = false;
        public void TickSecAtkCharged() => SecAtkCharged = false;

        void SwitchSlot(EquipmentBase slot)
        {
            player.ResetAttackDelegates();
            player.AttackDelegate += slot.DoAttack;
            player.HeavyAttackDelegate += slot.DoHeavyAttack;
        }

        void JumpInputHoldTime()
        {
            if (Time.time >= jumpInputStartTime + playerData.inputHoldTime)
            {
                TickJumpInput();
            }
        }

        void PrimAtkInputHoldTime()
        {
            if (Time.time >= primAtkInputStartTime + playerData.inputHoldTime)
            {
                TickPrimAtkInput();
            }
        }

        void SecAtkInputHoldTime()
        {
            if (Time.time >= secAtkInputStartTime + playerData.inputHoldTime)
            {
                TickSecAtkInput();
            }
        }

        void SetLookDir(Vector2 v)
        {
            data.LookDir = v;
            data.LookAngle = Vector2.SignedAngle(Vector2.right, data.LookDir);

            int lookAngle;

            if (data.LookAngle > 90)
                lookAngle = (int)(180 - data.LookAngle);
            else if (data.LookAngle < -90)
                lookAngle = (int)(-180 - data.LookAngle);
            else
                lookAngle = (int)data.LookAngle;

            anim.SetInteger("lookAngle", lookAngle);
        }

        void LookDirCheck()
        {
            if (RawMovementInput == Vector2.zero)
                SetLookDir(new Vector2(data.facingDir, 0));
        }

        #region DEPRECIATED
        public bool AtkToggleInput { get; private set; }
        /*public void OnAtkToggleInput(InputAction.CallbackContext context)
        {
            AtkToggleInput = context.performed;
            if (context.performed)
            {
                SwitchSlot(data.slot2);
                AtkToggleInput = true;
            }
            else
            {
                SwitchSlot(data.slot1);
                AtkToggleInput = false;
            }
            anim.SetBool("toggleAtk", AtkToggleInput);
        }*/
        #endregion
    }
}