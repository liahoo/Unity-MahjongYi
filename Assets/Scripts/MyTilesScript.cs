using System.Collections;
using System.Collections.Generic;
using MahjongGame;
using MahjongGame.Controllers;
using UnityEngine;
using UnityEngine.UI;

public class MyTilesScript : MonoBehaviour
{
    public GameObject mahjongPrefab;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Screen.width : " + Screen.width);
        int width = Screen.width / 15 * 13;
        int countOneRow = 1;
        int countOneCol = width / (75+8);
        if (countOneCol < 13)
        {
            countOneRow = 2;
        }
        InitAllTiles(countOneRow, countOneCol);
        RectTransform rectTransofrm = GetComponent<RectTransform>() as RectTransform;
        rectTransofrm.sizeDelta = new Vector2(Screen.width / 15 * 13, rectTransofrm.rect.height);
    }
    private void InitAllTiles(int row, int col)
    {
        Debug.Log("[InitAllTiles] row:" + row + " col:" + col);
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
            MJMineController mahjong = newCell.GetComponent<MJMineController>() as MJMineController;
            mahjong.SetValue(indexList[i]);
            newCell.transform.SetParent(this.gameObject.transform, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
