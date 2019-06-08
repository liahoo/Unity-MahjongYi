using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MahjongController : MonoBehaviour
{
    public Transform back;
    public Transform front;
    public Transform value;
    private void Start()
    {
        ShowFront(false);
    }

    public void onPointUp()
    {
        ShowFront(back.gameObject.activeSelf);//如果显示的是背面，则显示正面，反之亦然
    }
    public void ShowFront(bool isToShow)
    {
        front.gameObject.SetActive(isToShow);
        back.gameObject.SetActive(!isToShow);
    }
    internal void SetValue(int v)
    {
        value.GetComponent<Image>().sprite = Resources.Load<Sprite>(v + "");
    }
}
