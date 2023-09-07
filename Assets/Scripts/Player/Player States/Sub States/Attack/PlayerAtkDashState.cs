using System.Collections;
using UnityEngine;

namespace DeaLoux.Entity
{
    public class PlayerAtkDashState : PlayerDashState
    {
        public PlayerAtkDashState(Player player, PlayerStateMachine stateMachine, EntityData data, PlayerData playerData, string animBoolName) : base(player, stateMachine, data, playerData, animBoolName)
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
    }
}