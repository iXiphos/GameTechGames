using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject groundCollider;

    Rigidbody2D rgbd;

    float defaultGravity;

    public float moveSpeed;
    public float jumpSpeed;

    private bool grounded;

    public float gravityIncreaseAmount;

    // Start is called before the first frame update
    void Start()
    {
        rgbd = gameObject.GetComponent<Rigidbody2D>();
        defaultGravity = rgbd.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        grounded = groundCollider.GetComponent<FloorDetection>().touchingFloor;
        Movement();
        Jump();
    }

    void Movement()
    {
        float moveX = Input.GetAxis("Horizontal");
        rgbd.velocity = new Vector2(moveX * moveSpeed, rgbd.velocity.y);
    }

    void Jump()
    {
        if(Input.GetButtonDown("Jump") && grounded)
        {
            rgbd.AddForce(Vector2.up * jumpSpeed);
        }

        //if(rgbd.velocity.y < 0)
        //{
        //    rgbd.gravityScale = defaultGravity * gravityIncreaseAmount * Time.deltaTime;
        //}else if(rgbd.velocity.y == 0)
        //{
        //    rgbd.gravityScale = defaultGravity;
        //}

    }

}
