using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeaLoux.Player
{
    public class PlayerDashState : PlayerBasicActionState
    {
        public bool Dashable { get; private set; }

        public PlayerDashState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) { }
        private Vector2 lastAIPos;

        public override void Enter()
        {
            base.Enter();
            Dashable = false;
            _playerData.dashJumpMultiplier = 1.24f;
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
                _player.ShouldFlip(_xInput);
                PlaceAfterImage();
                _player.MoveHorizontally(_playerData.dashVelocity * _playerData.FacingDir);
                ShouldPlaceAfterImage();

                if (_jumpInput && _player.JumpState.CanJump())
                {
                    ChangeStateSH(_player.JumpState);
                }
                else if (_primAtkInput && !(_stateMachine.CurrState is PlayerAtkDashState))
                {
                    ChangeStateSH(_player.PrimAtkDashState);
                }
                else if (_secAtkInput && !(_stateMachine.CurrState is PlayerAtkDashState))
                {
                    ChangeStateSH(_player.SecAtkDashState);
                }

                if (Time.time >= _startTime + _playerData.dashTime || !_grounded)
                {
                    isActionDone = true;
                    _player.InputHandler.TickDashInput();
                }
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public bool ResetDash() => Dashable = true;
        public void ShouldPlaceAfterImage()
        {
            if (Vector2.Distance(_player.transform.position, lastAIPos) >= _playerData.afterImageDistance)
            {
                PlaceAfterImage();
            }
        }

        private void PlaceAfterImage()
        {
            PlayerAfterImagePool.Instance.Get();
            lastAIPos = _player.transform.position;
        }
    }
}