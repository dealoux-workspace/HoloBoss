using Data;
using UnityEngine;

namespace DeaLoux.Player
{
    public class PlayerAtkWallChargedState : PlayerAutomataState
    {
        public PlayerAtkWallChargedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _player.InputHandler.TickPrimAtkCharged();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (_animFinished)
            {
                _stateMachine.ChangeToPreviousState();
            }
        }
    }
}