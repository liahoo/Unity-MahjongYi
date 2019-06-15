using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MahjongGame.Controllers
{
    public class MjFreeController : Mahjong
    {
        public GameObject MyTiles;
        // Start is called before the first frame update
        void Start() {
            
        }
        public void OnPointUp()
        {
            // ShowFront(back.gameObject.activeSelf);//如果显示的是背面，则显示正面，反之亦然
            AddToMyMahjong();
        }

        private void AddToMyMahjong()
        {
            // MyTilesScript myMahjong = MyTiles.GetComponent<MyTilesScript>() as MyTilesScript;
            // myMahjong.Grab(GetMahjongValue());
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}