using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public static CoinController instance;

    public delegate void CoinControllerDelegate();
    public static CoinControllerDelegate onCoinChange;

    private void Awake()
    {
        if(instance == null)
            instance = this;

        ShowCoinsCount();
        onCoinChange += CoinsCountAnim;
    }

    void ShowCoinsCount()
    {
        transform.GetComponentInChildren<TextMeshProUGUI>().text = ApplicationModel.Coins.ToString(); ;
    }

    Coroutine anim;
    public void CoinsCountAnim()
    {
        if(anim != null)
        {
            StopCoroutine(anim);
        }
        anim = StartCoroutine(CoinsChangeAnim());
    }
    IEnumerator CoinsChangeAnim()
    {
        int curr = int.Parse(transform.GetComponentInChildren<TextMeshProUGUI>().text);
        int to = ApplicationModel.Coins;
        float duration = 0.2f;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            int cnt = (int)((to - curr) * time / duration + curr);
            transform.GetComponentInChildren<TextMeshProUGUI>().text = cnt.ToString();
            yield return null;
        }
        transform.GetComponentInChildren<TextMeshProUGUI>().text = to.ToString();
    }

    public void AddCoins(int count)
    {
        ApplicationModel.Coins += count;
        onCoinChange?.Invoke();
    }

    public void ConsumeCoins(int count)
    {
        ApplicationModel.Coins -= count;
        onCoinChange?.Invoke();
    }
}
