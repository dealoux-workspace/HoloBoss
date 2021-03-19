using Data;
using UnityEngine;

namespace DeaLoux.Player
{
    public abstract class PlayerState : CoreSystems.Patterns.IState
    {
        protected Player _player;
        protected PlayerStateMachine _stateMachine;
        protected PlayerData _playerData;

        protected bool _animFinished;
        protected bool _exitingState;
        protected float _startTime;
        public string _animBoolName;

        protected int _xInput;
        protected bool _dashInput;
        protected bool _jumpInput;
        protected bool _primAtkInput;
        protected bool _atkToggled;
        protected bool _grounded;
        protected bool _wallTouched;

        public PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName)
        {
            _player = player;
            _stateMachine = stateMachine;
            _playerData = playerData;
            _animBoolName = animBoolName;
        }

        public virtual void Enter()
        {
            _player.Anim.SetBool(_animBoolName, true);
            _startTime = Time.time;
            _animFinished = false;
            _exitingState = false;
            //Debug.Log(_animBoolName);
        }

        public virtual void Exit()
        {
            _player.Anim.SetBool(_animBoolName, false);
            _exitingState = true;
        }

        public virtual void LogicUpdate()
        {
            _atkToggled = _player.InputHandler.AtkToggleInput;
        }

        public virtual void PhysicsUpdate()
        {
        }

        protected void ChangeStateSH(PlayerState state, bool savePrevious = false) => _stateMachine.ChangeState(state, savePrevious);
        public virtual void AnimTrigger() { }
        public virtual void AnimFinishTrigger() => _animFinished = true;
    }
}