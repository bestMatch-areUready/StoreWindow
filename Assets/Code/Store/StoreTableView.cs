using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UIKit;
using UnityEngine;
using UnityEngine.EventSystems;

public class StoreTableView : UITableView, IUIGridViewDataSource, IUITableViewDelegate, IUITableViewClickable,IUITableViewMargin
{
    int _selectedTabIndex = 0;
    int _columnNumber;
    ScreenOrientation _lastOrientation;

    [SerializeField]
    private StoreItemCell StoreItemCellPrefab;

    List<List<StorePageData>> dataList = new List<List<StorePageData>>();


    protected override void Awake()
    {
        base.Awake();

        dataSource = this;
        @delegate = this;
        clickable = this;
        marginDataSource = this;

        _columnNumber = 3;

        var data = new List<StorePageData>();
        int cnt = UnityEngine.Random.Range(10, 20);
        data.AddRange(CreateCellData(cnt));
        dataList.Add(data);

        var coinPurchaseData = new List<StorePageData>();
        coinPurchaseData.AddRange(CreateCoinPurchaseCell(10));
        dataList.Add(coinPurchaseData);
    }

    protected override void OnDisable()
    {
        ScrollToCellAt(0, 0.1f, withMargin: true);
        base.OnDisable();
    }

    protected override void Update()
    {
        base.Update();
        if (_lastOrientation != Screen.orientation)
        {
            _lastOrientation = Screen.orientation;
            ReloadData();
        }
        //ReloadData();
    }

