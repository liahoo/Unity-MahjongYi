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
        public Transform gangArea;
        public GameObject gangButton;
        public GameObject huButton;
        public GameObject mahjongCurrent;
        // public Transform throwTo;
        private bool isInited = false;
        private List<int> valueList = new List<int>();
        private List<int> gangList = new List<int>();
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
            // InitAllTiles(countOneRow, countOneCol);
        }

        private void InitAllTiles(int row, int col)
        {
            Debug.Log("[MyTilesScript] [InitAllTiles] row:" + row + " col:" + col);
            List<Transform> allTiles = new List<Transform>();
            List<int> indexList = MJHelper.GenerateForFirstChoose(13);

            // GridLayoutGroup gridLayoutGroup = GetComponent<GridLayoutGroup>();
            // gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedRowCount;
            // gridLayoutGroup.constraintCount = row;
            indexList.Sort();
            for (int i = 0; i < indexList.Count; i++)
            {
                if(CreateMahjongByValue(indexList[i])==false)
                    break;
            }
        }

        // Update is called once per frame
        public bool CreateMahjongByValue(int value)
        {
            if (transform.childCount >= MaxCount) {
                Debug.LogWarning("[AddMahjong] Can not add any more Mahjong. We already have " + transform.childCount);
                return false;
            }
            GameObject newCell = Instantiate<GameObject>(mahjongPrefab) as GameObject;
            Mahjong mjToAdd = newCell.GetComponent<Mahjong>() as Mahjong;
            mjToAdd.SetMahjongValue(value);
            AddMahjong(mjToAdd);
            if(checkGang() == true) {
                return true;
            }
            return true;
        }

        private void handleHupai()
        {
            throw new NotImplementedException();
        }

        private bool checkHupai()
        {
            return false;
        }

        private bool checkGang()
        {
            Debug.Log("[checkGang] valueList.Count:"+valueList.Count);
            int foundGangIndexStart = -1;
            valueList.Sort();
            int i=0;
            while(i<valueList.Count) {
                int currentValue = valueList[i];
                int currentCount = 0;
                for(int j=i;j<valueList.Count;j++){
                    if(currentValue==valueList[j]){
                        currentCount++;
                    } else {
                        break;
                    }
                }
                if(currentCount >= 4){
                    foundGangIndexStart = i;
                    Debug.LogWarning("Has found gang at " + foundGangIndexStart);
                    break;
                } else {
                    i+=currentCount;
                }
            }
            
            if(foundGangIndexStart >= 0) {
                gangButton.GetComponent<GangScript>().OnGangEnabled(true);
                return true;
            } else {
                gangButton.GetComponent<GangScript>().OnGangEnabled(false);
                return false;
            }
        }

        public void handleGang(int startIndex){
            Debug.Log("[handleGang] startIndex:" + startIndex);
            Mahjong[] mj = new Mahjong[4];
            for (int i = 0; i < 4; i++)
            {
                gangList.Add(valueList[startIndex+i]);
                mj[i] = transform.GetChild(i+startIndex).gameObject.GetComponent<Mahjong>();
            }
            Array.ForEach(mj, (m) => { 
                m.SetStatus(Mahjong.Status.IsForGang);
                m.transform.localScale = new Vector3(0.75f, 0.75f, 1f);
                m.transform.SetParent(gangArea);
            });
            MaxCount-=3;
            valueList.RemoveRange(startIndex, 4);
            AddCurrentMahjongToMine();
            ResetStatus();
        }

        private void AddCurrentMahjongToMine()
        {
            if(!mahjongCurrent) mahjongCurrent=GameObject.Find("CurrentMahjong");
            Mahjong currentMahjong = mahjongCurrent.GetComponent<Mahjong>();
            CreateMahjongByValue((currentMahjong.GetMahjongValue()));
            currentMahjong.SetStatus(Mahjong.Status.Gone);
        }

        public void ResetStatus()
        {
            Debug.Log("[ResetStatus]");
            Mahjong mj = null;
            for (int i = 0; i < transform.childCount; i++)
            {
                mj = transform.GetChild(i).gameObject.GetComponent<Mahjong>();
                if (mj.GetStatus() == Mahjong.Status.HoldingToThrow || mj.GetStatus() == Mahjong.Status.HoldingToGang)
                {
                    mj.SetStatus(Mahjong.Status.Holding);
                }
            }
        }
        /*
            当用户按下杠时执行动作，由GangScript使用SendMessage("OnGangClicked")调用 
         */
        public void OnGangClicked(){
            Debug.Log("[OnGangClicked]");
            List<int> startIndex = new List<int>();
            int i=0;
            while(i<valueList.Count) {
                int currentValue = valueList[i];
                int currentCount = 0;
                Debug.Log("Now checking at " + i +" Value="+currentValue);
                for(int j=i;j<valueList.Count;j++){
                    if(currentValue==valueList[j]){
                        currentCount++;
                    } else {
                        break;
                    }
                }
                if(currentCount >= 4){
                    startIndex.Add(i);
                    Debug.Log("Has found gang at " + i);
                }
                i+=currentCount;
            }
            if(startIndex.Count==0){
                Debug.LogWarning("Wrong Message! Nothing to Gang!");
            } else if(startIndex.Count==1){
                handleGang(startIndex[0]);
            } else if(startIndex.Count>1){
                Debug.Log("Pending to choose which to Gang. count=" + startIndex.Count);
                Mahjong mj = null;
                startIndex.ForEach( (start) => {
                    Debug.Log("Pending to choose which to Gang." + start);
                    for(int k=start; k< start+4;k++){
                        mj = transform.GetChild(k).gameObject.GetComponent<Mahjong>();
                        mj.SetStatus(Mahjong.Status.HoldingToGang);
                    }
                });
            }
        }

        public void OnMahjongSelectedToGang(int value){
            handleGang(valueList.IndexOf(value));
        }
        public void AddMahjong(Mahjong mjToAdd)
        {
            ResetStatus();
            valueList.Add(mjToAdd.GetMahjongValue());
            mjToAdd.SetStatus(Mahjong.Status.Holding);
            mjToAdd.name = mjToAdd.GetMahjongValue().ToString();
            mjToAdd.isMine = true;
            mjToAdd.transform.SetParent(this.gameObject.transform, false);
            valueList.Sort();
            mjToAdd.transform.SetSiblingIndex(valueList.IndexOf(mjToAdd.GetMahjongValue()));
        }

        public void updateMahjong(Mahjong from, int toValue){
            ResetStatus();
            int fromValue = from.GetMahjongValue();
            Debug.Log("[updateMahjong] " + fromValue + " > " + toValue);
            valueList.Remove(fromValue);
            valueList.Add(toValue);
            string before = "";
            valueList.ForEach( (v) => { before += v + ",";});
            Debug.Log("BeforeSort " + before);
            valueList.Sort();
            string after = "";
            valueList.ForEach( (v) => { after += v + ",";});
            Debug.Log("AfterSort " + after);
            from.SetStatus(Mahjong.Status.Holding);
            from.SetMahjongValue(toValue);
            from.transform.SetSiblingIndex(valueList.IndexOf(toValue));

            if(checkGang() == true) {
                return;
            } else if( checkHupai()) {
                handleHupai();
            }
        }
    }
}