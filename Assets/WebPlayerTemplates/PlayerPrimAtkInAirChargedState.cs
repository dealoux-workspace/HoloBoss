using UnityEngine;

public class PlayerPrimAtkInAirChargedState : PlayerState
{
    public PlayerPrimAtkInAirChargedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _player.InputHandler.TickPrimAtkCharged();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_animFinished)
        {
            _stateMachine.ChangeToPreviousState();
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
