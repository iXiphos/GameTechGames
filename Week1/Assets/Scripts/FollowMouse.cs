using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Follow();
        FollowWithEase();
    }

    void Follow()
    {
        Vector3 mousePos = Input.mousePosition; //Get mouse position in screen
        Debug.Log(mousePos);
        //Convert Screen Coordinates to World Space
        Vector3 adjustedPosition = Camera.main.ScreenToWorldPoint(mousePos);
        //Clear the Z axis
        adjustedPosition.z = 0f;
        //Reposition Player
        transform.position = adjustedPosition;
    }

    void FollowWithEase()
    {
        Vector3 playerPos = transform.position; //Get Player Position
        //Convert world space coordinates to screen coordinates
        playerPos = Camera.main.WorldToScreenPoint(playerPos);
        Vector3 mousePos = Input.mousePosition; //Get Mouse Position in screen Pixels
        Vector3 diff = playerPos - mousePos; //Get Difference between player and mouse
        playerPos -= diff / 8; //Divide by a number, the higher the number, the longer the transition
        //Converting the Screen to world space
        Vector3 adjustedPosition = Camera.main.ScreenToWorldPoint(playerPos);
        //Zero out the z
        adjustedPosition.z = 0;
        transform.position = adjustedPosition;
    }
}
