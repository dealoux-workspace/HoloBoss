using UnityEngine;

namespace DeaLoux.Player
{
    public class PlayerStateMachine : CoreSystems.Patterns.PushDownAutomata<PlayerState>
    {
        public override void ChangeState(PlayerState newState, bool savePrevious = false)
        {
            if (newState != CurrState)
                base.ChangeState(newState, savePrevious);
        }
    }
}