/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtkState : PlayerState
{
    public PlayerAtkState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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

        if(_stateMachine.LastState is PlayerIdleState)
        {
            _animBoolName = "primAtkIdle";
        }
        else if(_stateMachine.LastState is PlayerInAirState)
        {
            _animBoolName = "primAtkInAir";
        }
        else if (_stateMachine.LastState is PlayerDashState)
        {
            _animBoolName = "primAtkDash";
        }
        else if (_stateMachine.LastState is PlayerWallSlideState)
        {
            _animBoolName = "primAtkWall";
        }
        else if (_stateMachine.LastState is PlayerMoveState)
        {
            _animBoolName = "primAtkMove";
        }

        _stateMachine.LastState.LogicUpdate();
        _player.Anim.SetBool(_animBoolName, true);

        if (_animFinished)
        {
            _player.InputHandler.TickPrimAtkInput();
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
*/