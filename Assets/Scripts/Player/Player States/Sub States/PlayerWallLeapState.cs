using Data;
using UnityEngine;

namespace DeaLoux.Player
{
    public class PlayerWallLeapState : PlayerWallActionState
    {
        public PlayerWallLeapState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) { }

        public override void Enter()
        {
            base.Enter();
            //Debug.Log("Leap");
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            _player.ShouldFlip(_xInput);
            _player.HandleMovement(_playerData.wallLeap.x * _playerData.FacingDir, _playerData.wallLeap.y);
        }
    }
}