using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCardsState : State
{
    public override void EnterState(GameStateManager game)
    {
        CapsaGameManager.instance.StartGame(() => game.SwitchState(game.mixingCardsState));
    }

    public override void ExitState(GameStateManager game)
    {
        
    }

    public override void UpdateState(GameStateManager game)
    {
        
    }
}
