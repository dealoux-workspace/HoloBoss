using System.Collections;
using UnityEngine;

namespace DeaLoux.Entity
{
    public class PlayerAtkWallChargedState : PlayerAutomataState
    {
        public PlayerAtkWallChargedState(Player player, PlayerStateMachine stateMachine, EntityData data, PlayerData playerData, string animBoolName) : base(player, stateMachine, data, playerData, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (_animFinished)
            {
                if (_primAtkCharged)
                    _player.InputHandler.TickPrimAtkCharged();
                if (_secAtkCharged)
                    _player.InputHandler.TickSecAtkCharged();
                _stateMachine.ChangeToPreviousState();
            }
        }
    }
}