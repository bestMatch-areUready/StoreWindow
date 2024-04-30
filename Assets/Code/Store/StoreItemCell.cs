using System.Collections;
using System.Collections.Generic;
using TMPro;
using UIKit;
using UnityEngine;
using UnityEngine.UI;

public class StoreItemCell : UITableViewCell
{


    /// <summary>
    /// update cell content
    /// </summary>
    /// <param name="cellIndex"></param>
    /// <param name="selectedTabIndex"></param>
    /// <param name="content"></param>
    public void UpdateData(int cellIndex, int selectedTabIndex, string content)
    {
        if (selectedTabIndex == 0)
        {
            GameObject icon = transform.GetChild(0).gameObject;
            GameObject coin = transform.GetChild(1).gameObject;

            string[] datas = content.Split('|');
            if (datas.Length < 3) return;

            int type = int.Parse(datas[0]);
            int cnt = int.Parse(datas[1]);
            int consume = int.Parse(datas[2]);

            icon.GetComponent<Image>().sprite = ResourcesManager.Instance.ItemIcon(type);
            icon.GetComponentInChildren<TextMeshProUGUI>().text = "x" + cnt;
            coin.GetComponentInChildren<TextMeshProUGUI>().text = consume.ToString();
        }
        else
        {
            GameObject icon = transform.GetChild(0).gameObject;
            GameObject coin = transform.GetChild(1).gameObject;

            string[] datas = content.Split('|');
            if (datas.Length < 2) return;

            int cnt = int.Parse(datas[0]);
            int consume = int.Parse(datas[1]);

            icon.GetComponent<Image>().sprite = ResourcesManager.Instance.CoinImage;
            icon.GetComponentInChildren<TextMeshProUGUI>().text = cnt.ToString();
            coin.GetComponentInChildren<TextMeshProUGUI>().text = "$" + consume.ToString();
        }      
    }

    /// <summary>
    /// clean cell content
    /// </summary>
    public void ClearUp()
    {

    }
}
