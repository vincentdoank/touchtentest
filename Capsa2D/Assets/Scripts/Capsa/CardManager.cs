using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class CardChance
{
    public List<int> selectedIndex;
    public float chance;

    public CardChance(List<int> selectedIndex, float chance)
    {
        this.selectedIndex = selectedIndex;
        this.chance = chance;
    }
}

[System.Serializable]
public class CardInfo
{
    public string cardID;
    public Sprite cardImage;
    public int value;
}

public class CardManager : MonoBehaviour {

    [SerializeField]
    private List<CardInfo> cardList;
    private Dictionary<string, CardInfo> cardDict;

    public List<CardChance> cardChanceList;

    public static CardManager instance;

    private void Awake()
    {
        instance = this;
        SetCardInfo();
        Util.ConvertMoneyToChip(Random.Range(100, 1231232));
    }

    private void SetCardInfo()
    {
        cardDict = new Dictionary<string, CardInfo>();
        foreach (CardInfo info in cardList)
        {
            cardDict.Add(info.cardID, info);
        }
    }

    public List<CardInfo> GetCardList()
    {
        return cardList;
    }

    public List<CardInfo> GetCardList(string[] exceptionIds)
    {
        var query = cardList.Where(q => exceptionIds.Contains(q.cardID));
        return query.ToList();
    }

    public CardInfo GetCard(string cardID)
    {
        if (!cardDict.ContainsKey(cardID))
        {
            return null;
        }
        return cardDict[cardID];
    }

    public CardInfo GetCard(int index)
    {
        return GetCard(GetAllCardID()[index]);
    }

    public Sprite GetImage(string cardID)
    {
        if (!cardDict.ContainsKey(cardID))
        {
            return null;
        }
        return cardDict[cardID].cardImage;
    }

    public List<string> GetAllCardID()
    {
        var query = cardList.Select(q => q.cardID);
        return query.ToList();
    }
}
