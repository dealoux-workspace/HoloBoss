using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeaLoux.Entity
{
    public class PlayerLandingState : PlayerGroundedState
    {
        public PlayerLandingState(Player player, PlayerStateMachine stateMachine, EntityData data, PlayerData playerData, string animBoolName) : base(player, stateMachine, data, playerData, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (_xInput != 0)
            {
                ChangeStateSH(_player.MoveState);
            }
            else if (_animFinished)
            {
                ChangeStateSH(_player.IdleState);
            }
        }
    }
}