using System.Collections;
using UnityEngine;

namespace DeaLoux.Entity
{
    public class PlayerWallJumpState : PlayerBasicActionState
    {
        float lastJumpTime;
        bool hyperJump;
        bool leap;

        public PlayerWallJumpState(Player player, PlayerStateMachine stateMachine, EntityData data, PlayerData playerData, string animBoolName) : base(player, stateMachine, data, playerData, animBoolName) { }

        public override void Enter()
        {
            base.Enter();

            _player.JumpState.ResetAmount();
            _player.JumpState.DecreaseAmount();
            _player.AerialState.Jumping();
            _player.InputHandler.TickJumpInput();
            lastJumpTime = Time.time;

            hyperJump = _player.InputHandler.DashInput;
            _data.dashJumpMultiplier = hyperJump ? 1.24f : 1f;
            if (hyperJump)
            {
                _player.AerialState.DashedLastFrame();
            }
            _player.Anim.SetBool("wallJumpHyper", hyperJump);

            if (leap)
            {
                _player.Flip();
            }
        }
        public override void Exit()
        {
            base.Exit();
            leap = false;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            Vector2 jumpSpeed = leap ? _data.wallLeap : _data.wallJump;

            if (hyperJump)
                _player.DashState.ShouldPlaceAfterImage();
        
            _player.HandleMovement(jumpSpeed.x * -_data.facingDir * _data.dashJumpMultiplier, jumpSpeed.y * _data.dashJumpMultiplier);

            /*if (Time.time >= _startTime + _playerData.wallJumpTime)
            {
                isActionDone = true;
            }*/

            if (_animFinished)
            {
                isActionDone = true;
            }
        }

        public void Leap() => leap = true;

        public bool CoolDownDone() => Time.time >= lastJumpTime + _data.wallJumpCDTime;
    }
}