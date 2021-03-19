using Data;
using UnityEngine;

namespace DeaLoux.Player
{
    public class PlayerAtkIdle8State : PlayerState
    {
        public PlayerAtkIdle8State(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _player.InputHandler.TickPrimAtkInput();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (_animFinished)
            {
                _stateMachine.ChangeToPreviousState(_player.IdleState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}