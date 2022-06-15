using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;

public class CapsaPlayerController : PlayerCardController {

    private const int UPPER_LIMIT = 3;
    private const int MIDDLE_LIMIT = 5;
    private const int BOTTOM_LIMIT = 5;

    public CenteringGridItem upperGrid;
    public CenteringGridItem middleGrid;
    public CenteringGridItem bottomGrid;

    public Text upperCardValueText;
    public Text middleCardValueText;
    public Text bottomCardValueText;

    public List<CardInfo> upperCards = new List<CardInfo>();
    public List<CardInfo> middleCards = new List<CardInfo>();
    public List<CardInfo> bottomCards = new List<CardInfo>();

    public List<int> upperCardsValue = new List<int>();
    public List<int> middleCardsValue = new List<int>();
    public List<int> bottomCardsValue = new List<int>();

    public int upperCardScore;
    public int middleCardScore;
    public int bottomCardScore;

    public int totalScore;

    private List<CardInfo>[] cardsOnHand;
    private List<int>[] cardsValueOnHand;
    private List<int> positionValues = new List<int>();

    private List<Card> selectedCards = new List<Card>();

    public enum PlayerState
    {
        MIXING = 0,
        READY = 1
    }

    public PlayerState playerState;

    protected override void Awake()
    {
        base.Awake();
        cardsOnHand = new List<CardInfo>[3];
        cardsValueOnHand = new List<int>[3];
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        if (isMine)
        {
            Card.onCardSelected += OnSelectCard;
        }
        CapsaGameManager.instance.OnPlayerJoined(this);
    }

    private void OnDisable()
    {
        if (isMine)
        {
            Card.onCardSelected -= OnSelectCard;
        }
        CapsaGameManager.instance.OnPlayerLeft(this);
    }

    public void Reset()
    {
        profile.avatar.Reset();
    }

    public void DrawCardDummy(bool faceUp, int index)
    {
        CapsaCard card = (CapsaCard)CapsaGameManager.instance.cardDeck.DrawDummyCard(this, index);
        if (upperGrid.transform.childCount < UPPER_LIMIT)
        {
            card.Position = 0;
            upperCards.Add(card.Info);
            card.transform.SetParent(upperGrid.transform, false);
            card.transform.position = CapsaGameManager.instance.cardDeck.transform.position;
            Tweener tween = card.transform.DOMove(upperGrid.transform.position, 0.2f);
            tween.SetEase(Ease.Linear);
            tween.OnComplete(() => {
                upperGrid.Refresh();
            });
            tween.Play();
        }
        else if (middleGrid.transform.childCount < MIDDLE_LIMIT)
        {
            card.Position = 1;
            middleCards.Add(card.Info);
            card.transform.SetParent(middleGrid.transform, false);
            card.transform.position = CapsaGameManager.instance.cardDeck.transform.position;
            Tweener tween = card.transform.DOMove(middleGrid.transform.position, 0.2f);
            tween.SetEase(Ease.Linear);
            tween.OnComplete(() => {
                middleGrid.Refresh();
            });
            tween.Play();
        }
        else if (bottomGrid.transform.childCount < BOTTOM_LIMIT)
        {
            card.Position = 2;
            bottomCards.Add(card.Info);
            card.transform.SetParent(bottomGrid.transform, false);
            card.transform.position = CapsaGameManager.instance.cardDeck.transform.position;
            Tweener tween = card.transform.DOMove(bottomGrid.transform.position, 0.2f);
            tween.SetEase(Ease.Linear);
            tween.OnComplete(() => {
                bottomGrid.Refresh();
            });
            tween.Play();
        }

        if (upperGrid.transform.childCount == UPPER_LIMIT && middleGrid.transform.childCount == MIDDLE_LIMIT && bottomGrid.transform.childCount == BOTTOM_LIMIT)
        {
            cardsOnHand[0] = new List<CardInfo>();
            cardsOnHand[0].AddRange(upperCards);
            Debug.Log("AddUpper");

            cardsOnHand[1] = new List<CardInfo>();
            cardsOnHand[1].AddRange(middleCards);

            cardsOnHand[2] = new List<CardInfo>();
            cardsOnHand[2].AddRange(bottomCards);
        }
    }

