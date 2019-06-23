using UnityEngine;

namespace MahjongGame.Controllers
{
    public class NewFetchScript : MonoBehaviour
    {
        public GameObject currentMahjong;
        internal bool ReceiveMahjong(int value)
        {
            Debug.Log("[ReceiveMahjong]");
            if(!currentMahjong) currentMahjong = GameObject.Find("CurrentMahjong");
            if (currentMahjong != null)
            {
                Mahjong new_mahjong = currentMahjong.GetComponent<Mahjong>();
                new_mahjong.SetMahjongValue(value);
                new_mahjong.SetStatus(Mahjong.Status.Holding);
            }
            return true;
        }
    }
}