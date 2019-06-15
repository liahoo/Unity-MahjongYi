using System;
using System.Collections;
using System.Collections.Generic;
using MahjongGame.Controllers;
using UnityEngine;

namespace MahjongGame.Controllers
{
    public class CurrentMahjong : MonoBehaviour
    {
        public Mahjong mahjong;
        // Start is called before the first frame update
        void Start()
        {
            mahjong.SetStatus(Mahjong.Status.Gone);
        }

        // Update is called once per frame
        void Update()
        {

        }

        internal bool ReceiveMahjong(int mahjongValue)
        {
            mahjong.SetStatus(Mahjong.Status.HoldingToThrow);
            mahjong.SetMahjongValue(mahjongValue);
            return true;
        }
    }
}