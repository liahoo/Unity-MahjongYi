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
            HoldingToThrow,
            HoldingToGang,
            IsForGang
        }
        public bool isMine = false;
        private Status currentStatus;
        public int mahjongValue = 0;
        private void Start()
        {
            //SetStatus(Status.Closed);
        }

        public Status GetStatus(){
            return currentStatus;
        }
        public void SetStatus(Status status)
        {
            // Debug.Log(name + " [SetStatus] "+ this.currentStatus + " -> " + status);
            this.currentStatus = status;
            holding.transform.localPosition = new Vector2(0,0);
            switch (status)
            {
                case Status.Holding:
                    closed.gameObject.SetActive(false);
                    holding.gameObject.SetActive(true);
                    opened.gameObject.SetActive(false);
                    break;
                case Status.HoldingToGang:
                case Status.HoldingToThrow:
                    closed.gameObject.SetActive(false);
                    holding.gameObject.SetActive(true);
                    opened.gameObject.SetActive(false);
                    holding.transform.localPosition = new Vector2(0,40);
                    break;
                case Status.Selecting:
                case Status.Opened:
                case Status.IsForGang:
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
            Sprite[] allImagesHolding = Resources.LoadAll<Sprite>("h");
            Image holdingImage = holding.GetComponent<Image>();
            holdingImage.sprite = allImagesHolding[MJHelper.ConvertValueToHoldingImageIndex(v)];
            holdingImage.SetNativeSize();

            Sprite[] allImagesOpened = Resources.LoadAll<Sprite>("o");
            Image openImage = opened.GetComponent<Image>();
            openImage.sprite = allImagesOpened[MJHelper.ConvertValueToHoldingImageIndex(v)];
            openImage.SetNativeSize();
        }

        public int GetMahjongValue()
        {
            return mahjongValue;
        }

        public void OnPointUp()
        {
            MyTilesScript myMahjongs = GameObject.Find("MyTiles").GetComponent<MyTilesScript>() as MyTilesScript;
            switch (currentStatus)
            {
                case Status.Selecting:
                case Status.Closed:
                    myMahjongs.ResetStatus();
                    if (myMahjongs.CreateMahjongByValue(mahjongValue))
                    {
                        SetStatus(Status.Gone);
                        return; //如果被手里的牌接收了，则处理结束
                    }
                    NewFetchScript currentMahjong = GameObject.Find("NewFetched").GetComponent<NewFetchScript>() as NewFetchScript;
                    if (currentMahjong.ReceiveMahjong(this.GetMahjongValue()))
                    {
                        // GameObject.Destroy(this); // 已经被传给当前牌了，因为当前牌是复用，所以可以直接销毁被摸的牌
                        // transform.SetParent(null);
                        SetStatus(Status.Gone);
                        return; // 摸牌的情况
                    }

                    break;
                case Status.Holding:
                    SetStatus(Status.HoldingToThrow);
                    break;
                case Status.HoldingToThrow:
                    // 如果不是手里的牌，直接销毁
                    if (!this.isMine)
                    {
                        SetStatus(Status.Gone);
                    }
                    else
                    {
                        // 如果是手里的牌，则需要把新抓的牌替代进去
                        GameObject currentMahjong2 = GameObject.Find("CurrentMahjong");
                        if (currentMahjong2 != null)
                        {
                            Mahjong to = currentMahjong2.GetComponent<Mahjong>() as Mahjong;
                            if(to.GetStatus()!=Status.Gone){
                                myMahjongs.updateMahjong(this, to.GetMahjongValue());
                                to.SetStatus(Status.Gone);
                            }
                        } else {
                            // (未知错误的时候，通常不会发生)如果是手里的牌，但又没有摸牌，则恢复。
                            SetStatus(Status.Holding);
                        }
                    }
                    break;
                case Status.HoldingToGang:
                    myMahjongs.OnMahjongSelectedToGang(mahjongValue);
                    break;

            }
        }
    }
}