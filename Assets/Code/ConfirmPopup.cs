using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmPopup : MonoBehaviour
{
    [SerializeField]
    RectTransform wrap;
    [SerializeField]
    private GameObject consume;
    [SerializeField]
    private GameObject purchase;
    [SerializeField]
    private GameObject lackCoins;

    [SerializeField]
    private Button yesButton;
    [SerializeField]
    private Button noButton;


    public void Show(ChestJsonData data, Action onPurchased, Action beforeShow = null, Action afterShow = null)
    {
        beforeShow?.Invoke();
        gameObject.SetActive(true);
        lackCoins.SetActive(false);
        wrap.localScale = Vector3.zero;

        if (data.type == (int)ChestJsonData.packType.realMoneyPayments)
        {
            consume.SetActive(false);
            purchase.SetActive(true);
        }
        else
        {
            consume.SetActive(true);
            purchase.SetActive(false);

            GameObject from = consume.transform.Find("from").gameObject;
            GameObject to = consume.transform.Find("to").gameObject;
            TextMeshProUGUI coinFrom = from.GetComponentInChildren<TextMeshProUGUI>();
            TextMeshProUGUI coinTo = to.GetComponentInChildren<TextMeshProUGUI>();
            int result = ApplicationModel.Coins - data.coins;

            if (coinFrom != null)
                coinFrom.text = ApplicationModel.Coins.ToString();
            else Debug.Log("from_text not found");

            if (coinTo != null)
                coinTo.text = (result).ToString();
            else Debug.Log("to_text not found");
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

    public void lackOfCoin(Action onConfirm,Action beforeShow = null,Action afterShow = null)
    {
        beforeShow?.Invoke();

        gameObject.SetActive(true);
        consume.SetActive(false);
        purchase.SetActive(false);
        lackCoins.SetActive(true);

        wrap.localScale = Vector3.zero; wrap.DOScale(1f, 0.25f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            afterShow?.Invoke();
        });

        yesButton.onClick.AddListener(() =>
        {
            onConfirm?.Invoke();
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
