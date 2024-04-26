using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EdgeHider : MonoBehaviour
{
    public enum Position
    {
        leftEdge,
        rightEdge,
        upperEdge,
        bottomEdge,
    }

    public Position boardPos;

    [SerializeField]
    private bool isShowing = false;
    [SerializeField]
    private float showingDuration = 0.8f;

    private float defaultPostion;

    private void Start()
    {
        if ((int)boardPos < 2)
        {
            defaultPostion = GetComponent<RectTransform>().anchoredPosition.x;
        }
        else
        {
            defaultPostion = GetComponent<RectTransform>().anchoredPosition.y;
        }
    }


    public void OnButtonClicked()
    {
        if(isShowing)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    /// <summary>
    /// function to move board from outside
    /// </summary>
    public void Show()
    {
        if (!isShowing)
        {
            isShowing = true;
            GetComponent<RectTransform>().DOComplete();
            if ((int)boardPos < 2)
            {
                GetComponent<RectTransform>().DOAnchorPosX(-defaultPostion, showingDuration).SetEase(Ease.OutBack).OnStart(() =>
                {
                    GetComponent<IEdgeHidedCallback>().OnEdgeShowed();
                }); ;
            }
            else
            {
                GetComponent<RectTransform>().DOAnchorPosY(-defaultPostion, showingDuration).SetEase(Ease.OutBack).OnStart(() =>
                {
                    GetComponent<IEdgeHidedCallback>().OnEdgeShowed();
                }); ;
            }
        }
    }

    /// <summary>
    /// function to move board to outside
    /// </summary>
    public void Hide()
    {
        if (isShowing)
        {
            isShowing = false;
            GetComponent<RectTransform>().DOComplete();

            if ((int)boardPos < 2)
            {
                GetComponent<RectTransform>().DOAnchorPosX(defaultPostion, showingDuration).SetEase(Ease.OutBack).OnStart(() =>
                {
                    GetComponent<IEdgeHidedCallback>().OnEdgeHided();
                });
            }
            else
            {
                GetComponent<RectTransform>().DOAnchorPosY(defaultPostion, showingDuration).SetEase(Ease.OutBack).OnStart(() =>
                {
                    GetComponent<IEdgeHidedCallback>().OnEdgeHided();
                });
            }
        }
    }
}
