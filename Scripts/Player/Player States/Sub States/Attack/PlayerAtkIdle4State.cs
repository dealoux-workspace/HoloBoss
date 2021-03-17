using Data;
using UnityEngine;

namespace DeaLoux.Player
{
    public class PlayerAtkIdle4State : PlayerState
    {
        private int index;
        private bool atkToggledLastFrame;
        public PlayerAtkIdle4State(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            index = 1;
            _player.Anim.SetInteger("atkIdleIndex", index);
            _player.InputHandler.TickPrimAtkInput();
        }

        public override void Exit()
        {
            base.Exit();
            _player.Anim.SetInteger("atkIdleIndex", 0);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            _player.Anim.SetInteger("atkIdleIndex", index);
            if (_player.InputHandler.PrimAtkInput && index < 3)
            {
                _player.InputHandler.TickPrimAtkInput();

                if (atkToggledLastFrame)
                {
                    if (_atkToggled)
                        index++;

                    else if (_playerData.slot1.type == EquipmentType.EIGHT_WAY)
                        ChangeStateSH(_player.AtkIdle8State);
                }
                else
                {
                    if (!_atkToggled)
                        index++;

                    else if (_playerData.slot2.type == EquipmentType.EIGHT_WAY)
                        ChangeStateSH(_player.AtkIdle8State);
                }
            }

            if (_animFinished)
            {
                _stateMachine.ChangeToPreviousState();
            }
        }

        public void SetToggleStatus(bool value) => atkToggledLastFrame = value;

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}