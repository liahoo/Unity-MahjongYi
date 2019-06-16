using System;
using System.Collections;
using System.Collections.Generic;
using MahjongGame;
using MahjongGame.Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace MahjongGame.Controllers
{
    public class MyTilesScript : MonoBehaviour
    {
        public GameObject mahjongPrefab;
        private bool isInited = false;
        private List<int> valueList = new List<int>();
        private int MaxCount = 13;
        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("[MyTilesScript] Screen.width : " + Screen.width + " Screen.orientation : " + Screen.orientation);
            bool isLandscape = Screen.width > Screen.height;
            int countOneRow = isLandscape ? 1 : 2;
            int countOneCol = isLandscape ? 13 : 7;
            RectTransform rectTransofrm = GetComponent<RectTransform>() as RectTransform;
            // rectTransofrm.sizeDelta = new Vector2(width, rectTransofrm.rect.height);
            rectTransofrm.Clear();
            isInited = false;
            // 判断屏幕方向，横屏显示一排，竖屏显示两排
            // if (Screen.orientation == ScreenOrientation.Landscape)
            // {
            //     countOneRow = Screen.orientation == ScreenOrientation.Landscape ? 1 : 2;
            //     countOneCol = Screen.orientation == ScreenOrientation.Landscape ? 7 : 13;
            // }
            InitAllTiles(countOneRow, countOneCol);
        }

        internal void Grab(int mahjongValue)
        {

        }

        private void InitAllTiles(int row, int col)
        {
            Debug.Log("[MyTilesScript] [InitAllTiles] row:" + row + " col:" + col);
            List<Transform> allTiles = new List<Transform>();
            List<int> indexList = MJGenerator.GenerateForFirstChoose(13);
            // GridLayoutGroup gridLayoutGroup = GetComponent<GridLayoutGroup>();
            // gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedRowCount;
            // gridLayoutGroup.constraintCount = row;
            indexList.Sort();
            for (int i = 0; i < indexList.Count; i++)
            {
                valueList.Add(indexList[i]);
                GameObject newCell = Instantiate<GameObject>(mahjongPrefab) as GameObject;
                Mahjong mjToAdd = newCell.GetComponent<Mahjong>() as Mahjong;
                mjToAdd.name = indexList[i].ToString();
                mjToAdd.SetMahjongValue(indexList[i]);
                mjToAdd.SetStatus(Mahjong.Status.Holding);
                mjToAdd.isMine = true;
                newCell.transform.SetParent(this.gameObject.transform, false);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void AddMahjong(Mahjong mjToAdd, bool sort = true)
        {
            valueList.Add(mjToAdd.GetMahjongValue());
            mjToAdd.SetStatus(Mahjong.Status.Holding);
            mjToAdd.isMine = true;
            mjToAdd.transform.SetParent(this.gameObject.transform, false);
            if(sort){
                valueList.Sort();
                mjToAdd.transform.SetSiblingIndex(valueList.IndexOf(mjToAdd.GetMahjongValue()));
            }
        }

        public void ReplaceMahjong(Mahjong mjToAdd) {
            mjToAdd.SetStatus(Mahjong.Status.Holding);
            mjToAdd.isMine = true;
            mjToAdd.transform.SetParent(this.gameObject.transform, false);
            valueList.Add(mjToAdd.GetMahjongValue());
            valueList.Sort();
            mjToAdd.transform.SetSiblingIndex(valueList.IndexOf(mjToAdd.GetMahjongValue()));
        }
        public void DeleteMahjong(Mahjong from)
        {
            valueList.Remove(from.GetMahjongValue());
            GameObject.Destroy(from);
        }
        public void ReplaceMahjong(Mahjong from, Mahjong to){
            DeleteMahjong(from);
            ReplaceMahjong(to);
        }

        public void updateMahjong(Mahjong from, int new_value){
            valueList.Remove(from.GetMahjongValue());
            valueList.Add(new_value);
            valueList.Sort();
            from.SetStatus(Mahjong.Status.Holding);
            from.SetMahjongValue(new_value);
            from.transform.SetSiblingIndex(valueList.IndexOf(new_value));

        }
        public bool ReceiveMahjong(Mahjong mj)
        {
            if (transform.childCount < MaxCount)
            {
                AddMahjong(mj);
                return true;
            }
            Debug.LogWarning("[ReceiveMahjong] Failed: Already has childCount = " + transform.childCount);
            return false;
        }
    }
}