using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopWindowBoard : MonoBehaviour
{
    [SerializeField]
    private ShopEdgeMenu menu;

    [SerializeField]
    private GameObject chestView;

    [SerializeField]
    private GameObject storeView;

    private void Awake()
    {
        menu.onBtnClicked += changePage;
    }

    void changePage()
    {
        chestView.SetActive(false);
        storeView.SetActive(false);

        if (menu.CurrentIndex == 0)
        {
            chestView.SetActive(true);
        }
        else
        {
            storeView.SetActive(true);
        }
    }
}