    public override void DrawCard(bool faceUp)
    {
        CapsaCard card = (CapsaCard)CapsaGameManager.instance.cardDeck.DrawPhysicalCard(this, faceUp, true);
        if (upperGrid.transform.childCount < UPPER_LIMIT)
        {
            card.Position = 0;
            upperCards.Add(card.Info);
            card.transform.SetParent(upperGrid.transform, false);
            card.transform.position = CapsaGameManager.instance.cardDeck.transform.position;
            Tweener tween = card.transform.DOMove(upperGrid.transform.position, 0.2f);
            tween.SetEase(Ease.Linear);
            tween.OnComplete(() => {
                upperGrid.Refresh();
            });
            tween.Play();
        }
        else if (middleGrid.transform.childCount < MIDDLE_LIMIT)
        {
            card.Position = 1;
            middleCards.Add(card.Info);
            card.transform.SetParent(middleGrid.transform, false);
            card.transform.position = CapsaGameManager.instance.cardDeck.transform.position;
            Tweener tween = card.transform.DOMove(middleGrid.transform.position, 0.2f);
            tween.SetEase(Ease.Linear);
            tween.OnComplete(() => {
                middleGrid.Refresh();
            });
            tween.Play();
        }
        else if (bottomGrid.transform.childCount < BOTTOM_LIMIT)
        {
            card.Position = 2;
            bottomCards.Add(card.Info);
            card.transform.SetParent(bottomGrid.transform, false);
            card.transform.position = CapsaGameManager.instance.cardDeck.transform.position;
            Tweener tween = card.transform.DOMove(bottomGrid.transform.position, 0.2f);
            tween.SetEase(Ease.Linear);
            tween.OnComplete(() => {
                bottomGrid.Refresh();
            });
            tween.Play();
        }

        if (upperGrid.transform.childCount == UPPER_LIMIT && middleGrid.transform.childCount == MIDDLE_LIMIT && bottomGrid.transform.childCount == BOTTOM_LIMIT)
        {
            cardsOnHand[0] = new List<CardInfo>();
            cardsOnHand[0].AddRange(upperCards);
            Debug.Log("AddUpper");

            cardsOnHand[1] = new List<CardInfo>();
            cardsOnHand[1].AddRange(middleCards);

            cardsOnHand[2] = new List<CardInfo>();
            cardsOnHand[2].AddRange(bottomCards);
        }
    }

    public void OnSelectCard(Card card, PlayerCardController owner)
    {
        if (owner == this)
        {
            CapsaCard capsaCard = (CapsaCard)card;
            if (selectedCards.Count < 2)
            {
                Debug.Log("cardID : " + card.Info.cardID);
                if (selectedCards.Contains(card))
                {
                    cardsOnHand[capsaCard.Position].Add(card.Info);
                    card.highlightObj.SetActive(false);
                    selectedCards.Remove(card);
                }
                else
                {
                    cardsOnHand[capsaCard.Position].Remove(card.Info);
                    selectedCards.Add(card);
                    card.highlightObj.SetActive(true);
                    if (selectedCards.Count == 2)
                    {
                        SwapCard();
                    }
                }
            }
        }
    }

    private void SwapCard()
    {
        CardInfo card1 = selectedCards[0].Info;
        CardInfo card2 = selectedCards[1].Info;

        CardInfo temp = card1;
        card1 = card2;
        card2 = temp;

        CapsaCard capsaCard1 = (CapsaCard)selectedCards[0];
        CapsaCard capsaCard2 = (CapsaCard)selectedCards[1];

        cardsOnHand[capsaCard1.Position].Add(card1);
        cardsOnHand[capsaCard2.Position].Add(card2);

        selectedCards[0].SetCard(this, card1, true);
        selectedCards[1].SetCard(this, card2, true);

        selectedCards[0].highlightObj.SetActive(false);
        selectedCards[1].highlightObj.SetActive(false);
        selectedCards.Clear();
    }

    private int GetHighestCard(int position, bool aceAsOne = true)
    {
        int max = 0;
        bool hasAce = false;
        for (int i = 0; i < cardsOnHand[position].Count; i++)
        {
            if (cardsOnHand[position][i].value == 1)
            {
                hasAce = true;
            }
            if (cardsOnHand[position][i].value > max)
            {
                max = cardsOnHand[position][i].value;
            }
        }

        if (!aceAsOne)
        {
            if (max == 13 && hasAce)
            {
                return 14;
            }
        }

        return max;
    }

    private int GetLowestCard(int position, bool aceAsOne = true)
    {
        int min = 999;
        for (int i = 0; i < cardsOnHand[position].Count; i++)
        {
            if (cardsOnHand[position][i].value < min)
            {
                if (!aceAsOne)
                {
                    if (cardsOnHand[position][i].value == 1)
                    {
                        continue;
                    }
                }
                min = cardsOnHand[position][i].value;
            }
        }
        return min;
    }

