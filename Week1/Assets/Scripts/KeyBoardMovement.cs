using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBoardMovement : MonoBehaviour
{
    float xMove = 0f;
    float yMove = 0f;

    public float playerSpeed = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkInput();
        MovePlayer();
    }

    void checkInput()
    {
        xMove = Input.GetAxis("Horizontal");
        yMove = Input.GetAxis("Vertical");
    }

    void MovePlayer()
    {
        transform.Translate(new Vector3(xMove, yMove, 0) / playerSpeed);
    }

}
