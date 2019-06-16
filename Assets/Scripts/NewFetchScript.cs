using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MahjongGame.Controllers
{
    public class NewFetchScript : MonoBehaviour
    {
        internal bool ReceiveMahjong(Mahjong new_mahjong)
        {
            GameObject currentMahjong = GameObject.Find("CurrentMahjong");
            if (currentMahjong != null)
            {
                GameObject.Destroy(currentMahjong);
            }
            new_mahjong.name = "CurrentMahjong";
            new_mahjong.transform.SetParent(this.transform, false);
            new_mahjong.transform.SetSiblingIndex(0);
            new_mahjong.SetStatus(Mahjong.Status.HoldingToThrow);
            return true;
        }
        internal bool ReceiveMahjong(int value)
        {
            Debug.Log("[ReceiveMahjong]");
            GameObject currentMahjong = GameObject.Find("CurrentMahjong");
            if (currentMahjong != null)
            {
                Mahjong new_mahjong = currentMahjong.GetComponent<Mahjong>();
                new_mahjong.SetMahjongValue(value);
                new_mahjong.SetStatus(Mahjong.Status.HoldingToThrow);
            }
            return true;
        }
    }
}