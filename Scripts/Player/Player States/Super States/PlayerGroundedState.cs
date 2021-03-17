using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeaLoux.Player
{
    public abstract class PlayerGroundedState : PlayerState
    {
        public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();

            _player.JumpState.ResetAmount();
            _player.DashState.ResetDash();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (!_exitingState)
            {
                _grounded = _player.Grounded();
                _xInput = _player.InputHandler.NormInputX;
                _jumpInput = _player.InputHandler.JumpInput;
                _dashInput = _player.InputHandler.DashInput;

                if (_jumpInput && _player.JumpState.CanJump())
                {
                    ChangeStateSH(_player.JumpState);
                }
                else if (_dashInput && _player.DashState.Dashable)
                {
                    ChangeStateSH(_player.DashState);
                }
                else if (!_grounded)
                {
                    ChangeStateSH(_player.AerialState);
                }
                else if (_primAtkInput)
                {
                    switch (_atkToggled ? _playerData.slot2.type : _playerData.slot1.type)
                    {
                        case EquipmentType.FOUR_WAY:
                            if (_stateMachine.CurrState is PlayerMoveState)
                                ChangeStateSH(_player.AtkMoveState);
                            else
                            {
                                _player.AtkIdle4State.SetToggleStatus(_atkToggled);
                                ChangeStateSH(_player.AtkIdle4State, true);
                            }

                            break;

                        case EquipmentType.EIGHT_WAY:
                            ChangeStateSH(_player.AtkIdle8State, true);
                            break;
                    }
                }
                else if (_player.InputHandler.AtkCharged)
                {
                    switch (_atkToggled ? _playerData.slot2.Ctype : _playerData.slot1.Ctype)
                    {
                        case EquipmentType.FOUR_WAY:
                            _player.AtkChargedState.SetAnim("atkIdle4Charged");
                            break;

                        case EquipmentType.EIGHT_WAY:
                            _player.AtkChargedState.SetAnim("atkIdle8Charged");
                            break;
                    }
                    ChangeStateSH(_player.AtkChargedState, true);
                }
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}