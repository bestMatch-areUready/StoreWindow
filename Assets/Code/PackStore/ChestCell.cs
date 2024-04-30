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
    public int index = 0;
    ChestJsonData data;

    List<GameObject> itemGOPool = new List<GameObject>();

    /// <summary>
    /// 礼包是否可被购买
    /// </summary>
    public bool ChestRemained
    {
        get
        {
            return ((ApplicationModel.PurchaseTag & (1 << index)) == 0);
        }
        set
        {
            if (!value)
            {
                ApplicationModel.PurchaseTag |= (1 << index);
            }
            else
            {
                ApplicationModel.PurchaseTag &= ~(1 << index);
            }

        }
    }

    void cantPurchase()
    {
        transform.Find("remain").GetComponentInChildren<TextMeshProUGUI>().text = "0/1";
        transform.Find("mask").gameObject.SetActive(true);
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

        transform.Find("name").GetComponentInChildren<TextMeshProUGUI>().text = content.name;
        if (data.type == 0)
        {
            transform.Find("coin").GetComponent<Image>().enabled = false;
            transform.Find("coin").GetComponentInChildren<TextMeshProUGUI>().text = "$" + content.cost;
        }
        else
        {
            transform.Find("coin").GetComponent<Image>().enabled = true;
            transform.Find("coin").GetComponentInChildren<TextMeshProUGUI>().text = content.coins.ToString();
        }

        Sprite image = null;
        try
        {
             image= ResourcesManager.Instance.ChestBG(content.bg);
        }
        catch(NullReferenceException e)
        {
            Debug.Log(e);
        }
        transform.Find("icon").GetComponent<Image>().sprite = image;

        SetContentImage();

        if (ChestRemained)
        {
            transform.Find("remain").GetComponentInChildren<TextMeshProUGUI>().text = "1/1";
            transform.Find("mask").gameObject.SetActive(false);
            transform.GetComponent<Button>().onClick.AddListener(() =>
            {
                if (data != null)
                {
                    if (data.type == 0)
                    {
                        PopupManager.instance.ConfirmPopup.Show(data, () =>
                        {
                            CoinController.instance.AddCoins(data.coins);
                            ChestRemained = false;
                            cantPurchase();
                            transform.GetComponent<Button>().onClick.RemoveAllListeners();
                        });

                    }
                    else
                    {
                        if (ApplicationModel.Coins >= data.coins)
                        {
                            PopupManager.instance.ConfirmPopup.Show(data, () =>
                            {
                                CoinController.instance.ConsumeCoins(data.coins);
                                ChestRemained = false;
                                cantPurchase();
                                transform.GetComponent<Button>().onClick.RemoveAllListeners();
                            });

                        }
                        else
                        {
                            ShopWindowBoard.instance.ChangeSelectTab(ShopWindowBoard.PageContent.Coin);
                        }
                    }
                }
            });
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
        foreach(Transform child in transform.Find("content"))
        {
            child.gameObject.SetActive(false);
        }
        if (data.coins != 0 && data.type == 0)
        {
            transform.Find("content/item").gameObject.SetActive(true);
            transform.Find("content/item").GetComponentInChildren<TextMeshProUGUI>().text = data.coins.ToString();
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

        GameObject newGO = Instantiate(transform.Find("content/item").gameObject, transform.Find("content"));
        itemGOPool.Add(newGO);

        return newGO;
    }

    public void ClearUp()
    {
        transform.GetComponent<Button>().onClick.RemoveAllListeners();
        foreach(Transform child in transform.Find("content"))
        {
            child.gameObject.SetActive(false);
        }
    }
}
