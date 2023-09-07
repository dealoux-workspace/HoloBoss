using System.Collections;

namespace DeaLoux.CoreSystems.Patterns
{
    public interface IState
    {
        void Enter();

        void Exit();

        void LogicUpdate();
    }
}
