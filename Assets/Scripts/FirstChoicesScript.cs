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
        public GameObject mahjongPrefab;
        //GridLayoutGroup gridLayout;
        // Start is called before the first frame update
        void Start()
        {
            //gridLayout = GetComponent<GridLayoutGroup>();
            Debug.Log("Screen.width : " + Screen.width);
            Debug.Log("Screen.height : " + Screen.height);
            bool isLandscape = Screen.width > Screen.height;
            int col = isLandscape ? 15 : 9;
            int row = isLandscape ? 4 : 9;
            RectTransform rectTransofrm = GetComponent<RectTransform>() as RectTransform;
            rectTransofrm.Clear();
            InitAllTiles(row, col);
        }

        private void InitAllTiles(int row, int col)
        {
            Debug.Log("[InitAllTiles] row:" + row + " col:"+col);
            
            List<Transform> allTiles = new List<Transform>();
            List<int> indexList = MJHelper.GenerateForFirstChoose(row * col);
            GridLayoutGroup gridLayoutGroup = GetComponent<GridLayoutGroup>();
            gridLayoutGroup.constraint = GridLayoutGroup.Constraint.Flexible;
            // gridLayoutGroup.constraintCount = row;

            for (int i = 0; i < indexList.Count; i++)
            {
                GameObject newCell = Instantiate<GameObject>(mahjongPrefab) as GameObject;
                Mahjong mahjong = newCell.GetComponent<Mahjong>() as Mahjong;
                mahjong.SetMahjongValue(indexList[i]);
                mahjong.SetStatus(Mahjong.Status.Selecting);
                // mahjong.SetStatus(Mahjong.Status.Closed);
                newCell.transform.SetParent(this.gameObject.transform, false);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}