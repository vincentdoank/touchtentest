using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerCardController : MonoBehaviour {

    public enum PlayerType
    {
        player,
        AI
    }

    public PlayerType playerType = PlayerType.player; 

    public enum PreferenceCardPosition
    {
        horizontal,
        vertical
    }

    public PreferenceCardPosition cardPositioning;

    public bool isEmpty;
    public bool isMine;
    public bool isReady = false;
    public string playerId;
    public int cardCount;
    private List<Card> cards = new List<Card>();
    public PlayerProfile profile;
    public BetController betController;

    protected int currentBet;
    public int chips;

    public int CurrentBet
    {
        get
        {
            return currentBet;
        }
    }

    public List<Card> Cards
    {
        get
        {
            return cards;
        }
    }

    protected virtual void OnEnable()
    {
        //Debug.Log("OnEnable : " + profile);
        //if (profile != null)
        //{
        //    isMine = false;
        //    isEmpty = true;
        //    profile.gameObject.SetActive(false);
        //} 
    }

    protected virtual void Awake()
    {
        OnPlayerEnter();
    }

    public void Init()
    {
        if (playerType == PlayerType.player)
        {
            chips = Random.Range(10000, 100000);
            if (isMine)
            {
                PlayerAtr.instance.TotalChips = chips;
                profile.Init();
            }
            else
            {
                profile.Init("", 0, 0, 0);
            }
        }
    }

    public virtual void OnPlayerEnter()
    {
        Init();
    }

    public virtual void OnPlayerLeave()
    {

    }

    public void InitBetController(List<int> betList)
    {
        if (betController != null)
        {
            betController.Init(this, betList);
        }
    }

    public void AddCard(Card card)
    {
        Cards.Add(card);
    }

    public void RemoveCard()
    {

    }

    public virtual void DrawCard(bool faceUp)
    {

    }

    public virtual void OnPlaceBet(int index)
    {
        
    }

    protected virtual void Reposition(float angle)
    {
        Debug.Log("Reposition.. cards count : " + Cards.Count);
        if (Cards.Count < 2)
        {
            return;
        }
        for (int i = 0; i < Cards.Count; i++)
        {
            float dist = 0f;
            if (cards.Count == 2)
            {
                dist = 1.5f;
            }
            else
            {
                dist = 3f / cards.Count;
            }
            Transform card = Cards[i].transform;
            if (cardPositioning == PreferenceCardPosition.horizontal)
            {
                Tweener tweenPos = card.DOMoveX(transform.position.x + i * dist - dist * (cards.Count - 1)/2f, 0.5f).SetEase(Ease.OutQuad);
                tweenPos.Play();
                if (angle > 0)
                {
                    Tweener tweenRot = card.DOLocalRotate(new Vector3(0, 0, i * angle - 15), 0.5f).SetEase(Ease.OutQuad);
                    tweenRot.Play();
                }
            }
            else
            {
                Tweener tweenPos = card.DOMoveY(transform.position.y + i * dist - dist * (cards.Count - 1) / 2f, 0.5f).SetEase(Ease.OutQuad);
                tweenPos.Play();
                if (angle > 0)
                {
                    Tweener tweenRot = card.DOLocalRotate(new Vector3(0, 0, i * angle - 15), 0.5f).SetEase(Ease.OutQuad);
                    tweenRot.Play();
                }
            }
        }
    }
}
