using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmPopup : MonoBehaviour
{
    RectTransform wrap;

    private GameObject consume;
    private GameObject purchase;

    [SerializeField]
    private Button yesButton;
    [SerializeField]
    private Button noButton;

    private void Awake()
    {
        wrap = transform.GetChild(0).GetComponent<RectTransform>();
        consume = wrap.Find("consume").gameObject;
        purchase = wrap.Find("purchase").gameObject;
    }

    public void Show(ChestJsonData data, Action onPurchased, Action beforeShow = null, Action afterShow = null)
    {
        beforeShow?.Invoke();

        gameObject.SetActive(true);
        wrap.localScale = Vector3.zero;
        if (data.type == 0)
        {
            consume.SetActive(false);
            purchase.SetActive(true);
        }
        else
        {
            consume.SetActive(true);
            purchase.SetActive(false);
            consume.transform.Find("from").GetComponentInChildren<TextMeshProUGUI>().text = ApplicationModel.Coins.ToString();
            int result = ApplicationModel.Coins - data.coins;
            consume.transform.Find("to").GetComponentInChildren<TextMeshProUGUI>().text = (result).ToString();
        }

        wrap.DOScale(1f, 0.25f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            afterShow?.Invoke();
        });


        yesButton.onClick.AddListener(() =>
        {
            onPurchased?.Invoke();
            Dismiss();
        });
        noButton.onClick.AddListener(() =>
        {
            Dismiss();
        });
    }

    public void Dismiss()
    {
        yesButton.onClick.RemoveAllListeners();
        noButton.onClick.RemoveAllListeners();
        wrap.DOScale(0f, 0.2f).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }   
}
