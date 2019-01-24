using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{

    public string myName = "Aidan Murphy";
    public int myAge = 23;
    float numberWithDecimals = 31.3f;
    GameObject player;
    public GameObject player2;

    int counter = 0;
    public int squareSpeed = 2;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        counter++;
        if (counter % squareSpeed == 0)
        {
            MoveSquare();
        }

    }

    void MoveSquare()
    {
        int rX = Random.Range(-5, 5);
        int rY = Random.Range(-5, 5);
        Vector2 newPos = new Vector2(rX, rY);
        player.transform.position = newPos;
    }
}
