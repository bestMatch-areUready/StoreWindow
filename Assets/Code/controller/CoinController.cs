using UnityEngine;
using UnityEngine.Events;

public class CoinController : MonoBehaviour
{
    public static CoinController instance;

    //public delegate void CoinControllerDelegate();
    //public static CoinControllerDelegate onCoinChange;

    public UnityEvent onCoinChanged;

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }

    public void AddCoins(int count)
    {
        ApplicationModel.Coins += count;
        onCoinChanged?.Invoke();
    }

    public void ConsumeCoins(int count)
    {
        ApplicationModel.Coins -= count;
        onCoinChanged?.Invoke();
    }
}
