using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("[CanvasController] Start");
        SetupScaler();
    }

    private void SetupScaler()
    {
        CanvasScaler cs = GetComponent<CanvasScaler>();
        // 横屏
        if(Screen.width > Screen.height) {
            float refWidth = 1280;
            float refHeight = Screen.height / Screen.width * refWidth;
            cs.referenceResolution = new Vector2(refWidth,  refHeight);
        // 竖屏
        } else {
            float refWidth = 800;
            float refHeight = Screen.height / Screen.width * refWidth;
            cs.referenceResolution = new Vector2(refWidth,  refHeight);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
