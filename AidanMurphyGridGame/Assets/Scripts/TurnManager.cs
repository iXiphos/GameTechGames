using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{

    public int TurnCount;

    public Text txt;

    public GameObject player1;

    public GameObject player2;

    Grid grid;

    public GameObject GridManager; //Grid Manager Object

    // Start is called before the first frame update
    void Start()
    {
        grid = GridManager.GetComponent<Grid>();
        TurnCount = 0;
        if (TurnCount % 2 == 0)
        {
            player1.GetComponent<Unit>().enabled = true;
            player2.GetComponent<Unit>().enabled = false;
        }
        else
        {
            player1.GetComponent<Unit>().enabled = false;
            player2.GetComponent<Unit>().enabled = true;
        }
        txt.text = "Turn: 0";
    }

    // Update is called once per frame
    void Update()
    {
        if(grid.NodeFromWorldPosition(player1.transform.position).Position == grid.NodeFromWorldPosition(player2.transform.position).Position)
        {
            if (TurnCount % 2 == 0)
            {
                Debug.Log("Player1 Wins");
            }
            else
            {
                Debug.Log("Player2 Wins");
            }
        }

        if (TurnCount % 2 == 0)
        {
            player1.GetComponent<Unit>().enabled = true;
            player2.GetComponent<Unit>().enabled = false;
        }
        else
        {
            player1.GetComponent<Unit>().enabled = false;
            player2.GetComponent<Unit>().enabled = true;
        }
        NextTurn();
    }


    void NextTurn()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TurnCount++;
            txt.text = "Turn: " + TurnCount;
            if (TurnCount % 2 == 0)
            {
                player1.GetComponent<Unit>().Move = true;
                player2.GetComponent<Unit>().Move = false;
            }
            else
            {
                player1.GetComponent<Unit>().Move = false;
                player2.GetComponent<Unit>().Move = true;
            }
        }
    }

}
