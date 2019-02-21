using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDetection : MonoBehaviour
{
    public bool touchingFloor = false;

    LayerMask layer;

    // Start is called before the first frame update
    void Start()
    {
        layer = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Physics2D.OverlapBox(transform.position, Vector2.one, 0, layer)) touchingFloor = true;
        else touchingFloor = false;
    }
}
