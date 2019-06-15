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
        public Transform holdingValue;
        public Transform opened;
        public Transform openedValue;
        public enum Status{
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
            this.currentStatus = status;
            holding.transform.localPosition = new Vector2(0, 0);
            switch(status) {
                case Status.Holding:
                    closed.gameObject.SetActive(false);
                    holding.gameObject.SetActive(true);
                    opened.gameObject.SetActive(false);
                    break;
                case Status.HoldingToThrow:
                    closed.gameObject.SetActive(false);
                    holding.gameObject.SetActive(true);
                    if(isMine)
                        holding.transform.localPosition = new Vector2(0, 50);
                    opened.gameObject.SetActive(false);
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
            holdingValue.GetComponent<Image>().sprite = Resources.Load<Sprite>(mahjongValue + "");
            openedValue.GetComponent<Image>().sprite = Resources.Load<Sprite>(mahjongValue + "");
        }

        public int GetMahjongValue()
        {
            return mahjongValue;
        }

        public void OnPointUp()
        {
            if (Status.Selecting == this.currentStatus || Status.Closed == this.currentStatus)
            {
                MyTilesScript myMahjong = GameObject.Find("MyTiles").GetComponent<MyTilesScript>() as MyTilesScript;
                if (myMahjong.ReceiveMahjong(mahjongValue))
                {
                    GameObject.Destroy(gameObject);
                    return;
                }
                CurrentMahjong currentMahjong = GameObject.Find("CurrentMahjong").GetComponent<CurrentMahjong>() as CurrentMahjong;
                if (currentMahjong.ReceiveMahjong(mahjongValue))
                {
                    GameObject.Destroy(gameObject);
                    return;
                }
            }
            else if (Status.Holding == this.currentStatus)
            {
                Mahjong currentMahjong = GameObject.Find("CurrentMahjong").GetComponent<Mahjong>() as Mahjong;
                if(currentMahjong.GetStatus() == Status.Holding || currentMahjong.GetStatus() == Status.HoldingToThrow){
                    SetStatus(Status.HoldingToThrow);
                }
            }
            else if (Status.HoldingToThrow == this.currentStatus)
            {
                Mahjong currentMahjong = GameObject.Find("CurrentMahjong").GetComponent<Mahjong>() as Mahjong;
                if (!this.isMine)
                {
                    currentMahjong.SetStatus(Status.Gone);
                    return;
                }
                int newValue = currentMahjong.GetMahjongValue();
                currentMahjong.SetStatus(Status.Gone);
                MyTilesScript myMahjongs = GameObject.Find("MyTiles").GetComponent<MyTilesScript>() as MyTilesScript;
                myMahjongs.ReplaceMahjong(this, newValue);
            }
        }

        private Status GetStatus()
        {
            return currentStatus;
        }
    }
}