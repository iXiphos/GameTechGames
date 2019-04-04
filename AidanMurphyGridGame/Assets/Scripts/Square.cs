using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;

    //Get Players
    private void Awake()
    {
        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");
    }

    //On Mouse Over, Change color and set as current location
    private void OnMouseOver()
    {
        player1.GetComponent<Unit>().currLoc = transform.position;
        player2.GetComponent<Unit>().currLoc = transform.position;
        Color tmp = gameObject.GetComponent<SpriteRenderer>().color;
        tmp.a = 1f;
        gameObject.GetComponent<SpriteRenderer>().color = tmp;
    }

    //When you leave the square
    private void OnMouseExit()
    {
        Color tmp = gameObject.GetComponent<SpriteRenderer>().color;
        tmp.a = 110f / 255f;
        gameObject.GetComponent<SpriteRenderer>().color = tmp;
    }
}
