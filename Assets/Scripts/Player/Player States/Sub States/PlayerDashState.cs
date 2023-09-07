using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeaLoux.Entity
{
    public class PlayerDashState : PlayerBasicActionState
    {
        public bool Dashable { get; private set; }
        Vector2 lastAIPos;

        public PlayerDashState(Player player, PlayerStateMachine stateMachine, EntityData data, PlayerData playerData, string animBoolName) : base(player, stateMachine, data, playerData, animBoolName) { }

        public override void Enter()
        {
            base.Enter();
            Dashable = false;
            _data.dashJumpMultiplier = 1.24f;
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
                _player.MoveHorizontally(_data.dashVelocity * _data.facingDir);
                ShouldPlaceAfterImage();

                if (_jumpInput && _player.JumpState.CanJump())
                {
                    _player.AerialState.DashedLastFrame();
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

                if (Time.time >= _startTime + _data.dashTime || !_grounded)
                {
                    isActionDone = true;
                    _player.InputHandler.TickDashInput();
                }
            }
        }

        public bool ResetDash() => Dashable = true;
        public void ShouldPlaceAfterImage()
        {
            if (Vector2.Distance(_player.transform.position, lastAIPos) >= _data.afterImageDistance)
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