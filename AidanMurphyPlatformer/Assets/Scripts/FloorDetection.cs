using System.Collections;
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
        layer = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Physics2D.OverlapBox(transform.position, new Vector2(1.1f, 1.1f), layer)) touchingFloor = true;
        else touchingFloor = false;
    }
}
