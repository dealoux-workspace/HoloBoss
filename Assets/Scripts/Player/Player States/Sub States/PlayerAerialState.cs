using System.Collections;
using UnityEngine;

namespace DeaLoux.Entity
{
    public class PlayerAerialState : PlayerState
    {
        bool jumping;
        bool coyote;
        bool wallLeapCoyote;
        float wallLeapCoyoteStartTime;
        bool wallTouchedLastFrame;
        bool dashedLastFrame;

        public PlayerAerialState(Player player, PlayerStateMachine stateMachine, EntityData data, PlayerData playerData, string animBoolName) : base(player, stateMachine, data, playerData, animBoolName) { }

        public override void Enter()
        {
            base.Enter();

            coyote = true;
            wallTouchedLastFrame = _wallTouched;
            _data.dashJumpMultiplier = dashedLastFrame ? 1.24f : 1f;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (!_exitingState)
            {
                CheckCoyoteTime();
                CheckWallLeapCoyoteTime();

                _player.Anim.SetFloat("yVelocity", _player.Controller.velocity.y);

                JumpingCheck();

                _player.ShouldFlip(_xInput);
                _player.MoveHorizontally(_data.movementSpeed * _data.dashJumpMultiplier * _xInput);

                if (_grounded && _player.Controller.velocity.y < 0.01f)
                {
                    ChangeStateSH(_player.LandingState);
                }
                else if (_jumpInput && !jumping)
                {
                    if (_wallTouched && _player.WallJumpState.CoolDownDone())
                    {
                        if (_xInput != 0 && _xInput != _data.facingDir)
                            _player.WallJumpState.Leap();
                        ChangeStateSH(_player.WallJumpState);
                    }

                    else if (!_wallTouched && wallTouchedLastFrame && wallLeapCoyote && _player.WallJumpState.CoolDownDone())
                    {
                        wallLeapCoyote = false;
                        _player.WallJumpState.Leap();
                        ChangeStateSH(_player.WallJumpState);
                    }
                    else if (_player.JumpState.CanJump())
                        ChangeStateSH(_player.JumpState);
                }
                else if (_wallTouched && _xInput == _data.facingDir && _player.Controller.velocity.y < 0)
                {
                    ChangeStateSH(_player.WallSlideState);
                }
                else if (_primAtkInput && !_wallTouched)
                {
                    switch (_data.slot1.type)
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
                    switch (_data.slot2.type)
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
                    switch (_data.slot1.Ctype)
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
                    switch (_data.slot2.Ctype)
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

        public override void Exit()
        {
            base.Exit();

            dashedLastFrame = false;
        }

        void JumpingCheck()
        {
            if (jumping)
            {
                if (dashedLastFrame)
                {
                    _player.DashState.ShouldPlaceAfterImage();
                }

                else if (_player.Controller.velocity.y <= 0.0f)
                {
                    jumping = false;
                }
            }
        }

        void CheckCoyoteTime()
        {
            if (coyote && Time.time > _startTime + _playerData.coyoteTime)
            {
                coyote = false;
                _player.JumpState.DecreaseAmount();
            }
        }

        void CheckWallLeapCoyoteTime()
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

        public void DashedLastFrame() => dashedLastFrame = true;

        public void Jumping() => jumping = true;
    }
}