using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int speed; //Movement Speed

    Grid grid;

    Node StartNode; //Current Node

    public GameObject GridManager; //Grid Controller

    List<Node> NeighboringNode; //Neighboring Nodes to player

    Vector3 target; //Target Location

    bool move = true; //Check if player can move

    public Transform player; //transform of play

    private Vector3 start, end; //The start and end points for each move

    private bool locSet = false;

    public float delay; //Delay between each move

    // Start is called before the first frame update
    void Start()
    {
        grid = GridManager.GetComponent<Grid>();
        StartCoroutine(Wait());
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        //If player puts in input, and they are allowed to move, check for valid location
        if ((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) && move) StartCoroutine(Movement());

        //If movement is valid location, move there
        if (locSet) Move(start, end);
    }


    //Move player from current location to target square
    void Move(Vector3 currLoc, Vector3 targetLoc)
    {
        if(currLoc != targetLoc)
        gameObject.transform.position = Vector3.MoveTowards(transform.position, targetLoc, Time.deltaTime * speed);
    }

    //Make Sure grid is initialized before getting player node
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        StartNode = grid.NodeFromWorldPosition(player.position, true);
    }

    //Check for valid movement and assign two locations if valid
    IEnumerator Movement()
    {
        //Get Starting location
        StartNode = grid.NodeFromWorldPosition(player.position, true);
        //Don't allow player to move until spot is found or it is determined that it is a wall;
        move = false;
        //Get list of neighboring nodes
        NeighboringNode = grid.GetNeighboringNodes(StartNode);

        //See what input is and check for wall, if input is not wall, set end location to Node location
        if (Input.GetAxis("Horizontal") > 0 && !NeighboringNode[0].isWall)
        {
            start = player.position;
            end = NeighboringNode[0].Position;
            locSet = true;
            yield return new WaitForSeconds(delay);
            move = true;
        }
        else if (Input.GetAxis("Horizontal") < 0 && !NeighboringNode[1].isWall)
        {
            start = player.position;
            end = NeighboringNode[1].Position;
            locSet = true;
            yield return new WaitForSeconds(delay);
            move = true;
        }
        else if (Input.GetAxis("Vertical") > 0 && !NeighboringNode[2].isWall)
        {
            start = player.position;
            end = NeighboringNode[2].Position;
            locSet = true;
            yield return new WaitForSeconds(delay);
            move = true;
        }
        else if (Input.GetAxis("Vertical") < 0 && !NeighboringNode[3].isWall)
        {
            start = player.position;
            end = NeighboringNode[3].Position;
            locSet = true;
            yield return new WaitForSeconds(delay);
            move = true;
        }
        else //If It is wall, allow player to move again
        {
            move = true;
        }
        yield return new WaitForSeconds(0.0f);
    }

}
