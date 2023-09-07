using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeaLoux.Entity
{
    public class PlayerWallSlideState : PlayerState
    {
        public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, EntityData data, PlayerData playerData, string animBoolName) : base(player, stateMachine, data, playerData, animBoolName)
        {
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (!_exitingState)
            {
                _player.MoveVertically(-_data.wallSlideVelocity);

                if (_grounded)
                {
                    ChangeStateSH(_player.IdleState);
                }
                else if (!_wallTouched || _xInput != _data.facingDir)
                {
                    _player.Flip();
                    _player.AerialState.StartWallLeapCoyoteTime();
                    ChangeStateSH(_player.AerialState);
                }
                else if (_jumpInput && _player.WallJumpState.CoolDownDone())
                {
                    ChangeStateSH(_player.WallJumpState);
                }
                else if (_primAtkInput)
                {
                    ChangeStateSH(_player.PrimAtkWallState);
                }
                else if (_secAtkInput)
                {
                    ChangeStateSH(_player.SecAtkWallState);
                }
                else if (_primAtkCharged)
                {
                    _player.Flip();
                    ChangeStateSH(_player.PrimAtkWallChargedState, true);
                }
                else if (_secAtkCharged)
                {
                    _player.Flip();
                    ChangeStateSH(_player.SecAtkWallChargedState, true);
                }
            }
        }

        /*public override void Exit()
        {
            base.Exit();
            _player.Flip();
        }*/
    }
}