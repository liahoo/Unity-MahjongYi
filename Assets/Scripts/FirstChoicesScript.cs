using System;
using System.Collections;
using System.Collections.Generic;
using MahjongGame;
using UnityEngine;
using UnityEngine.UI;
namespace MahjongGame.Controllers
{
    public class FirstChoicesScript : MonoBehaviour
    {
        public int cellSpacing = -20;
        public int cellWidth = 75;
        public int cellHeight = 111;
        public int paddingTop = 20;
        public int paddingBottom = 191;
        public GameObject mahjongPrefab;
        //GridLayoutGroup gridLayout;
        // Start is called before the first frame update
        void Start()
        {
            //gridLayout = GetComponent<GridLayoutGroup>();
            Debug.Log("Screen.width : " + Screen.width);
            Debug.Log("Screen.height : " + Screen.height);
            int col = (Screen.width - 80) / 75;
            int row = (Screen.height - paddingBottom - paddingTop - cellSpacing) / (cellHeight + cellSpacing);
            if(row * col > 80)
            {
                row = 80 / col;
            }
            int width = col * 75;
            int height = row * cellHeight + (row - 1) * cellSpacing;
            RectTransform rectTransofrm = GetComponent<RectTransform>() as RectTransform;
            rectTransofrm.sizeDelta = new Vector2(width, height);
            rectTransofrm.localPosition = new Vector3(0, 0, 0);
            InitAllTiles(row, col);
        }

        private void InitAllTiles(int row, int col)
        {
            Debug.Log("[InitAllTiles] row:" + row + " col:"+col);
            List<Transform> allTiles = new List<Transform>();
            List<int> indexList = MJGenerator.GenerateForFirstChoose(row * col);
            GridLayoutGroup gridLayoutGroup = GetComponent<GridLayoutGroup>();
            gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedRowCount;
            gridLayoutGroup.constraintCount = row;

            for (int i = 0; i < indexList.Count; i++)
            {
                GameObject newCell = Instantiate<GameObject>(mahjongPrefab) as GameObject;
                MjFreeController mahjong = newCell.GetComponent<MjFreeController>() as MjFreeController;
                mahjong.SetValue(indexList[i]);
                newCell.transform.SetParent(this.gameObject.transform, false);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}