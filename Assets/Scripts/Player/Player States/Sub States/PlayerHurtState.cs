using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeaLoux.Entity
{
    public class PlayerHurtState : PlayerState
    {
        public PlayerHurtState(Player player, PlayerStateMachine stateMachine, EntityData data, PlayerData playerData, string animBoolName) : base(player, stateMachine, data, playerData, animBoolName)
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

            //_player.MoveHorizontally(_playerData.knockbackDistance * _playerData.knockbackDir.x);
            _player.HandleMovement(_data.knockbackDir);

            if (_animFinished)
                ChangeStateSH(_player.AerialState);
        }
    }
}