using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    private State currentState;
    public CalculateCardState calculateCardState = new CalculateCardState();
    public DrawCardsState drawCardsState = new DrawCardsState();
    public MixingCardsState mixingCardsState = new MixingCardsState();

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);
        currentState = drawCardsState;
        currentState.EnterState(this);
    }

    private void Update()
    {
        if (currentState != null)
            currentState.UpdateState(this);
    }

    public void SwitchState(State state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
