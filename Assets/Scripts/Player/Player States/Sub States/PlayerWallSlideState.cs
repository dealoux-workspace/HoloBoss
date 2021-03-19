using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeaLoux.Player
{
    public class PlayerWallSlideState : PlayerState
    {
        public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
        {
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (!_exitingState)
            {
                _grounded = _player.Grounded();
                _wallTouched = _player.WallTouched();
                _primAtkInput = _player.InputHandler.PrimAtkInput;
                _xInput = _player.InputHandler.NormInputX;
                _jumpInput = _player.InputHandler.JumpInput;

                _player.MoveVertically(-_playerData.wallSlideVelocity);

                if (_grounded)
                {
                    ChangeStateSH(_player.IdleState);
                }
                else if (_primAtkInput)
                {
                    ChangeStateSH(_player.AtkWallState);

                }
                else if (_jumpInput && _xInput == _playerData.FacingDir && _player.WallJumpState.CoolDownDone())
                {
                    ChangeStateSH(_player.WallJumpState);
                }
                else if (_jumpInput && _xInput != _playerData.FacingDir && _player.WallLeapState.CoolDownDone())
                {
                    ChangeStateSH(_player.WallLeapState);
                }
                else if (!_wallTouched || _xInput != _playerData.FacingDir)
                {
                    _player.Flip();
                    _player.AerialState.StartWallLeapCoyoteTime();
                    ChangeStateSH(_player.AerialState);
                }
                else if (_player.InputHandler.AtkCharged)
                {
                    _player.Flip();
                    ChangeStateSH(_player.AtkWallChargedState, true);
                }
            }
        }
    }
}