using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeaLoux.Player
{
    public class PlayerAtkWallState : PlayerWallSlideState
    {
        public PlayerAtkWallState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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
                ChangeStateSH(_player.WallSlideState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}