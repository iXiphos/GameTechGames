using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Transform startPosition; //Where the grid should start

    public LayerMask WallMask; // What layermask should be considered wall
    public Vector3 gridWorldSize; //How large the grid should be
    public float nodeRadius; //Radius of each node
    public float Distance;
    public List<Node> FinalPath; //List for final path

    Node[,] grid;

    public GameObject square;

    float nodeDiameter;
    int gridSizeX, gridSizeY;

    int x, y;

    public void Awake()
    {
        x = 0;
        y = 0;
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    }

    //Max Size of Grid
    public int MaxSize
    {
        get
        {
            return gridSizeX * gridSizeY;
        }
    }

    //Generate a grid that is made out of nodes
    void CreateGrid()
    {
        //Starting Node
        grid = new Node[gridSizeX, gridSizeY];
        //Vector Math to find bottom left location in grid
        Vector2 bottomLeft = new Vector2(transform.position.x, transform.position.y) - Vector2.right * gridWorldSize.x / 2 - Vector2.up * gridWorldSize.y / 2;
        //Loop through the points one by one and create a node and figure out if the node is a wall
        for (y = 0; y < gridSizeY; y++)
        {
            for (x = 0; x < gridSizeX; x++)
            {
                Vector2 worldPoint = bottomLeft + Vector2.right * (x * nodeDiameter + nodeRadius) + Vector2.up * (y * nodeDiameter + nodeRadius);
                bool Wall = false;
                if(Physics2D.OverlapBox(worldPoint, new Vector2(nodeRadius, nodeRadius), 0, WallMask))
                {
                    Wall = true;
                }

                GameObject Square = GameObject.Instantiate(square, worldPoint, gameObject.transform.rotation);
                Square.transform.parent = transform;
                Square.name = "Square-" + (x) + "-" + (gridSizeY - y - 1);
                //Create Node in Grid
                grid[x, y] = new Node(Wall, worldPoint, x, y, Square);
            }
        }
    }

    //Turn the World position of Game Object to Node location
    public Node NodeFromWorldPosition(Vector3 a_WorldPosition)
    {
        float xpoint = ((a_WorldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x);
        float ypoint = ((a_WorldPosition.y + gridWorldSize.y / 2) / gridWorldSize.y);

        xpoint = Mathf.Clamp01(xpoint);
        ypoint = Mathf.Clamp01(ypoint);
        int x = Mathf.RoundToInt((gridSizeX - 1) * xpoint);
        int y = Mathf.RoundToInt((gridSizeY - 1) * ypoint);

        return grid[x, y];
    }

    public GameObject SquareFromWorldPosition(Vector3 a_WorldPosition)
    {
        float xpoint = ((a_WorldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x);
        float ypoint = ((a_WorldPosition.y + gridWorldSize.y / 2) / gridWorldSize.y);

        xpoint = Mathf.Clamp01(xpoint);
        ypoint = Mathf.Clamp01(ypoint);
        int x = Mathf.RoundToInt((gridSizeX - 1) * xpoint);
        int y = Mathf.RoundToInt((gridSizeY - 1) * ypoint);

        return grid[x, y].square;
    }

    //Return a list of nodes that represent the nodes next to object
    public List<Node> GetNeighboringNodes(Node a_Node)
    {
        List<Node> NeighboringNodes = new List<Node>();
        int xCheck, yCheck;

        xCheck = a_Node.gridX + 1;
        yCheck = a_Node.gridY;
        //Get Right Node
        if(xCheck >= 0 && xCheck < gridSizeX)
        {
            if(yCheck >= 0 && yCheck < gridSizeY)
            {
                NeighboringNodes.Add(grid[xCheck, yCheck]);
            }
        }

        xCheck = a_Node.gridX - 1;
        yCheck = a_Node.gridY;
        //Get Left Node
        if (xCheck >= 0 && xCheck < gridSizeX)
        {
            if(yCheck >= 0 && yCheck < gridSizeY)
            {
                NeighboringNodes.Add(grid[xCheck, yCheck]);
            }
        }

        xCheck = a_Node.gridX;
        yCheck = a_Node.gridY + 1;
        //Get Up Load
        if (xCheck >= 0 && xCheck < gridSizeX)
        {
            if (yCheck >= 0 && yCheck < gridSizeY)
            {
                NeighboringNodes.Add(grid[xCheck, yCheck]);
            }
        }
        xCheck = a_Node.gridX;
        yCheck = a_Node.gridY - 1;
        //Get Down Node
        if (xCheck >= 0 && xCheck < gridSizeX)
        {
            if (yCheck >= 0 && yCheck < gridSizeY)
            {
                NeighboringNodes.Add(grid[xCheck, yCheck]);
            }
        }

        return NeighboringNodes;
    }

}
