using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeaLoux.Player
{
    public class PlayerJumpState : PlayerBasicActionState
    {
        private int amountLeft;

        public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
        {
            amountLeft = _playerData.amountOfJumps;
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            _player.InputHandler.TickJumpInput();
            _player.AerialState.SetJumpingStatus();
            _player.MoveVertically(_player.InputHandler.JumpInputTap ? _playerData.minJumpVelocity : _playerData.maxJumpVelocity);

            isActionDone = true;
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public bool CanJump() => amountLeft > 0;
        public void ResetAmount() => amountLeft = _playerData.amountOfJumps;
        public void DecreaseAmount() => amountLeft--;
    }
}