using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeck : MonoBehaviour {

    public string cardPrefabName = "Card";
    public int deckCount;
    public bool includeJoker;
    public bool canDuplicate;
    private Vector3 position;
    private List<Card> cardList;
    private List<CardInfo> playedCardInfoList;
    private List<CardInfo> cardInfoList;

    private List<CardInfo> shuffledCardList;

    public Vector3 Position
    {
        get
        {
            return transform.position;
        }
    }

    private void Awake()
    {
        cardList = new List<Card>();
        playedCardInfoList = new List<CardInfo>();
        cardInfoList = new List<CardInfo>();
        shuffledCardList = new List<CardInfo>();
        SetDeck();
        Pooling();
    }

    private void Pooling()
    {
        for (int i = 0; i < cardInfoList.Count; i++)
        {
            SpawnCard();
        }
    }

    private void SetDeck()
    {
        List<CardInfo> cardInfoList = CardManager.instance.GetCardList();
        cardList = new List<Card>();
        for (int i = 0; i < deckCount; i++)
        {
            for (int j = 0; j < cardInfoList.Count; j++)
            {
                if (cardInfoList[j].cardID == "" || cardInfoList[j].cardID == " ")
                {
                    continue;
                }
                if (!includeJoker)
                {
                    if (cardInfoList[j].cardID.Contains("JOKER"))
                    {
                        continue;
                    }
                }
                playedCardInfoList.Add(cardInfoList[j]);
                this.cardInfoList.Add(cardInfoList[j]);
            }
        }

        Shuffle();

        /*
        List<CardInfo> shuffledCardList = Shuffle();
        for (int i = 0; i < shuffledCardList.Count; i++)
        {
            SpawnCard(shuffledCardList[i], (i + 1));
        }
        */
    }

    private void SpawnCard()
    {
        GameObject go = Instantiate(Resources.Load<GameObject>("Prefabs/" + cardPrefabName));
        go.transform.SetParent(transform, false);
        go.transform.localScale = Vector3.one;
        go.transform.localPosition = Vector3.zero;
        go.SetActive(false);
        cardList.Add(go.GetComponent<Card>());
    }

    public void Shuffle()
    {
        List<CardInfo> randomPlayedCardList = new List<CardInfo>();
        for (int i = 0; i < cardInfoList.Count; i++)
        {
            int rand = Random.Range(0, playedCardInfoList.Count);
            randomPlayedCardList.Add(playedCardInfoList[rand]);
            playedCardInfoList.Remove(playedCardInfoList[rand]);
        }
        shuffledCardList = randomPlayedCardList;
    }

    public CardInfo DrawCard(PlayerCardController playerCardController, bool removeDrawnCard)
    {
        if (removeDrawnCard)
        {
            if (shuffledCardList.Count > 0)
            {
                CardInfo card = shuffledCardList[0];
                shuffledCardList.RemoveAt(0);
                return card;
            }
        }
        else
        {
            DrawCard(playerCardController);
        }
        return null;
    }

    public CardInfo DrawCard(PlayerCardController playerCardController)
    {
        float maxRange = 0f;
        List<CardInfo> cardList = new List<CardInfo>();
        List<CardChance> splittedCardChance = new List<CardChance>();

        List<CardChance> cardChanceList = CardManager.instance.cardChanceList;
        foreach (CardChance c in cardChanceList)
        {
            maxRange += c.chance;
            for (int i = 0; i < c.chance; i++)
            {
                splittedCardChance.Add(c);
            }
        }
        int rand = Random.Range(0, Mathf.FloorToInt(maxRange));
        return CardManager.instance.GetCard(CardManager.instance.GetAllCardID()[splittedCardChance[rand].selectedIndex[Random.Range(0, splittedCardChance[rand].selectedIndex.Count)]]);
    }

    public Card DrawDummyCard(PlayerCardController playerCardController, int index)
    {
        Card card = GetCard();
        if (card != null)
        {
            card.SetCard(playerCardController, CardManager.instance.GetCard(index), true);
        }

        return card;
    }

    public Card DrawPhysicalCard(PlayerCardController playerCardController, bool faceUp = false, bool removeDrawnCard = false)
    {
        if (removeDrawnCard)
        {
            Card card = GetCard();
            if (card != null)
            {
                CardInfo cardInfo = DrawCard(playerCardController, true);
                if (cardInfo.cardID != "" && cardInfo.cardID != " ")
                {
                    card.SetCard(playerCardController, cardInfo, faceUp);
                }
                else
                {
                    card.gameObject.SetActive(faceUp);
                }
            }
            return card;
        }
        else
        {
            return DrawPhysicalCard(playerCardController, faceUp);
        }
    }

    public Card DrawPhysicalCard(PlayerCardController playerCardController, bool faceUp = false)
    {
        float maxRange = 0f;
        List<CardInfo> cardList = new List<CardInfo>();
        List<CardChance> splittedCardChance = new List<CardChance>(); ;

        List<CardChance> cardChanceList = CardManager.instance.cardChanceList;
        foreach (CardChance c in cardChanceList)
        {
            maxRange += c.chance;
            for (int i = 0; i < c.chance; i++)
            {
                splittedCardChance.Add(c);
            }
        }
        int rand = Random.Range(0, Mathf.FloorToInt(maxRange));
        Card card = GetCard();
        if (card != null)
        {
            /*
            if (playerCardController.playerType == PlayerCardController.PlayerType.house)
            {
                if (playerCardController.Cards.Count == 0)
                {
                    card.SetCard(CardManager.instance.GetCard(0), faceUp);
                }
                else
                {
                    card.SetCard(CardManager.instance.GetCard(Random.Range(0, 50)), faceUp);
                }
            }
            else
            {
              */  
                Debug.Log("splittedCardChanceCount : " + splittedCardChance.Count);
                int finalRand = Random.Range(0, splittedCardChance[rand].selectedIndex.Count);
                card.SetCard(playerCardController, CardManager.instance.GetCard(splittedCardChance[rand].selectedIndex[finalRand]), faceUp);
            //}
        }
        return card;
    }

    public Card GetCard()
    {
        for (int i = 0; i < cardList.Count; i++)
        {
            if (!cardList[i].gameObject.activeInHierarchy)
            {
                cardList[i].gameObject.SetActive(true);
                return cardList[i];
            }
        }

        return null;
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 100, 50), "Draw"))
        {
            DrawPhysicalCard(null, true, true);
        }
    }
}