    IEnumerable<StorePageData> CreateCellData(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var data = new StorePageData();

            int type = UnityEngine.Random.Range(0, 5);
            int cnt = UnityEngine.Random.Range(1, 10);
            int consume = UnityEngine.Random.Range(100, 1000);
            data.content = type + "|" + cnt + "|" + consume;
            data.scalar = 270;
            yield return data;
        }
    }

    public void ChangeSelectTab(int index)
    {
        _selectedTabIndex = index;
        ReloadData();
    }

    IEnumerable<StorePageData> CreateCoinPurchaseCell(int count)
    {
        for (int i = 1; i < count; i++)
        {
            var data = new StorePageData();

            int coinCnt = i * 10;
            int consume = i * 10;
            data.content = coinCnt + "|" + consume;
            data.scalar = 270;
            yield return data;
        }
    }


    #region IUIGridViewDataSource
    public UITableViewAlignment AlignmentOfCellsAtLastRow(UITableView grid)
    {
        return UITableViewAlignment.LeftOrBottom;
    }

    public UITableViewCell CellAtIndexInTableView(UITableView tableView, int index)
    {
        return tableView.ReuseOrCreateCell(StoreItemCellPrefab);
    }

    public float LengthForCellInTableView(UITableView tableView, int index)
    {
        return dataList[_selectedTabIndex][index].scalar;
    }

    public int NumberOfCellsInTableView(UITableView tableView)
    {
        return dataList[_selectedTabIndex].Count;
    }

    public int NumberOfColumnPerRow(UITableView grid, int rowIndex)
    {
        //if (rowIndex % 2 == 0)
        //{
        //    return Mathf.Max(_columnNumber / 2, 1);
        //}
        return _columnNumber;
    }
    #endregion

    #region IUITableViewDelegate
    public void CellAtIndexInTableViewWillAppear(UITableView tableView, int index)
    {
        //if (_dataList[index] >= 0)
        //{
        tableView.GetLoadedCell<StoreItemCell>(index).UpdateData(index, _selectedTabIndex, dataList[_selectedTabIndex][index].content);
        //}
    }

    public void CellAtIndexInTableViewDidDisappear(UITableView tableView, int index)
    {
        //throw new NotImplementedException();
    }
    #endregion

    #region IUITableViewClickable

    public void TableViewOnPointerDownCellAt(UITableView tableView, int index, PointerEventData eventData)
    {
        //throw new NotImplementedException();
    }

    public void TableViewOnPointerClickCellAt(UITableView tableView, int index, PointerEventData eventData)
    {
        //throw new NotImplementedException();
    }

    public void TableViewOnPointerUpCellAt(UITableView tableView, int index, PointerEventData eventData)
    {
        //throw new NotImplementedException();
    }

    public Camera TableViewCameraForInteractive(UITableView tableView)
    {
        return null;
    }
    #endregion
    #region IUITableViewMargin
    public float LengthForUpperMarginInTableView(UITableView tableView, int rowIndex)
    {
        return 15; 
    }

    public float LengthForLowerMarginInTableView(UITableView tableView, int rowIndex)
    {
        return 15;
    }
    #endregion



    //#region IUITableViewDelegate
    //public void CellAtIndexInTableViewDidDisappear(UITableView tableView, int index)
    //{
    //    var data = dataList[_selectedTabIndex][index];
    //    var imageCell = tableView.GetLoadedCell<StoreItemCell>(index);
    //    imageCell.ClearUp();
    //
    //    Debug.Log($"cell at index:{index} will disappear.");
    //}
    //
    //public void CellAtIndexInTableViewWillAppear(UITableView tableView, int index)
    //{
    //    UITableViewCellLifeCycle lifeCycle;
    //    var data = dataList[_selectedTabIndex][index];
    //    var textCell = tableView.GetLoadedCell<StoreItemCell>(index);
    //    textCell.UpdateData(index, _selectedTabIndex, data.content);
    //    lifeCycle = textCell.lifeCycle;
    //
    //    //switch (data.sampleType)
    //    //{
    //    //    case SampleData.SampleType.Text:
    //    //        var textCell = tableView.GetLoadedCell<StoreItemCell>(index);
    //    //        textCell.UpdateData(index, _selectedTabIndex, data.text);
    //    //        lifeCycle = textCell.lifeCycle;
    //    //        break;
    //    //    case SampleData.SampleType.Image:
    //    //        var imageCell = tableView.GetLoadedCell<SampleImageCell>(index);
    //    //        imageCell.UpdateData(index, _selectedTabIndex, data);
    //    //        lifeCycle = imageCell.lifeCycle;
    //    //        break;
    //    //    case SampleData.SampleType.Tab:
    //    //        var tabCell = tableView.GetLoadedCell<SampleTabCell>(index);
    //    //        tabCell.UpdateData(_selectedTabIndex, OnTabClicked);
    //    //        lifeCycle = tabCell.lifeCycle;
    //    //        break;
    //    //    case SampleData.SampleType.Chat:
    //    //        var chatCell = tableView.GetLoadedCell<SampleChatCell>(index);
    //    //        chatCell.UpdateData(index, data.text);
    //    //        lifeCycle = chatCell.lifeCycle;
    //    //        break;
    //    //    default:
    //    //        throw new ArgumentOutOfRangeException();
    //    //}
    //
    //    Debug.Log($"Cell at index:{index} is appeared. UITableViewLifeCycle is <color=green>{lifeCycle}</color>");
    //}
    //#endregion
    //
    //#region IUITableViewDataSource
    //public UITableViewCell CellAtIndexInTableView(UITableView tableView, int index)
    //{
    //    var data = dataList[_selectedTabIndex][index];
    //    return tableView.ReuseOrCreateCell(StoreItemCellPrefab);
    //
    //    //switch (data.sampleType)
    //    //{
    //    //    case SampleData.SampleType.Text:
    //    //        return tableView.ReuseOrCreateCell(_textCellPrefab);
    //    //    case SampleData.SampleType.Image:
    //    //        return tableView.ReuseOrCreateCell(_imageCellPrefab);
    //    //    case SampleData.SampleType.Tab:
    //    //        return tableView.ReuseOrCreateCell(_tabCellPrefab, UITableViewCellLifeCycle.RecycleWhenReloaded);
    //    //    case SampleData.SampleType.Chat:
    //    //        return tableView.ReuseOrCreateCell(_chatCellPrefab);
    //    //    default:
    //    //        throw new ArgumentOutOfRangeException();
    //    //}
    //}
    //
    //
    //public float LengthForCellInTableView(UITableView tableView, int index)
    //{
    //    return dataList[_selectedTabIndex][index].scalar;
    //}
    //
    //public int NumberOfCellsInTableView(UITableView tableView)
    //{
    //    return dataList[_selectedTabIndex].Count;
    //}
    //#endregion
    //
    //#region IUITableViewMargin
    //public float LengthForUpperMarginInTableView(UITableView tableView, int rowIndex)
    //{
    //    if (tableView.direction.IsTopToBottomOrRightToLeft())
    //        return rowIndex == 0 ? 100f : 0f;
    //    var dataList = this.dataList[_selectedTabIndex];
    //    return rowIndex == dataList.Count - 1 ? 100f : 0f;
    //}
    //
    //public float LengthForLowerMarginInTableView(UITableView tableView, int rowIndex)
    //{
    //    if (tableView.direction.IsTopToBottomOrRightToLeft())
    //    {
    //        var dataList = this.dataList[_selectedTabIndex];
    //        return rowIndex == dataList.Count - 1 ? 100f : 0f;
    //    }
    //    return rowIndex == 0 ? 100f : 0f;
    //}
    //#endregion
}
