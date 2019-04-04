using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Unit : MonoBehaviour
{
    Grid grid;
    public GameObject GridManager; //Grid Manager Object
    public GameObject TurnManager; //Player
    public float speed = 5; //How fast the enemy should move
    Vector3[] path; //Array of the path enemy should take
    int targetIndex; //Current target location in path

    public int PlayerNumber;

    public int MoveAmount;

    public bool Move = true;

    public Color color;

    private LineRenderer line;

    public int playerNum;

    public bool currTurn = true;

    public Vector3 currLoc;

    // Start is called before the first frame update
    void Start()
    {
        // Add a Line Renderer to the GameObject
        line = this.gameObject.AddComponent<LineRenderer>();
        // Set the width of the Line Renderer
        line.SetWidth(0.05F, 0.05F);
        // Set the number of vertex fo the Line Renderer
        grid = GridManager.GetComponent<Grid>();
        transform.position = grid.NodeFromWorldPosition(transform.position).Position;
    }


    void Update()
    {
        //Check the Tiles around the player to see if they have lost
        checkTilesAround();

        //If they can move, display the line
        if (Move)
        {
            line.enabled = true;
        }
        else
        {
            line.enabled = false;
        }
        
        //If it is the current players turn allow them to move
        if (currTurn) MovePlayer();
    }

    //Display the path for the player, and if they right click, move them to that position
    void MovePlayer()
    {
        //If Player Left Clicks, path them to the location
        if (Input.GetMouseButtonDown(0) && Move)
        {
            PathRequestManager.RequestPath(transform.position, currLoc, OnPathFound);
        }
        else if (Input.GetMouseButtonDown(1) && Move && !grid.NodeFromWorldPosition(currLoc).isWall) //Else if it is not a wall and they right click, change it to a wall
        {
            Move = false;
            blockTile();
        }
        else if(Move) //Else if they can move, draw a path
        {
            PathRequestManager.RequestPath(transform.position, currLoc, drawPath);
        }
    }

    //Check for tiles around player and see if they are walls
    void checkTilesAround()
    {
        bool captured = false;

        //Check all nodes around player
        List<Node> tmp;
        tmp = grid.GetNeighboringNodes(grid.NodeFromWorldPosition(transform.position));
        foreach(Node node in tmp) {
            if (!node.isWall) //If one is not a wall, they are are not captured
            {
                captured = true;
            }
        }

        //If they can't move they the other player wins
        if (!captured)
        {
            TurnManager.GetComponent<TurnManager>().Win("Player " + playerNum + " wins");
        }
    }
    
    //Turn the tile into a wall and change the color
    void blockTile()
    {
        Node tmp = grid.NodeFromWorldPosition(currLoc);
        tmp.square.GetComponent<SpriteRenderer>().color = new Color(253f, 255f, 0f, 110f / 255f);
        tmp.isWall = true;
    }

    //Display the path using a line renderer
    public void drawPath(Vector3[] newPath, bool pathSuccessful)
    {
        //If the path is a certain size, display the path
        if (pathSuccessful && newPath.Length <= MoveAmount && newPath.Length > 0)
        {
            path = newPath;
            targetIndex = 0;
            line.positionCount = path.Length;
            for (int i = targetIndex; i < path.Length; i++)
            {
                line.SetPosition(i, path[i]);
            }
        }
        else
        {
            line.positionCount = 0;
        }
    }

    //If a path is found by PathFinding, start to move across path
    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        //If the path doesn't exist, reset the game
        if (pathSuccessful && newPath.Length <= MoveAmount)
        {
            Move = false;
            path = newPath;
            targetIndex = 0;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    //Follow the path given
    IEnumerator FollowPath()
    {
        //Current node the enemy needs to move towards
        Vector3 currentWaypoint = path[0];

        while (true)
        {
            //When at a point, move to next point
            if(transform.position == currentWaypoint)
            {
                targetIndex++;
                if(targetIndex >= path.Length)
                {
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            yield return null;

        }
    }
}
