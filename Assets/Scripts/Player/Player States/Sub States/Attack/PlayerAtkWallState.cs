using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeaLoux.Entity
{
    public class PlayerAtkWallState : PlayerWallSlideState
    {
        public PlayerAtkWallState(Player player, PlayerStateMachine stateMachine, EntityData data, PlayerData playerData, string animBoolName) : base(player, stateMachine, data, playerData, animBoolName)
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
    }
}