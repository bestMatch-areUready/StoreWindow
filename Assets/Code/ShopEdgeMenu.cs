using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopEdgeMenu : MonoBehaviour
{
    int curIndex = 0;
    public int CurrentIndex
    {
        get
        {
            return curIndex;
        }
    }

    [SerializeField]
    private List<GameObject> btns = new List<GameObject>();

    public delegate void OnBtnClicked();
    public OnBtnClicked onBtnClicked;

    private void Awake()
    {
        for (int i = 0; i < btns.Count; i++)
        {
            int id = i;
            btns[i].GetComponent<Button>().onClick.AddListener(() =>
            {
                OnButtonClick(id);
            });
        }
    }

    public void OnButtonClick(int index)
    {
        if (curIndex == index) return;

        btns[curIndex].GetComponent<Image>().enabled = false;
        btns[index].GetComponent<Image>().enabled = true;

        curIndex=index;
        onBtnClicked?.Invoke();
    }
}
