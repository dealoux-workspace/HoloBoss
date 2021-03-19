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
        public int _animID;

        protected int _xInput;
        protected bool _dashInput;
        protected bool _jumpInput;
        protected bool _primAtkInput;
        protected bool _primAtkCharged;
        protected bool _secAtkInput;
        protected bool _secAtkCharged;
        protected bool _grounded;
        protected bool _wallTouched;

        public PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName)
        {
            _player = player;
            _stateMachine = stateMachine;
            _playerData = playerData;
            _animBoolName = animBoolName;
            _animID = Animator.StringToHash(animBoolName);
        }

        public virtual void Enter()
        {
            _player.Anim.SetBool(_animID, true);
            _startTime = Time.time;
            _animFinished = false;
            _exitingState = false;
            //Debug.Log(_animBoolName);
        }

        public virtual void Exit()
        {
            _player.Anim.SetBool(_animID, false);
            _exitingState = true;
        }

        public virtual void LogicUpdate()
        {
            Inputs();
        }

        public virtual void PhysicsUpdate()
        {
        }

        protected void Inputs()
        {
            _grounded = _player.Grounded();
            _wallTouched = _player.WallTouched();

            _primAtkInput = _player.InputHandler.PrimAtkInput;
            _primAtkCharged = _player.InputHandler.PrimAtkCharged;
            _secAtkInput = _player.InputHandler.SecAtkInput;
            _secAtkCharged = _player.InputHandler.SecAtkCharged;

            _xInput = _player.InputHandler.NormInputX;
            _jumpInput = _player.InputHandler.JumpInput;
            _dashInput = _player.InputHandler.DashInput;
        }

        protected void ChangeStateSH(PlayerState state, bool savePrevious = false) => _stateMachine.ChangeState(state, savePrevious);
        public virtual void AnimTrigger() { }
        public virtual void AnimFinishTrigger() => _animFinished = true;
    }
}