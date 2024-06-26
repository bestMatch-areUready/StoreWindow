using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIKit;
using Newtonsoft.Json;
using System;
public class GiftTableView : UITableView, IUITableViewDataSource, IUITableViewDelegate, IUITableViewMargin
{

    List<int> orders = new List<int>();
    List<ChestData> dataList = new List<ChestData>();

    [SerializeField]
    private ChestCell ChestCellPrefab;
    ScreenOrientation _lastOrientation;

    protected override void Awake()
    {
        base.Awake();

        dataSource = this;
        marginDataSource = this;
        @delegate = this;

        TextAsset data = Resources.Load<TextAsset>("json/content");
        JsonWrap wrap = Newtonsoft.Json.JsonConvert.DeserializeObject<JsonWrap>(data.text);

        int i = 0;
        foreach(ChestJsonData d in wrap.wrap)
        {
            orders.Add(i++);
            ChestData tmp = new ChestData();
            tmp.scalar = 550;
            tmp.content = d;
            dataList.Add(tmp);
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        //将无效礼包移动到下方
        int i = 0;
        int cnt = orders.Count;
        bool refresh=false;
        while (i < cnt)
        {
            int id = orders[i];
            int packPurchasedCnt = PlayerPrefs.GetInt(ConstKey.ChestPurchaseTimes + id, 0);
            int packCount = dataList[id].content.PackCount;

            if (packPurchasedCnt >= packCount)//(ApplicationModel.PurchaseTag & (1 << id)) != 0)
            {
                orders.RemoveAt(i);
                orders.Add(id);
                cnt--;
                refresh = true;
            }
            else
            {
                i++;
            }
        }
        if (refresh) ReloadData();
    }

    protected override void Update()
    {
        base.Update();
        if (_lastOrientation != Screen.orientation)
        {
            _lastOrientation = Screen.orientation;
            ReloadData();
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        //重新定位到页面最上方
        ScrollToCellAt(0, 0.1f, withMargin: true);
    }

    #region IUITableViewDelegate
    public void CellAtIndexInTableViewDidDisappear(UITableView tableView, int index)
    {
        var imageCell = tableView.GetLoadedCell<ChestCell>(index);
        imageCell.ClearUp();
    }
    
    public void CellAtIndexInTableViewWillAppear(UITableView tableView, int index)
    {
        UITableViewCellLifeCycle lifeCycle;
        int id = orders[index];
        var data = dataList[id];
        var textCell = tableView.GetLoadedCell<ChestCell>(index);
        textCell.UpdateData(id, data.content);
        lifeCycle = textCell.lifeCycle;

    }
    #endregion
    
    #region IUITableViewDataSource
    public UITableViewCell CellAtIndexInTableView(UITableView tableView, int index)
    {
        return tableView.ReuseOrCreateCell(ChestCellPrefab);
    }
    
    
    public float LengthForCellInTableView(UITableView tableView, int index)
    {
        return dataList[index].scalar;
    }
    
    public int NumberOfCellsInTableView(UITableView tableView)
    {
        return dataList.Count;
    }
    #endregion
    
    #region IUITableViewMargin
    public float LengthForUpperMarginInTableView(UITableView tableView, int rowIndex)
    {
        if (tableView.direction.IsTopToBottomOrRightToLeft())
            return rowIndex == 0 ? 50f : 0f;
        var dataList = this.dataList;
        return rowIndex == dataList.Count - 1 ? 50f : 0f;
    }
    
    public float LengthForLowerMarginInTableView(UITableView tableView, int rowIndex)
    {
        if (tableView.direction.IsTopToBottomOrRightToLeft())
        {
            var dataList = this.dataList;
            return rowIndex == dataList.Count - 1 ? 50f : 0f;
        }
        return rowIndex == 0 ? 50f : 0f;
    }
    #endregion
}
