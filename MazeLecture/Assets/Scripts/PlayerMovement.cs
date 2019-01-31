using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    float xMove = 0;
    float yMove = 0;

    Rigidbody2D rb2d;

    public float speed = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        MovePlayer();
    }

    void MovePlayer()
    {
        rb2d.AddForce(new Vector2(xMove, yMove) * speed);
    }

    void CheckInput()
    {
        xMove = Input.GetAxis("Horizontal");
        yMove = Input.GetAxis("Vertical");
    }

}
