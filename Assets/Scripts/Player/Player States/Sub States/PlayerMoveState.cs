using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeaLoux.Player
{
    public class PlayerMoveState : PlayerGroundedState
    {
        public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) { }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            _player.MoveHorizontally(_playerData.movementSpeed * _playerData.FacingDir);

            _player.ShouldFlip(_xInput);

            if (_xInput == 0)
            {
                ChangeStateSH(_player.IdleState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}