    public int CheckHasStraightFlush(int position)
    {
        for (int i = 0; i < cardsValueOnHand[position].Count; i++)
        {
            Debug.LogError("val : " + cardsValueOnHand[position][i]);
        }

        if (CheckHasStraight(position))
        {
            if (CheckHasFlush(position))
            {
                Debug.LogWarning("STRAIGHT FLUSH CHECKING ROYAL FLUSH");
                int flag = 0;
                for (int i = 10; i <= 14; i++)
                {
                    Debug.LogWarning(cardsValueOnHand[position][flag] + " " + i);
                    if (cardsValueOnHand[position].Contains(1))
                    {
                        continue;
                    }

                    if (!cardsValueOnHand[position].Contains(i))
                    {
                        return CapsaGameManager.STRAIGHT_FLUSH;
                    }
                    flag += 1;
                }
                if (cardsValueOnHand[position].Contains(1))
                {
                    return CapsaGameManager.ROYALFLUSH;
                }
                
            }
            return CapsaGameManager.STRAIGHT;
        }
        else if (CheckHasFlush(position))
        {
            return CapsaGameManager.FLUSH;
        }
        return 0;
    }

    public int CheckHasPair(int position)
    {
        int value = 0;
        Dictionary<int, int> sameKindDict = new Dictionary<int, int>();
        for (int i = 0; i < cardsOnHand[position].Count; i++)
        {
            if (sameKindDict.ContainsKey(cardsOnHand[position][i].value))
            {
                sameKindDict[cardsOnHand[position][i].value] += 1;
            }
            else
            {
                sameKindDict.Add(cardsOnHand[position][i].value, 1);
            }
        }

        foreach (KeyValuePair<int, int> kvp in sameKindDict)
        {
            if (kvp.Value == 2)
            {
                value += CapsaGameManager.PAIR;
            }
            else if (kvp.Value == 3)
            {
                value += CapsaGameManager.THREE_OF_A_KIND;
                foreach (KeyValuePair<int, int> kvp2 in sameKindDict)
                {
                    if (kvp2.Key != kvp.Key)
                    {
                        if (kvp2.Value == 2)
                        {
                            return CapsaGameManager.FULLHOUSE;
                        }
                    }
                }
            }
            else if (kvp.Value == 4)
            {
                value += CapsaGameManager.FOUR_OF_A_KIND;
            }
        }

        return value;
    }

    public bool CheckHasFlush(int position)
    {
        if (position == 0)
        {
            return false;
        }

        if (cardsOnHand[position][0].cardID.Contains(Util.SPADE))
        {
            for (int i = 1; i < cardsOnHand[position].Count; i++)
            {
                if (!cardsOnHand[position][i].cardID.Contains(Util.SPADE))
                {
                    return false;
                }
            }
            return true;
        }
        else if (cardsOnHand[position][0].cardID.Contains(Util.CLUB))
        {
            for (int i = 1; i < cardsOnHand[position].Count; i++)
            {
                if (!cardsOnHand[position][i].cardID.Contains(Util.CLUB))
                {
                    return false;
                }
            }
            return true;
        }
        else if (cardsOnHand[position][0].cardID.Contains(Util.DIAMOND))
        {
            for (int i = 1; i < cardsOnHand[position].Count; i++)
            {
                if (!cardsOnHand[position][i].cardID.Contains(Util.DIAMOND))
                {
                    return false;
                }
            }
            return true;
        }
        else if (cardsOnHand[position][0].cardID.Contains(Util.HEART))
        {
            for (int i = 1; i < cardsOnHand[position].Count; i++)
            {
                if (!cardsOnHand[position][i].cardID.Contains(Util.HEART))
                {
                    return false;
                }
            }
            return true;
        }
        return false;
    }

