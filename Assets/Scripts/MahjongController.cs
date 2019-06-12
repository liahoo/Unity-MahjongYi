using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace MahjongGame.Controllers
{
    public class MahjongController : MonoBehaviour
    {
        public Transform back;
        public Transform front;
        public Transform value;
        private void Start()
        {
            ShowFront(false);
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
}