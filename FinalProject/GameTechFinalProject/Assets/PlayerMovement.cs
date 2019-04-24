using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D rgbd;

    float defaultGravity;

    public SpriteRenderer rend;

    public float moveSpeed;
    public float jumpSpeed;

    LayerMask layer;

    public float fallMultiplyer = 15f;
    public float lowJumpMultiplyer = 26f;

    public GameObject feet;

    bool isGrounded = false;

    bool shouldJump = false;

    float moveX;

    // Start is called before the first frame update
    void Start()
    {
        rgbd = gameObject.GetComponent<Rigidbody2D>();
        defaultGravity = rgbd.gravityScale;
        layer = LayerMask.GetMask("Ground");
    }

    private void Update()
    {
        Jump();
        moveX = Input.GetAxis("Horizontal");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
        if (shouldJump)
        {
            shouldJump = false;
            rgbd.velocity = Vector2.up * jumpSpeed;
        }
        BetterJump();
    }

    void Movement()
    {
        rgbd.velocity = new Vector2(moveX * moveSpeed, rgbd.velocity.y);
        RaycastHit2D col = Physics2D.Raycast(feet.transform.position, new Vector2(moveX, 0), 0.55f, layer);
        if (col.collider != null)
        {
            rgbd.velocity = new Vector2(0f, rgbd.velocity.y);
        }
    }

    void Jump()
    {
        isGrounded = feet.GetComponent<FloorDetection>().touchingFloor;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                shouldJump = true;
            }
        }
    }

    void BetterJump()
    {
        if (rgbd.velocity.y < 0) rgbd.gravityScale = fallMultiplyer;
        else if (rgbd.velocity.y > 0 && !Input.GetButton("Jump")) rgbd.gravityScale = lowJumpMultiplyer;
        else rgbd.gravityScale = 2f;
    }

}
