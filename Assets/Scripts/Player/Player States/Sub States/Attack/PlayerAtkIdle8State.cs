using UnityEngine;

namespace DeaLoux.Entity
{
    public class PlayerAtkIdle8State : PlayerState
    {
        public PlayerAtkIdle8State(Player player, PlayerStateMachine stateMachine, EntityData data, PlayerData playerData, string animBoolName) : base(player, stateMachine, data, playerData, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            if (_primAtkInput)
                _player.InputHandler.TickPrimAtkInput();
            if (_secAtkInput)
                _player.InputHandler.TickSecAtkInput();
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
    }
}