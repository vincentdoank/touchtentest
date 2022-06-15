using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixingCardsState : State
{
    public override void EnterState(GameStateManager game)
    {
        
    }

    public override void ExitState(GameStateManager game)
    {
        
    }

    public override void UpdateState(GameStateManager game)
    {
        float currentTime = CapsaGameManager.instance.UpdateGameTime();
        if (currentTime <= 0)
        {
            game.SwitchState(game.calculateCardState);
        }
    }
}
