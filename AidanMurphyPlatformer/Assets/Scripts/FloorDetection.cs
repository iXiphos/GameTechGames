﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDetection : MonoBehaviour
{
    public bool touchingFloor = false;

    LayerMask layer;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        touchingFloor = false;
        layer = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Collider2D col = Physics2D.OverlapCircle(transform.position, 1f, layer);
        if (col != null) touchingFloor = true;
        else touchingFloor = false;
    }
}
