using Data;
using UnityEngine;

namespace DeaLoux.Player
{
    public abstract class PlayerWallActionState : PlayerBasicActionState
    {
        protected float lastJumpTime;

        public PlayerWallActionState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) { }

        public override void Enter()
        {
            base.Enter();
            _player.JumpState.ResetAmount();
            _player.JumpState.DecreaseAmount();
            _player.AerialState.SetJumpingStatus();
            _player.InputHandler.TickJumpInput();
            lastJumpTime = Time.time;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (Time.time >= _startTime + _playerData.wallJumpTime)
            {
                isActionDone = true;
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public bool CoolDownDone() => Time.time >= lastJumpTime + _playerData.wallJumpCDTime;
    }
}