using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinLabel : MonoBehaviour
{
    int coinCnt;
    TextMeshProUGUI coinTxt;
    Coroutine anim;


    private void Awake()
    {
        coinTxt = transform.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        ShowCoinsCount();
        CoinController.instance.AddCoinListener(CoinsCountAnim);
    }
    private void OnDisable()
    {
        CoinController.instance.RemoveCoinListener(CoinsCountAnim);
        if (anim != null)
        {
            StopCoroutine(anim);
        }
    }

    void ShowCoinsCount()
    {
        coinCnt = ApplicationModel.Coins;

        if (coinTxt != null)
            coinTxt.text = coinCnt.ToString();
        else
            Debug.Log("coin text is null!");
    }

    public void CoinsCountAnim()
    {
        if (anim != null)
        {
            StopCoroutine(anim);
        }
        anim = StartCoroutine(CoinsChangeAnim());
    }
    IEnumerator CoinsChangeAnim()
    {
        int curr = coinCnt;
        int to = ApplicationModel.Coins;
        float duration = 0.2f;
        float time = 0f;

        coinCnt = to;
        if (coinTxt == null)
        {
            Debug.Log("coin text is null!");
            yield break;
        }

        while (time < duration)
        {
            time += Time.deltaTime;
            int cnt = curr + (int)((to - curr) * time / duration);
            coinTxt.text = cnt.ToString();
            yield return null;
        }
        coinTxt.text = to.ToString();
    }
}
