using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

namespace DeaLoux.Player
{
    public class PlayerInputHandler : MonoBehaviour
    {
        public Vector2 RawMovementInput { get; private set; }
        public int NormInputX { get; private set; }
        public int NormInputY { get; private set; }
        public bool JumpInput { get; private set; }
        public bool JumpInputTap { get; private set; }
        public bool DashInput { get; private set; }
        public bool PrimAtkInput { get; private set; }
        public bool AtkToggleInput { get; private set; }
        public bool AtkCharged { get; private set; }
        public bool MenuInput { get; private set; }
        public bool CycleInput { get; private set; }

        [SerializeField]
        Data.PlayerData data;
        [SerializeField]
        GameObject chargingFX;
        Animator anim;
        Player player;

        private bool holdingPrimAtkInput;
        private float jumpInputStartTime;
        private float dashInputStartTime;
        private float primAtkInputStartTime;
        private float chargingStartTime;
        private float chargedAtkStartTime;

        private void Start()
        {
            anim = GetComponent<Animator>();
            player = GetComponent<Player>();
        }

        private void Update()
        {
            JumpInputHoldTime();
            DashInputHoldTime();
            PrimAtkInputHoldTime();
            ChargedAtkReleaseWindow();
            LookDirCheck();
        }

        public void OnMoveInput(InputAction.CallbackContext context)
        {
            RawMovementInput = context.ReadValue<Vector2>();
            SetLookDir(Vector2Int.RoundToInt(RawMovementInput.normalized));

            NormInputX = Mathf.Abs(RawMovementInput.x) > .5f ? (int)(RawMovementInput * Vector2.right).normalized.x : 0;
            NormInputY = Mathf.Abs(RawMovementInput.y) > .5f ? (int)(RawMovementInput * Vector2.up).normalized.y : 0;
            //Debug.Log(RawMovementInput);
        }

        public void OnJumpInput(InputAction.CallbackContext context)
        {
            jumpInputStartTime = Time.time;

            if (context.interaction is TapInteraction)
            {
                if (context.started)
                {
                    JumpInput = true;
                    JumpInputTap = true;
                }

                if (context.canceled)
                {
                    JumpInputTap = false;
                }
            }
            else
            {
                JumpInput = context.performed;
            }

            //Debug.Log(context.interaction);
        }

        public void OnDashInput(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                dashInputStartTime = Time.time;
                DashInput = true;
            }
            else if (context.canceled)
                DashInput = false;

        }
        public void OnPrimAtkInput(InputAction.CallbackContext context)
        {
            //Debug.Log(context);

            if (context.started)
            {
                PrimAtkInput = true;
            }

            else if (context.performed)
                chargingStartTime = Time.time;

            else if (context.canceled)
            {
                if (holdingPrimAtkInput && Time.time > chargingStartTime + data.minChargeTime)
                {
                    chargedAtkStartTime = Time.time;
                    AtkCharged = true;
                }

                PrimAtkInput = false;
            }


            holdingPrimAtkInput = context.performed;
            chargingFX.SetActive(holdingPrimAtkInput);
        }


        public void OnAtkToggleInput(InputAction.CallbackContext context)
        {
            //AtkToggleInput = context.performed;
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
        public void TickPrimAtkInput() => PrimAtkInput = false;
        public void TickPrimAtkCharged() => AtkCharged = false;
        public void TickMenuInput() => MenuInput = false;
        public void TickCycleInput() => CycleInput = false;

        void SwitchSlot(Data.EquipmentBase slot)
        {
            player.ResetAttackDelegate();
            player.AttackDelegate += slot.DoAttack;
            player.ResetChargedAttackDelegate();
            player.ChargedAttackDelegate += slot.DoChargedAttack;
        }

        private void JumpInputHoldTime()
        {
            if (Time.time >= jumpInputStartTime + data.inputHoldTime)
            {
                JumpInput = false;
            }
        }

        private void DashInputHoldTime()
        {
            if (Time.time >= dashInputStartTime + data.inputHoldTime)
            {
                DashInput = false;
            }
        }

        private void PrimAtkInputHoldTime()
        {
            if (Time.time >= primAtkInputStartTime + data.inputHoldTime)
            {
                PrimAtkInput = false;
            }
        }

        private void ChargedAtkReleaseWindow()
        {
            if (Time.time >= chargedAtkStartTime + data.inputHoldTime)
            {
                AtkCharged = false;
            }
        }

        private void SetLookDir(Vector2 v)
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

        private void LookDirCheck()
        {
            if (RawMovementInput == Vector2.zero)
                SetLookDir(new Vector2(data.FacingDir, 0));
        }
    }
}