using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PressButton : Button
{
    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        GetComponent<RectTransform>().DOScale(0.95f, 0.25f);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        GetComponent<RectTransform>().DOScale(1, 0.25f);
    }
}
