using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopEdgeHidedCallback : MonoBehaviour, IEdgeHidedCallback
{
    [SerializeField]
    private GameObject ArrowButton;


    public void OnEdgeHided()
    {
        ArrowButton.GetComponent<Image>().transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void OnEdgeShowed()
    {
        ArrowButton.GetComponent<Image>().transform.rotation = Quaternion.Euler(0, 0, 180);
    }
}
