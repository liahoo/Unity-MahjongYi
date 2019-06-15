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
        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("[MyTilesScript] Screen.width : " + Screen.width + " Screen.orientation : " + Screen.orientation);
            bool isLandscape = Screen.width > Screen.height;
            int countOneRow = isLandscape ? 1 : 2;
            int countOneCol = isLandscape ? 13 : 7;
            int width = 80 * countOneCol;
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
            // InitAllTiles(countOneRow, countOneCol);
        }

        internal void Grab(int mahjongValue)
        {

        }

        private void InitAllTiles(int row, int col)
        {
            Debug.Log("[MyTilesScript] [InitAllTiles] row:" + row + " col:" + col);
            List<Transform> allTiles = new List<Transform>();
            List<int> indexList = MJGenerator.GenerateForFirstChoose(13);
            GridLayoutGroup gridLayoutGroup = GetComponent<GridLayoutGroup>();
            gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedRowCount;
            gridLayoutGroup.constraintCount = row;
            indexList.Sort();
            for (int i = 0; i < indexList.Count; i++)
            {
                Debug.Log(i + "/" + indexList.Count);
                Debug.Log("next==> " + indexList[i]);
                GameObject newCell = Instantiate<GameObject>(mahjongPrefab) as GameObject;
                Mahjong mahjong = newCell.GetComponent<Mahjong>() as Mahjong;
                mahjong.SetMahjongValue(indexList[i]);
                newCell.transform.SetParent(this.gameObject.transform, false);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void AddMahjong(int mahjongValue)
        {
            valueList.Add(mahjongValue);
            valueList.Sort();
            GameObject newCell = Instantiate<GameObject>(mahjongPrefab) as GameObject;
            Mahjong mahjong = newCell.GetComponent<Mahjong>() as Mahjong;
            mahjong.SetMahjongValue(mahjongValue);
            mahjong.SetStatus(Mahjong.Status.Holding);
            mahjong.isMine = true;
            newCell.transform.SetParent(this.gameObject.transform, false);
            newCell.transform.SetSiblingIndex(valueList.IndexOf(mahjongValue));
        }

        public void ReplaceMahjong(Mahjong toReplace, int mahjongValue){
            valueList.Remove(toReplace.GetMahjongValue());
            valueList.Add(mahjongValue);
            valueList.Sort();
            toReplace.SetStatus(Mahjong.Status.Holding);
            toReplace.SetMahjongValue(mahjongValue);
            toReplace.transform.SetSiblingIndex(valueList.IndexOf(mahjongValue));
        }
        public bool ReceiveMahjong(int mahjongValue)
        {
            if (transform.childCount < 13)
            {
                AddMahjong(mahjongValue);
                return true;
            }
            Debug.LogWarning("[ReceiveMahjong] Failed: Already has childCount = " + transform.childCount);
            return false;
        }
    }
}