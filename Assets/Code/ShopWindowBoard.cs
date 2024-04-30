using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopWindowBoard : MonoBehaviour
{
    public enum PageContent
    {
        Chest,
        Prop,
        Coin
    }

    [SerializeField]
    private ShopEdgeMenu menu;

    [SerializeField]
    private GameObject chestView;

    [SerializeField]
    private GameObject storeView;

    public static ShopWindowBoard instance;

    private void Awake()
    {
        menu.onBtnClicked += changePage;

        if (instance == null)
            instance = this;
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
            storeView.GetComponent<StoreTableView>().ChangeSelectTab(menu.CurrentIndex - 1);
        }
    }

    public void ChangeSelectTab(PageContent index)
    {
        menu.OnButtonClick((int)index);
    }
}
