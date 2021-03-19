
using UnityEngine;

public class PlayerPrimAtkIdle2OldState : PlayerIdleState
{
    public PlayerPrimAtkIdle2OldState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _player.InputHandler.TickPrimAtkInput();

        AudioController.instance.PlayAudio(AudioID.SFX_Yagoo_BokurawaHololive, true);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_primAtkInput)
        {
            ChangeStateSH(_player.PrimAtkIdle3State);
        }

        if (_animFinished)
        {
            ChangeStateSH(_player.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
} 
