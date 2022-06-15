using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateCardState : State
{
    public override void EnterState(GameStateManager game)
    {
        CapsaGameManager.instance.CalculatePlayerHands(this);
    }

    public override void ExitState(GameStateManager game)
    {
        
    }

    public override void UpdateState(GameStateManager game)
    {
        
    }
}
