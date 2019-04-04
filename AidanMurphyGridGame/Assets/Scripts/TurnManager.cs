using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TurnManager : MonoBehaviour
{

    public int TurnCount;

    public Text txt;

    public Text winText;

    public GameObject player1;

    public GameObject player2;

    Grid grid;

    bool gameOver = false;

    public GameObject GridManager; //Grid Manager Object

    // Start is called before the first frame update
    void Start()
    {
        //Get Grid
        grid = GridManager.GetComponent<Grid>();
        TurnCount = 0;

        //Set the current turn
        if (TurnCount % 2 == 0)
        {
            player1.GetComponent<Unit>().currTurn = true;
            player2.GetComponent<Unit>().currTurn = false;
        }
        else
        {
            player1.GetComponent<Unit>().currTurn = false;
            player2.GetComponent<Unit>().currTurn = true;
        }
        //Change Turn Text
        txt.text = "Turn: 1";
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
        //If Players are on same space
        if (grid.NodeFromWorldPosition(player1.transform.position).Position == grid.NodeFromWorldPosition(player2.transform.position).Position)
        {
            //Determine whos turn it is and do win condition
            if (TurnCount % 2 == 0)
            {
                Win("Player 1 Wins");
            }
            else
            {
                Win("Player 2 Wins");
            }
        }

        //Update Turn Count
        if (TurnCount % 2 == 0)
        {
            player1.GetComponent<Unit>().currTurn = true;
            player2.GetComponent<Unit>().currTurn = false;
        }
        else
        {
            player1.GetComponent<Unit>().currTurn = false;
            player2.GetComponent<Unit>().currTurn = true;
        }

        //Go to next turn
        NextTurn();

        //If a player has won, and press enter, reload the game
        if (gameOver && Input.GetKeyDown(KeyCode.Return))
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }

    //Win Condition and display text
    public void Win(string newText)
    {
        gameOver = true;
        player1.GetComponent<Unit>().enabled = false;
        player2.GetComponent<Unit>().enabled = false;
        winText.text = newText;
    } 

    //Check to see what the next turn should be and make sure player has made a move
    void NextTurn()
    {
        //Check to see which player it is
        if (TurnCount % 2 == 0)
        {
            if(player1.GetComponent<Unit>().Move != true)
            {
                changeTurn();
            }
        }
        else
        {
            if (player2.GetComponent<Unit>().Move != true)
            {
                changeTurn();
            }
        }
       
    }

    //If Space is pressed and the game isn't over, go to next turn and disable other player
    void changeTurn()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !gameOver)
        {

            //Increase Turn and Change Count
            TurnCount++;
            txt.text = "Turn: " + (TurnCount + 1);

            //Check for turn and change it accordingly
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
