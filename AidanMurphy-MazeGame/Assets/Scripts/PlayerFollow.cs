using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    public GameObject player;

    public float offset;

    void Update()
    {
        CameraFollow();    
    }

    //Have Camera Follow Player
    void CameraFollow()
    {
        Vector3 camPosition = transform.position;//get the players position in worlds space
        camPosition = Camera.main.WorldToScreenPoint(camPosition);//convert the world space coordinates to screen coordinate
        Vector3 mousePosition = player.transform.position; //Get the mouse position in screen pixels
        mousePosition = Camera.main.WorldToScreenPoint(mousePosition);
        Vector3 diff = camPosition - mousePosition;// get the difference between player and mouse
        camPosition -= diff / offset; //Divide by a number, the higher, the longer it will take for player to reach the mouse
        Vector3 adjustedPosition = Camera.main.ScreenToWorldPoint(camPosition);//convert screen coordinates to world space
        adjustedPosition.z = -10f;
        transform.position = adjustedPosition;

    }

}
