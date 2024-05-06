using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIKit;
using UnityEditor;
using UnityEngine.UI;
using TMPro;
using System;

public class ChestCell : UITableViewCell
{

    #region variables
    [HideInInspector]
    public new int index = 0;
    ChestJsonData data;
    int purchaseTimes;
    bool chestRamained;
    List<GameObject> itemGOPool = new List<GameObject>();

    [SerializeField]
    private GameObject packItemGO;
    [SerializeField]
    private Image packIcon;
    [SerializeField]
    private TextMeshProUGUI packNameText;
    [SerializeField]
    private GameObject consumeLabel;
    [SerializeField]
    private TextMeshProUGUI remainText;
    [SerializeField]
    private GameObject packMask;
    [SerializeField]
    private GameObject packContent;
    #endregion

    #region variables_accessor
    public GameObject PackItemGO
    {
        get
        {
            if (packItemGO == null)
            {
                Debug.LogError("pack item go is null");
            }
            return packItemGO;
        }
    }
    public Image PackIcon
    {
        get
        {
            if (packIcon == null)
            {
                Debug.LogError("pack icon is null");
            }
            return packIcon;
        }
    }
    public TextMeshProUGUI PackNameText
    {
        get
        {
            if (packNameText == null)
            {
                Debug.LogError("pack name text is null");
            }
            return packNameText;
        }
    }
    public GameObject ConsumeLabel
    {
        get
        {
            if (consumeLabel == null)
            {
                Debug.LogError("consume label is null");
            }
            return consumeLabel;
        }
    }
    public TextMeshProUGUI RemainText
    {
        get
        {
            if (remainText == null)
            {
                Debug.LogError("remain text is null");
            }
            return remainText;
        }
    }
    public GameObject PackMask
    {
        get
        {
            if (packMask == null)
            {
                Debug.LogError("pack mask is null");
            }
            return packMask;
        }
    }
    public GameObject PackContent
    {
        get
        {
            if (packContent == null)
            {
                Debug.LogError("pack content is null");
            }
            return packContent;
        }
    }
    /// <summary>
    /// 关于道具已购买次数的访问器
    /// </summary>
    public int PurchaseTimes
    {
        get
        {
            int cnt = PlayerPrefs.GetInt(ConstKey.ChestPurchaseTimes + index, 0);
            return cnt;
        }
        set
        {
            purchaseTimes = value;
            PlayerPrefs.SetInt(ConstKey.ChestPurchaseTimes + index, value);
        }
    }
    #endregion

    /// <summary>
    /// 礼包是否可被购买
    /// </summary>
    public bool ChestRemained
    {
        get
        {
            return chestRamained;
        }
        set
        {
            chestRamained = value;
        }
        //get
        //{
        //    return ((ApplicationModel.PurchaseTag & (1 << index)) == 0);
        //}
        //set
        //{
        //    if (!value)
        //    {
        //        ApplicationModel.PurchaseTag |= (1 << index);
        //    }
        //    else
        //    {
        //        ApplicationModel.PurchaseTag &= ~(1 << index);
        //    }
        //
        //}
    }

    void cantPurchase()
    {
        RemainText.text = "0/" + data.PackCount;
        PackMask.SetActive(true);
    }

    /// <summary>
    /// 刷新生成单个界面内的礼包内容
    /// </summary>
    /// <param name="index"></param>
    /// <param name="content"></param>
    public void UpdateData(int index, ChestJsonData content)
    {
        this.index = index;
        data = content;
        purchaseTimes = PurchaseTimes;

        if (purchaseTimes < content.PackCount)
        {
            ChestRemained = true;
        }
        else
        {
            ChestRemained = false;
        }

        if (PackNameText != null)
            PackNameText.text = content.name;

        if (ConsumeLabel != null)
        {
            if (data.type == 0)
            {
                ConsumeLabel.GetComponent<Image>().enabled = false;
                ConsumeLabel.GetComponentInChildren<TextMeshProUGUI>().text = "$" + content.cost;
            }
            else
            {
                ConsumeLabel.GetComponent<Image>().enabled = true;
                ConsumeLabel.GetComponentInChildren<TextMeshProUGUI>().text = content.coins.ToString();
            }
        }

        Sprite image = ResourcesManager.Instance.ChestBG(content.bg);

        if (PackIcon != null)
            PackIcon.sprite = image;

        SetContentImage();

        if (ChestRemained)
        {
            if (RemainText != null)
                RemainText.text = (content.PackCount - purchaseTimes) + "/" + content.PackCount;

            if (PackMask != null)
                PackMask.SetActive(false);
        }
        else
        {
            cantPurchase();
        }
    }


    /// <summary>
    /// 设置礼包内道具预览
    /// </summary>
    void SetContentImage()
    {
        foreach(Transform child in PackContent.transform)
        {
            child.gameObject.SetActive(false);
        }
        if (data.coins != 0 && data.type == 0)
        {
            if (PackItemGO != null)
            {
                PackItemGO.SetActive(true);
                PackItemGO.GetComponentInChildren<TextMeshProUGUI>().text = data.coins.ToString();
            }
        }
        for(int i = 0; i < data.content.Count; i++)
        {
            if (data.content[i] != null)
            {
                string itemID = data.content[i].id;
                int itemCount = data.content[i].cnt;
                if (itemCount <= 0) continue;
                GameObject itemIcon = GetGoInPool();
                itemIcon.SetActive(true);
                itemIcon.GetComponentInChildren<TextMeshProUGUI>().text = itemCount.ToString();
                itemIcon.GetComponent<Image>().sprite = ResourcesManager.Instance.ItemIcon(itemID);
            }
        }
    }

    /// <summary>
    /// 获取一个礼包内的道具图标
    /// </summary>
    /// <returns></returns>
    GameObject GetGoInPool()
    {
        foreach(var go in itemGOPool)
        {
            if (!go.activeInHierarchy)
            {
                return go;
            }
        }

        GameObject newGO = Instantiate(PackItemGO, PackContent.transform);
        itemGOPool.Add(newGO);

        return newGO;
    }

    public void ClearUp()
    {
        foreach(Transform child in PackContent.transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    public void PackPurchaseClicked()
    {
        if (!ChestRemained)
        {
            return;
        }

        if (data != null)
        {
            if (data.type == (int)ChestJsonData.packType.realMoneyPayments)
            {
                PopupManager.instance.ConfirmPopup.Show(data, () =>
                {
                    CoinController.instance.AddCoins(data.coins);
                    PurchaseTimes++;

                    if (RemainText != null)
                        RemainText.text = (data.PackCount - purchaseTimes) + "/" + data.PackCount;
                    if (purchaseTimes >= data.PackCount)
                    {
                        ChestRemained = false;
                        cantPurchase();
                    }
                    
                });

            }
            else
            {
                if (ApplicationModel.Coins >= data.coins)
                {
                    PopupManager.instance.ConfirmPopup.Show(data, () =>
                    {
                        CoinController.instance.ConsumeCoins(data.coins);
                        PurchaseTimes++;

                        if (RemainText != null)
                            RemainText.text = (data.PackCount - purchaseTimes) + "/" + data.PackCount;
                        if (purchaseTimes >= data.PackCount)
                        {
                            ChestRemained = false;
                            cantPurchase();
                        }
                        //
                    });

                }
                else
                {
                    ShopWindowBoard.instance.ChangeSelectTab(ShopWindowBoard.PageContent.Coin);
                }
            }
        }
    }
}
