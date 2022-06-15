 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public abstract void EnterState(GameStateManager game);

    public abstract void ExitState(GameStateManager game);

    public abstract void UpdateState(GameStateManager game);
}
