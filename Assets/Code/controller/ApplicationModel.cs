using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationModel
{
    private static int coinCount;

    private static int purchaseTag;

    public static int Coins
    {
        get { return coinCount; }
        set
        {
            coinCount = value;
            PlayerPrefs.SetInt("coin", value);
        }
    }

    public static int PurchaseTag
    {
        get { return purchaseTag; }
        set
        {
            purchaseTag = value;
            PlayerPrefs.SetInt("purTag", value);
        }
    }

    static ApplicationModel()
    {
        loadOrInit();
    }


    static void loadOrInit()
    {
        coinCount = PlayerPrefs.GetInt("coin", 100);

        //PurchaseTag = 0;
        purchaseTag = PlayerPrefs.GetInt("purTag", 0);
    }
}
