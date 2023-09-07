using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeaLoux.Entity
{
    public class PlayerJumpState : PlayerBasicActionState
    {
        int amountLeft;

        public PlayerJumpState(Player player, PlayerStateMachine stateMachine, EntityData data, PlayerData playerData, string animBoolName) : base(player, stateMachine, data, playerData, animBoolName)
        {
            amountLeft = _data.amountOfJumps;
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

            _player.AerialState.Jumping();
            //_player.MoveVertically(_jumpInput ? _playerData.maxJumpVelocity : _playerData.minJumpVelocity);
            _player.MoveVertically(_data.maxJumpVelocity);
            _player.InputHandler.TickJumpInput();
            isActionDone = true;
        }

        public bool CanJump() => amountLeft > 0;
        public void ResetAmount() => amountLeft = _data.amountOfJumps;
        public void DecreaseAmount() => amountLeft--;
    }
}