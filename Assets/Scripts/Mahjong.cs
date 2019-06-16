using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace MahjongGame.Controllers
{
    public class Mahjong : MonoBehaviour
    {
        public Transform closed;
        public Transform holding;
        public Transform opened;
        public enum Status
        {
            Gone,
            Selecting,
            Opened,
            Closed,
            Holding,
            HoldingToThrow
        }
        public bool isMine = false;
        private Status currentStatus;
        private int mahjongValue = 0;
        private void Start()
        {
            //SetStatus(Status.Closed);
        }

        public void SetStatus(Status status)
        {
            Debug.Log(name + " [SetStatus] "+ this.currentStatus + " -> " + status);
            this.currentStatus = status;
            holding.transform.localPosition = new Vector2(0,0);
            switch (status)
            {
                case Status.Holding:
                    closed.gameObject.SetActive(false);
                    holding.gameObject.SetActive(true);
                    opened.gameObject.SetActive(false);
                    break;
                case Status.HoldingToThrow:
                    closed.gameObject.SetActive(false);
                    holding.gameObject.SetActive(true);
                    opened.gameObject.SetActive(false);
                    if(isMine) {
                        Debug.Log("Up the holding");
                        holding.transform.localPosition = new Vector2(0,40);
                    }
                    break;
                case Status.Selecting:
                case Status.Opened:
                    closed.gameObject.SetActive(false);
                    holding.gameObject.SetActive(false);
                    opened.gameObject.SetActive(true);
                    break;
                case Status.Gone:
                    closed.gameObject.SetActive(false);
                    holding.gameObject.SetActive(false);
                    opened.gameObject.SetActive(false);
                    break;
                default:
                case Status.Closed:
                    closed.gameObject.SetActive(true);
                    holding.gameObject.SetActive(false);
                    opened.gameObject.SetActive(false);
                    break;

            }
        }
        public void SetMahjongValue(int v)
        {
            mahjongValue = v;
            Sprite[] allImages = Resources.LoadAll<Sprite>("mahjong_col9_row6");
            Image holdingImage = holding.GetComponent<Image>();
            holdingImage.sprite = Array.Find(allImages, (sprite) => sprite.name.Equals(v.ToString()));
            holdingImage.SetNativeSize();

            Image openImage = opened.GetComponent<Image>();
            openImage.sprite = Array.Find(allImages, (sprite) => sprite.name.Equals((v+30).ToString()));
            openImage.SetNativeSize();
        }

        public int GetMahjongValue()
        {
            return mahjongValue;
        }

        public void OnPointUp()
        {
            switch (currentStatus)
            {
                case Status.Selecting:
                case Status.Closed:
                    MyTilesScript myMahjong = GameObject.Find("MyTiles").GetComponent<MyTilesScript>() as MyTilesScript;
                    if (myMahjong.ReceiveMahjong(this))
                    {
                        return; //如果被手里的牌接收了，则处理结束
                    }
                    NewFetchScript currentMahjong = GameObject.Find("NewFetched").GetComponent<NewFetchScript>() as NewFetchScript;
                    if (currentMahjong.ReceiveMahjong(this.GetMahjongValue()))
                    {
                        GameObject.Destroy(this); // 已经被传给当前牌了，因为当前牌是复用，所以可以直接销毁被摸的牌
                        return; // 摸牌的情况
                    }

                    break;
                case Status.Holding:
                    SetStatus(Status.HoldingToThrow);
                    break;
                case Status.HoldingToThrow:
                    // 如果是手里的牌，则需要把新抓的牌替代进去
                    GameObject currentMahjong2 = GameObject.Find("CurrentMahjong");
                    if (currentMahjong2 != null)
                    {
                        Mahjong to = currentMahjong2.GetComponent<Mahjong>() as Mahjong;
                        MyTilesScript myMahjongs = GameObject.Find("MyTiles").GetComponent<MyTilesScript>() as MyTilesScript;
                        myMahjongs.updateMahjong(this, to.GetMahjongValue());
                        to.SetStatus(Status.Gone);
                    }
                    else
                    {
                        // 如果不是手里的牌，直接销毁
                        if (!this.isMine)
                        {
                            GameObject.Destroy(this);
                        }
                        else
                        {
                            // (未知错误的时候，通常不会发生)如果是手里的牌，但又没有摸牌，则恢复。
                            SetStatus(Status.Holding);
                        }
                    }
                    break;

            }
        }

        private Status GetStatus()
        {
            return currentStatus;
        }
    }
}