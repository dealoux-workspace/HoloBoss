using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeaLoux.Player
{
    public class PlayerLandingState : PlayerGroundedState
    {
        public PlayerLandingState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}