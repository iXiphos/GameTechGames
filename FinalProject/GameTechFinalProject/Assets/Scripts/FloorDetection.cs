using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDetection : MonoBehaviour
{
    public bool touchingFloor = false; //Is it touching the floor

    LayerMask layer; //Layer to Check For

    public GameObject player; //Player

    // Start is called before the first frame update
    void Start()
    {
        touchingFloor = false; //Set touchingfloor to false
        layer = LayerMask.GetMask("floor"); //Get the Layer
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Collider2D col = Physics2D.OverlapBox(transform.position, new Vector2(0.9f, 0.1f), 0f, layer); //Create a overlap box to check for ground

        //If touching the floor, set touching floor to true
        if (col != null) touchingFloor = true;
        else touchingFloor = false;
    }
}
