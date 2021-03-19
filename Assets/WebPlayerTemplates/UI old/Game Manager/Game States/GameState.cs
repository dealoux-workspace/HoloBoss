using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState : IState
{
    public GameState()
    {

    }

    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
    }

    public virtual void LogicUpdate()
    {
    }

    public virtual void PhysicsUpdate()
    {
    }
}
