using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Transform startPosition;

    public LayerMask WallMask;
    public Vector3 gridWorldSize;
    public float nodeRadius;
    public float Distance;
    public List<Node> FinalPath;

    Node[,] grid;

    float nodeDiameter;
    int gridSizeX, gridSizeY;

    int x = 0, y = 0;

    public GameObject cicle;

    public void Awake()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid(); 
    }

    public int MaxSize
    {
        get
        {
            return gridSizeX * gridSizeY;
        }
    }

    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector2 bottomLeft = new Vector2(transform.position.x, transform.position.y) - Vector2.right * gridWorldSize.x / 2 - Vector2.up * gridWorldSize.y / 2;
        for (y = 0; y < gridSizeY; y++)
        {
            for (x = 0; x < gridSizeX; x++)
            {
                Vector2 worldPoint = bottomLeft + Vector2.right * (x * nodeDiameter + nodeRadius) + Vector2.up * (y * nodeDiameter + nodeRadius);
                bool Wall = false;
                if(Physics2D.OverlapBox(worldPoint, new Vector2(nodeRadius, nodeRadius), 0, WallMask))
                {
                    Wall = true;
                    Debug.Log(Physics2D.OverlapBox(worldPoint, new Vector2(nodeRadius, nodeRadius), 0, WallMask).name);
                }
                grid[x, y] = new Node(Wall, worldPoint, x, y);
                if (Wall)
                GameObject.Instantiate(cicle, grid[x,y].Position, Quaternion.Euler(0, 0, 0));
            }
        }
    }

    public Node NodeFromWorldPosition(Vector3 a_WorldPosition, bool enemy)
    {
        float xpoint = ((a_WorldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x);
        float ypoint = ((a_WorldPosition.y + gridWorldSize.y / 2) / gridWorldSize.y);

        xpoint = Mathf.Clamp01(xpoint);
        ypoint = Mathf.Clamp01(ypoint);
        int x = Mathf.RoundToInt((gridSizeX - 1) * xpoint);
        int y = Mathf.RoundToInt((gridSizeY - 1) * ypoint);

        return grid[x, y];
    }

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
