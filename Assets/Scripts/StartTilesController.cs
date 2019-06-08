using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartTilesController : MonoBehaviour
{
    public GameObject mahjongPrefab;
    //GridLayoutGroup gridLayout;
    // Start is called before the first frame update
    void Start()
    {
        //gridLayout = GetComponent<GridLayoutGroup>();
        int countOneRow = (Screen.width - 40) / 75;
        InitAllTiles(countOneRow);
    }

    private void InitAllTiles(int countOneRow)
    {
        List<Transform> allTiles = new List<Transform>();
        List<int> indexList = new List<int>();
        List<int> fullIndex = new List<int>();

        int next = countOneRow*4;
        while(next > 0)
        {
            int nextValue = UnityEngine.Random.Range(11, 39);
            if(nextValue % 10 == 0 || fullIndex.Contains(nextValue))
            {
                continue;
            }
            int existCount = indexList.FindAll(n => n == nextValue).Count;
            if (existCount >= 4)
            {
                fullIndex.Add(nextValue);
                continue;
            }
            else
            {
                indexList.Add(nextValue);
                existCount++;
            }

            if (existCount == 4)
            {
                fullIndex.Add(nextValue);
            }
            next--;
        }
        for (int i=0;i< indexList.Count; i++)
        {
            GameObject newCell = Instantiate<GameObject>(mahjongPrefab) as GameObject;
            MahjongController mahjong = newCell.GetComponent<MahjongController>() as MahjongController;
            mahjong.SetValue(indexList[i]);
            newCell.transform.SetParent(this.gameObject.transform, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
