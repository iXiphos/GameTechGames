using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float xSpeed;
    float xMove = 0;
    public float ySpeed;
    float yMove = 0;
    float boundsLeft = -5f;
    float boundsRight = 5f;
    float boundsUp = 5f;
    float boundsDown = -8.5f;

    Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    void FixedUpdate()
    {
        Move();
        CheckBounds();
    }

    void Move()
    {
        Vector2 newVelocity = new Vector2(xMove, yMove);
        rb2d.velocity = newVelocity;
    }

    void CheckBounds()
    {
        Vector2 maxPosX;
        Vector2 maxPosY;
        if(transform.position.x < boundsLeft)
        {
            maxPosX = new Vector2(boundsLeft, transform.position.y);
            transform.position = maxPosX;
        }
        else if(transform.position.x > boundsRight)
        {
            maxPosX = new Vector2(boundsRight, transform.position.y);
            transform.position = maxPosX;
        }

        if(transform.position.y < boundsDown)
        {
            maxPosY = new Vector2(transform.position.x, boundsDown);
            transform.position = maxPosY;
        }
        else if (transform.position.y > boundsUp)
        {
            maxPosY = new Vector2(transform.position.x, boundsUp);
            transform.position = maxPosY;
        }
    }

    void CheckInput()
    {
        xMove = Input.GetAxis("Horizontal") * xSpeed;
        yMove = Input.GetAxis("Vertical") * ySpeed;
    }
}
