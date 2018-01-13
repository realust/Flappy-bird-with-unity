﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public float minHeight;
    public float maxHeight;
    public GameObject pivot;

    // Use this for initialization
    void Start()
    {
        ChangeHeight();
    }

    void ChangeHeight()
    {
        float height = Random.Range(minHeight, maxHeight);
        pivot.transform.localPosition = new Vector3(0.0f, height, 0.0f);
    }

    void OnScrollEnd() {
        ChangeHeight();
    }
}