    public bool CheckHasStraight(int position)
    {
        if (position == 0)
        {
            return false;
        }
        Debug.LogWarning("CheckhasStraight");
        List<int> cardValues = new List<int>();

        for (int i = 0; i < cardsOnHand[position].Count; i++)
        {
            cardValues.Add(cardsOnHand[position][i].value);
        }
        if (GetLowestCard(position) == 1)
        {
            Debug.LogWarning("lowest = 1");
            int lowestCardAceAsOne = GetLowestCard(position);
            int highestCardAceAsOne = GetHighestCard(position);

            int lowestCard = GetLowestCard(position, false);
            int highestCard = GetHighestCard(position, false);

            if (highestCard - lowestCard == 4 && highestCard == 14)
            {
                Debug.LogWarning("H - L : 4");
                Debug.LogWarning("L : " + lowestCard + " H : " + highestCard);
                int flag = 0;
                for (int i = lowestCard; i <= highestCard; i++)
                {
                    Debug.LogWarning(cardsValueOnHand[position][flag] + " " + i);
                    if (cardsValueOnHand[position][flag] == 1)
                    {
                        continue;
                    }
                    if (!cardsValueOnHand[position].Contains(i))
                    {
                        return false;
                    }
                    flag += 1;
                }
                return true;
            }
            else if (highestCardAceAsOne - lowestCardAceAsOne == 4 && highestCard == 5)
            {
                for (int i = lowestCardAceAsOne; i <= highestCardAceAsOne; i++)
                {
                    if (!cardsValueOnHand[position].Contains(i))
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        else
        {
            Debug.LogWarning("noAce");
            if (GetHighestCard(position) - GetLowestCard(position) == 4)
            {
                for (int i = GetLowestCard(position); i <= GetHighestCard(position); i++)
                {
                    Debug.LogError(i);
                    if (!cardsValueOnHand[position].Contains(i))
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        return false;
    }
    public bool[] CheckHasError()
    {
        bool[] hasError = new bool[3];
        positionValues.Sort();
        hasError[2] = positionValues[0] < positionValues[1];
        hasError[1] = positionValues[1] < positionValues[2] || positionValues[1] > positionValues[0];
        hasError[0] = positionValues[2] > positionValues[1];

        return hasError;
    }

    public void Check()
    {
        for (int j = 0; j < cardsOnHand[0].Count; j++)
        {
            upperCardsValue.Add(cardsOnHand[0][j].value);
        }

        for (int j = 0; j < cardsOnHand[1].Count; j++)
        {
            middleCardsValue.Add(cardsOnHand[1][j].value);
        }

        for (int j = 0; j < cardsOnHand[2].Count; j++)
        {
            bottomCardsValue.Add(cardsOnHand[2][j].value);
        }

        upperCardsValue.Sort();
        middleCardsValue.Sort();
        bottomCardsValue.Sort();

        cardsValueOnHand[0] = upperCardsValue;
        cardsValueOnHand[1] = middleCardsValue;
        cardsValueOnHand[2] = bottomCardsValue;

        
        for (int i = 0; i < 3; i++)
        {
            int val = 0;
            val += CheckHasStraightFlush(i);
            val += CheckHasPair(i);

            Debug.Log(i + " : " + val);
        }
    }

    public void CheckUpperCards()
    {
        upperCardsValue = new List<int>();
        for (int j = 0; j < cardsOnHand[0].Count; j++)
        {
            upperCardsValue.Add(cardsOnHand[0][j].value);
        }

        upperCardsValue.Sort();

        cardsValueOnHand[0] = upperCardsValue;

        int val = 0;
        val += CheckHasStraightFlush(0);
        val += CheckHasPair(0);

        upperCardValueText.transform.parent.gameObject.SetActive(true);
        middleCardValueText.transform.parent.gameObject.SetActive(false);
        bottomCardValueText.transform.parent.gameObject.SetActive(false);
        upperCardValueText.text = CapsaGameManager.instance.cardValueDict[val];
    }

    public void CheckMiddleCards()
    {
        middleCardsValue = new List<int>();
        for (int j = 0; j < cardsOnHand[1].Count; j++)
        {
            middleCardsValue.Add(cardsOnHand[1][j].value);
        }

        middleCardsValue.Sort();

        cardsValueOnHand[1] = middleCardsValue;

        int val = 0;
        val += CheckHasStraightFlush(1);
        Debug.LogWarning("hasStraightFlush : " + val);
        val += CheckHasPair(1);
        Debug.LogWarning("totVal : " + val);
        upperCardValueText.transform.parent.gameObject.SetActive(false);
        middleCardValueText.transform.parent.gameObject.SetActive(true);
        bottomCardValueText.transform.parent.gameObject.SetActive(false);
        middleCardValueText.text = CapsaGameManager.instance.cardValueDict[val];
    }

    public void CheckBottomCards()
    {
        bottomCardsValue = new List<int>();
        for (int j = 0; j < cardsOnHand[2].Count; j++)
        {
            bottomCardsValue.Add(cardsOnHand[2][j].value);
        }

        bottomCardsValue.Sort();

        cardsValueOnHand[2] = bottomCardsValue;

        int val = 0;
        val += CheckHasStraightFlush(2);
        val += CheckHasPair(2);

        upperCardValueText.transform.parent.gameObject.SetActive(false);
        middleCardValueText.transform.parent.gameObject.SetActive(false);
        bottomCardValueText.transform.parent.gameObject.SetActive(true);
        bottomCardValueText.text = CapsaGameManager.instance.cardValueDict[val];
    }

    public void OnWinState()
    {
        profile.avatar.OnWinState();
    }

    public void OnLoseState()
    {
        profile.avatar.OnLoseState();
    }

    public void OnGUI()
    {
        if (isMine)
        {
            if (GUI.Button(new Rect(0, 100, 100, 50), "CHECK UPPER"))
            {
                CheckUpperCards();
            }

            if (GUI.Button(new Rect(0, 150, 100, 50), "CHECK MIDDLE"))
            {
                CheckMiddleCards();
            }

            if (GUI.Button(new Rect(0, 200, 100, 50), "CHECK BOTTOM"))
            {
                CheckBottomCards();
            }
        }
    }

}
