using Data;
using UnityEngine;

namespace DeaLoux.Player
{
    public class PlayerAerialState : PlayerState
    {
        private bool jumping;
        private bool coyote;
        private bool wallLeapCoyote;
        private float wallLeapCoyoteStartTime;
        private bool wallTouchedLastFrame;

        public PlayerAerialState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) { }

        public override void Enter()
        {
            base.Enter();

            coyote = true;
            wallTouchedLastFrame = _wallTouched;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (!_exitingState)
            {
                CheckCoyoteTime();
                CheckWallLeapCoyoteTime();

                _player.Anim.SetFloat("yVelocity", _player.PlayerController.velocity.y);

                JumpingCheck();

                _player.ShouldFlip(_xInput);
                _player.MoveHorizontally(_playerData.movementSpeed * _playerData.dashJumpMultiplier * _xInput);

                if (_grounded && _player.PlayerController.velocity.y < 0.01f)
                {
                    ChangeStateSH(_player.LandingState);
                }
                else if (_jumpInput && !jumping)
                {
                    if (_wallTouched && _player.WallJumpState.CoolDownDone())
                        ChangeStateSH(_player.WallJumpState);
                    else if (!_wallTouched && wallTouchedLastFrame && wallLeapCoyote && _player.WallLeapState.CoolDownDone())
                    {
                        wallLeapCoyote = false;
                        ChangeStateSH(_player.WallLeapState);
                    }
                    else if (_player.JumpState.CanJump())
                        ChangeStateSH(_player.JumpState);
                }
                else if (_wallTouched && _xInput == _playerData.FacingDir && _player.PlayerController.velocity.y < 0)
                {
                    ChangeStateSH(_player.WallSlideState);
                }
                else if (_primAtkInput && !_wallTouched)
                {
                    switch (_playerData.slot1.type)
                    {
                        case EquipmentType.FOUR_WAY:
                            ChangeStateSH(_player.PrimAtkAerial4State);
                            break;
                        case EquipmentType.EIGHT_WAY:
                            ChangeStateSH(_player.PrimAtkAerial8State);
                            break;
                    }
                }

                else if (_secAtkInput && !_wallTouched)
                {
                    switch (_playerData.slot2.type)
                    {
                        case EquipmentType.FOUR_WAY:
                            ChangeStateSH(_player.SecAtkAerial4State);
                            break;
                        case EquipmentType.EIGHT_WAY:
                            ChangeStateSH(_player.SecAtkAerial8State);
                            break;
                    }
                }

                else if (_primAtkCharged)
                {
                    switch (_playerData.slot1.Ctype)
                    {
                        case EquipmentType.FOUR_WAY:
                            _player.AtkChargedState.SetAnim("primAtkAerial4Charged");
                            break;

                        case EquipmentType.EIGHT_WAY:
                            _player.AtkChargedState.SetAnim("primAtkAerial8Charged");
                            break;
                    }
                    ChangeStateSH(_player.AtkChargedState, true);
                }

                else if (_secAtkCharged)
                {
                    switch (_playerData.slot2.Ctype)
                    {
                        case EquipmentType.FOUR_WAY:
                            _player.AtkChargedState.SetAnim("secAtkAerial4Charged");
                            break;

                        case EquipmentType.EIGHT_WAY:
                            _player.AtkChargedState.SetAnim("secAtkAerial8Charged");
                            break;
                    }
                    ChangeStateSH(_player.AtkChargedState, true);
                }
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void Exit()
        {
            base.Exit();

            if (_dashInput)
                _player.InputHandler.TickDashInput();

            _playerData.dashJumpMultiplier = 1f;
        }

        private void JumpingCheck()
        {
            if (jumping)
            {
                if (_dashInput && _xInput != 0)
                {
                    _player.DashState.ShouldPlaceAfterImage();
                }

                else if (_player.PlayerController.velocity.y <= 0.0f)
                {
                    jumping = false;
                }
            }
        }

        private void CheckCoyoteTime()
        {
            if (coyote && Time.time > _startTime + _playerData.coyoteTime)
            {
                coyote = false;
                _player.JumpState.DecreaseAmount();
            }
        }

        private void CheckWallLeapCoyoteTime()
        {
            if (wallLeapCoyote && Time.time > wallLeapCoyoteStartTime + _playerData.coyoteTime)
            {
                wallLeapCoyote = false;
            }
        }

        public void StartWallLeapCoyoteTime()
        {
            wallLeapCoyote = true;
            wallLeapCoyoteStartTime = Time.time;
        }

        public void SetJumpingStatus() => jumping = true;
    }
}