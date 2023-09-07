using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeaLoux.Entity
{
    public class PlayerMoveState : PlayerGroundedState
    {
        public PlayerMoveState(Player player, PlayerStateMachine stateMachine, EntityData data, PlayerData playerData, string animBoolName) : base(player, stateMachine, data, playerData, animBoolName) { }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            _player.MoveHorizontally(_data.movementSpeed * _data.facingDir);

            _player.ShouldFlip(_xInput);

            if (_xInput == 0)
            {
                ChangeStateSH(_player.IdleState);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}