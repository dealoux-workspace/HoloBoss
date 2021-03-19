using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeaLoux.Player
{
    public class PlayerDamagedState : PlayerState
    {
        public PlayerDamagedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            //PlayAudio(AudioID.SFX_Yagoo_BokurawaHololive);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            //_grounded = _player.Grounded();

            if (_animFinished)
                ChangeStateSH(_player.AerialState);
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            _player.MoveHorizontally(_playerData.knockbackDistance * -_playerData.FacingDir);
        }
    }
}