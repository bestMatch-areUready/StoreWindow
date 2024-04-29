using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public static PopupManager instance;
    [SerializeField]
    private ConfirmPopup confirmPopup;
    [SerializeField]
    private GameObject ShopWindow;

    public ConfirmPopup ConfirmPopup { get { return confirmPopup; } }

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }

    public void ShowShop()
    {
        ShopWindow.SetActive(true);
    }
}
