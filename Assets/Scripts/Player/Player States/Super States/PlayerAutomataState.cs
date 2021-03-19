using Data;
using UnityEngine;

namespace DeaLoux.Player
{
    public abstract class PlayerAutomataState : PlayerState
    {
        public PlayerAutomataState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public virtual void SetAnim(string name)
        {
            _animBoolName = name;
            _animID = Animator.StringToHash(name);
        }
    }
}