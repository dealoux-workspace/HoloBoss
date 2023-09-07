using System.Collections;
using UnityEngine;

namespace DeaLoux.Entity
{
    public abstract class PlayerAutomataState : PlayerState
    {
        public PlayerAutomataState(Player player, PlayerStateMachine stateMachine, EntityData data, PlayerData playerData, string animBoolName) : base(player, stateMachine, data, playerData, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }

        public virtual void SetAnim(string name)
        {
            _animBoolName = name;
            _animID = Animator.StringToHash(name);
        }
    }
}