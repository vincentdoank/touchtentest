using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Card : MonoBehaviour {

    public bool canFacedown;
    public bool canFaceup;

    public Image faceupImage;
    public Image facedownImage;
    public GameObject highlightObj;

    private Canvas faceupCanvas;
    private Canvas facedownCanvas;
    private Sequence seq;


    public enum CardState
    {
        faceup = 0,
        facedown = 1
    }

    public CardState state;
    public bool canFlipped = true;
    public bool canHighlighted = false;

    [HideInInspector]
    public PlayerCardController owner;

    public delegate void OnCardSelected(Card card, PlayerCardController owner);
    public static event OnCardSelected onCardSelected;

    public CardInfo Info { get; set; }

    private void Awake()
    {
        faceupCanvas = faceupImage.GetComponent<Canvas>();
        facedownCanvas = facedownImage.GetComponent<Canvas>();
    }

    public void SetCard(PlayerCardController owner, CardInfo info, bool faceUp = false)
    {
        Info = info;
        this.owner = owner;
        faceupImage.sprite = Info.cardImage;
        if (faceUp)
        {
            state = CardState.faceup;
            faceupCanvas.sortingOrder = 1;
            facedownCanvas.sortingOrder = 0;
        }
        else
        {
            state = CardState.facedown;
            faceupCanvas.sortingOrder = 0;
            facedownCanvas.sortingOrder = 1;
        }
    }

    public void FlipCard()
    {
        if (!canFlipped)
        {
            return;
        }

        seq = DOTween.Sequence();
        if (state == CardState.faceup)
        {
            FlipFaceDown();
        }
        else
        {
            FlipFaceUp();
        }
    }

    private void Flip(bool full = false)
    {
        Debug.Log("Flip card : " + state.ToString());
        Vector3 dir = Vector3.zero;

        if (full)
        {
            if (state == CardState.facedown)
            {
                if (!canFaceup)
                {
                    return;
                }
                Tweener tween = transform.DOLocalRotate(new Vector3(0, 0, 0), 0.2f);
                tween.SetEase(Ease.Linear);
                state = CardState.faceup;
                tween.Play();
            }
            else
            {
                Debug.Log("canFacedown : " + canFacedown);
                if (!canFacedown)
                {
                    return;
                }
                Tweener tween = transform.DOLocalRotate(new Vector3(0, 0, 0), 0.2f);
                tween.SetEase(Ease.Linear);
                state = CardState.facedown;
                Debug.Log("run tweenrotate to 180");
            }   
        }
        else
        {
            Debug.Log("rotate");
            Tweener tween = transform.DOLocalRotate(new Vector3(0, 90, 0), 0.2f);
            tween.SetEase(Ease.Linear);
            tween.OnComplete(() => OnHalfwayFlip());
            seq.Append(tween);
        }
    }

    private void FlipFaceUp()
    {
        Debug.Log("flip face up");
        Tweener tween = transform.DOLocalRotate(new Vector3(0, 90, 0), 0.2f);
        seq.Append(tween);
        seq.AppendCallback(() => OnHalfwayFlip());
        tween = transform.DOLocalRotate(new Vector3(0, 0, 0), 0.2f);
        state = CardState.faceup;
        seq.Append(tween);
        seq.Play();
    }

    private void FlipFaceDown()
    {
        Debug.Log("flip face down");
        Tweener tween = transform.DOLocalRotate(new Vector3(0, 90, 0), 0.2f);
        tween.SetEase(Ease.Linear);
        seq.Append(tween);
        seq.AppendCallback(() => OnHalfwayFlip());
        tween = transform.DOLocalRotate(new Vector3(0, 0, 0), 0.2f);
        state = CardState.facedown;
        seq.Append(tween);
        seq.Play();
    }

    private void OnHalfwayFlip()
    {
        Debug.Log("continue");
        if (state == CardState.faceup)
        {
            faceupCanvas.sortingOrder = 1;
            facedownCanvas.sortingOrder = 0;
        }
        else
        {
            faceupCanvas.sortingOrder = 0;
            facedownCanvas.sortingOrder = 1;
        }
    }

    public void OnSelected()
    {
        if (canHighlighted)
        {
            if (onCardSelected != null)
            {
                onCardSelected(this, owner);
            }
        }
    }
}
