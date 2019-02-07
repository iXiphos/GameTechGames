using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The way my movement works, was causing on trigger enter to break. I had to make my own collision using the system I was using
public class Collectable : MonoBehaviour
{
    Grid grid; //Grid Component

    Node StartNode; //Node of GameObject
     
    Node PlayerNode; //Player Node

    public GameObject player; //Get Player

    public GameObject Enemy; //Get Enemy


    public GameObject GridManager; //Get A* manager

    public GameObject text;

    // Start is called before the first frame update
    void Start()
    {
        //Get Grid
        grid = GridManager.GetComponent<Grid>();
    }

    // Update is called once per frame
    void Update()
    {
        //Get Player Node and Current Node
        StartNode = grid.NodeFromWorldPosition(transform.position, true);
        PlayerNode = grid.NodeFromWorldPosition(player.transform.position, true);

        //Make sure it is on a node
        transform.position = StartNode.Position;

        //Simulate collision by testing if player and object are on same node
        if (PlayerNode == StartNode)
        {
            Enemy.SetActive(true);
            text.GetComponent<CoinsCollected>().coinsCollected++;
            gameObject.SetActive(false);
        }
    }
}
