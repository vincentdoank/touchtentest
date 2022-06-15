using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System;

public class CapsaGameManager : MonoBehaviour {

    public const int ROYALFLUSH = 9;
    public const int STRAIGHT_FLUSH = 8;
    public const int FOUR_OF_A_KIND = 7;
    public const int FULLHOUSE = 6;
    public const int FLUSH = 5;
    public const int STRAIGHT = 4;
    public const int THREE_OF_A_KIND = 3;
    public const int TWO_PAIR = 2;
    public const int PAIR = 1;
    public const int HIGH_CARD = 0;
    public const int ERROR = -1;

    public Dictionary<int, string> cardValueDict = new Dictionary<int, string>() {
        { -1, "ERROR"},
        { 0, "HIGH CARD" },
        { 1, "PAIR" },
        { 2, "TWO PAIR" },
        { 3, "TRIPLE" },
        { 4, "STRAIGHT" },
        { 5, "FLUSH" },
        { 6, "FULL HOUSE" },
        { 7, "FOUR OF A KIND" },
        { 8, "STRAIGHT FLUSH" },
        { 9, "ROYAL FLUSH" }
    };

    public CardDeck cardDeck;
    public List<CapsaPlayerController> capsaPlayerList;
    public TMP_Text gameTimerText;
    public const int waitingTime = 60;
    private float currentTime = 0;
    public DefaultAvatar avatar;

    public GameStateManager gameStateManager;

    public int initialBet = 10000;

    public enum GameState
    {
        WAITING = 0,
        START_GAME = 1,
        CALCULATING = 2
    }



    public static CapsaGameManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void StartGame(Action onShareCardCompleted)
    {
        StartTimer();
        StartCoroutine(ShareCards(onShareCardCompleted));
    }

    private void StartTimer()
    {
        currentTime = waitingTime;
    }

    public void OnPlayerJoined(CapsaPlayerController player)
    {
        capsaPlayerList.Add(player);
    }

    public void OnPlayerLeft(CapsaPlayerController player)
    {
        capsaPlayerList.Remove(player);
    }

    public void OnPlayerStateChanged(CapsaPlayerController player)
    {
        bool allReady = true;
        foreach (CapsaPlayerController p in capsaPlayerList)
        {
            if (p.playerState == CapsaPlayerController.PlayerState.MIXING)
                allReady = false;
        }
        if (allReady)
        {
            gameStateManager.SwitchState(gameStateManager.calculateCardState);
        }
    }

    public IEnumerator ShareCards(Action onShareCardCompleted)
    {
        for (int i = 0; i < 13; i++)
        {
            foreach (CapsaPlayerController capsaController in capsaPlayerList)
            {
                /*
                if (capsaController.isMine)
                {
                    capsaController.DrawCardDummy(true, i);
                }
                else
                {
                */
                    capsaController.DrawCard(capsaController.isMine);
                //}
                yield return new WaitForSeconds(0.1f);
            }
        }
        onShareCardCompleted?.Invoke();
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 200, 50), "FPS : " + 1f / Time.unscaledDeltaTime);
    }

    public float UpdateGameTime()
    {
        currentTime -= Time.deltaTime;
        if (currentTime < 0) currentTime = 0;
        gameTimerText.text = Mathf.FloorToInt(currentTime).ToString();
        return currentTime;
    }

    public void CalculatePlayerHands(State state)
    {
        //var upperList = capsaPlayerList.OrderByDescending(x => x.upperCardScore);
        //var middleList = capsaPlayerList.OrderByDescending(x => x.middleCardScore);
        //var bottomList = capsaPlayerList.OrderByDescending(x => x.bottomCardScore);

        var winList = capsaPlayerList.Where(x => x.totalScore > 0).OrderByDescending(x => x.totalScore);
        var loseList = capsaPlayerList.Where(x => x.totalScore < 1).OrderByDescending(x => x.totalScore);

        foreach (CapsaPlayerController pc in winList)
        {
            pc.OnWinState();
        }

        foreach (CapsaPlayerController pc in loseList)
        {
            pc.OnLoseState();
            pc.chips -= initialBet;
        }
    }
}
