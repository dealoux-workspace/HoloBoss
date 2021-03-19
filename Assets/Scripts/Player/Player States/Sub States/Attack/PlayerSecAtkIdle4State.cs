using Data;
using UnityEngine;

namespace DeaLoux.Player
{
    public class PlayerSecAtkIdle4State : PlayerState
    {
        private int index;
        public PlayerSecAtkIdle4State(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            index = 1;
            _player.Anim.SetInteger("atkIdleIndex", index);
            _player.InputHandler.TickSecAtkInput();
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
            if (_secAtkInput && index < 3)
            {
                _player.InputHandler.TickPrimAtkInput();
                index++;
            }
            else if (_primAtkInput)
            {
                switch (_playerData.slot2.type)
                {
                    case EquipmentType.FOUR_WAY:
                        ChangeStateSH(_player.PrimAtkIdle4State);
                        break;

                    case EquipmentType.EIGHT_WAY:
                        ChangeStateSH(_player.PrimAtkIdle8State);
                        break;
                }
            }
            else if (_primAtkCharged)
            {
                ChangeStateSH(_player.AtkChargedState);
            }

            if (_animFinished)
            {
                _stateMachine.ChangeToPreviousState();
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}