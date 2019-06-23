using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MahjongGame.Controllers
{
    public class GangScript : MonoBehaviour
    {
        // Start is called before the first frame update
        public GameObject imageActive;
        public GameObject myMahjongs;
        public bool gangEnabled
        {
            set { imageActive.SetActive(value); }
            get { return imageActive.activeSelf; }
        }
        void Start()
        {

        }

        public void OnGangEnabled(bool to_enabled)
        {
            gangEnabled = to_enabled;
        }
        public void onPointUp()
        {
            Debug.Log("[onPointUp] gangEnabled=" + gangEnabled);
            if (gangEnabled)
            {
                if (myMahjongs != null)
                {
                    MyTilesScript myTilesScript = myMahjongs.GetComponent<MyTilesScript>();
                    if (myTilesScript != null)
                    {
                        myTilesScript.OnGangClicked();
                    }
                }
            }
        }
    }
}