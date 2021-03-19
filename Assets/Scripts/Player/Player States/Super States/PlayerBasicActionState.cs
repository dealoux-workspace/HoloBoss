using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeaLoux.Player
{
    public abstract class PlayerBasicActionState : PlayerState
    {
        protected bool isActionDone;

        public PlayerBasicActionState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) { }

        public override void Enter()
        {
            base.Enter();

            isActionDone = false;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (isActionDone)
            {
                if (_grounded && _player.PlayerController.velocity.y < 0.01f)
                {
                    ChangeStateSH(_player.IdleState);
                }

                else
                {
                    ChangeStateSH(_player.AerialState);
                }
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}