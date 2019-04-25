using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float xSpeed; //How fast the player should move on the X Axis
    float xMove = 0; 
    public float ySpeed; //How fast the player should move on the Y Axis
    float yMove = 0;
    float boundsLeft = -17f; //How far the player can go left
    float boundsRight = 17f; //How far the player can go right
    float boundsUp = 10f; //How far the player can go up
    float boundsDown = -10f; //How far the player can go down

    Rigidbody2D rb2d; //Players rigidbody

    // Start is called before the first frame update
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>(); //Get rigidbody
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput(); //Check Input from user
    }

    void FixedUpdate()
    {
        Move(); //Move Player
        CheckBounds(); //Check the bounds of the player
    }

    void Move()
    {
        Vector2 newVelocity = new Vector2(xMove, yMove); //Get input direction
        rb2d.velocity = newVelocity; //set velocity to that
    }

    //Check to see if player is out of bounds and if so, put him back with play space
    void CheckBounds()
    {
        Vector2 maxPosX; //Where to lock players X
        Vector2 maxPosY; //Where to lock players Y
        if(transform.position.x < boundsLeft) //If player goes to left bounds
        {
            maxPosX = new Vector2(boundsLeft, transform.position.y);
            transform.position = maxPosX;
        }
        else if(transform.position.x > boundsRight) //If player goes to right bounds
        {
            maxPosX = new Vector2(boundsRight, transform.position.y);
            transform.position = maxPosX;
        }

        if(transform.position.y < boundsDown) //If player goes to down bounds
        {
            maxPosY = new Vector2(transform.position.x, boundsDown);
            transform.position = maxPosY;
        }
        else if (transform.position.y > boundsUp) //If player goes to up bounds
        {
            maxPosY = new Vector2(transform.position.x, boundsUp);
            transform.position = maxPosY;
        }
    }

    //Check for user input
    void CheckInput()
    {
        xMove = Input.GetAxis("Horizontal") * xSpeed;
        yMove = Input.GetAxis("Vertical") * ySpeed;
    }
}
