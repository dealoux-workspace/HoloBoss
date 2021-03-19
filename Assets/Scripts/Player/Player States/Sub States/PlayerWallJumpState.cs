using Data;
using UnityEngine;

namespace DeaLoux.Player
{
    public class PlayerWallJumpState : PlayerWallActionState
    {
        public PlayerWallJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) { }

        public override void Enter()
        {
            base.Enter();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            _xInput = _player.InputHandler.NormInputX;
            _player.HandleMovement(_playerData.wallJumpClimb.x * -_playerData.FacingDir, _playerData.wallJumpClimb.y);
        }
    }
}