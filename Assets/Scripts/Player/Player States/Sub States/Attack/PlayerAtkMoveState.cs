using Data;
using UnityEngine;

namespace DeaLoux.Player
{
    public class PlayerAtkMoveState : PlayerMoveState
    {
        public PlayerAtkMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (_animFinished)
            {
                ChangeStateSH(_player.MoveState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}