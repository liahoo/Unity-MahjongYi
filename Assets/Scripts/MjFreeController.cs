using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MahjongGame.Controllers
{
    public class MjFreeController : MahjongController
    {
        // Start is called before the first frame update
        public void OnPointUp()
        {
            ShowFront(back.gameObject.activeSelf);//如果显示的是背面，则显示正面，反之亦然
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}