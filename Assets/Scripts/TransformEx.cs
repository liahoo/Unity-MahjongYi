using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformEx{

    public static Transform Clear(this Transform transform)
    {
        foreach (Transform child in transform) {
            GameObject.Destroy(child.gameObject);
        }
        return transform;
    } 
}
