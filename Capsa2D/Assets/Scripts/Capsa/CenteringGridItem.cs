using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CenteringGridItem : MonoBehaviour {

    public enum SortingType
    {
        Horizontal = 0,
        Vertical = 1
    }

    public SortingType sortingType;

    public float distance;

    private List<Transform> itemList;

    private void OnEnable()
    {
        Refresh();
    }

    public void Refresh()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (sortingType == SortingType.Horizontal)
            {
                Tweener tween = transform.GetChild(i).DOLocalMoveX(distance * i - distance * (transform.childCount - 1) / 2f, 0.2f);
                tween.SetEase(Ease.InOutQuad);
                tween.Play();
            }
            else
            {
                Tweener tween = transform.GetChild(i).DOLocalMoveY(distance * i - distance * (transform.childCount - 1) / 2f, 0.2f);
                tween.SetEase(Ease.InOutQuad);
                tween.Play();
            }
        }
    }
}
