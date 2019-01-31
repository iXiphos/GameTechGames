using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pathfinding : MonoBehaviour
{
    Grid grid; //Grid script
    public Transform StartPosition; //Start location    
    public Transform TargetPosition; //End location

    public GameObject Enemy; //Enemy

    private Node memes; //The node enemy should move towards

    private bool move = true;

    private List<Node> path;


    private void Awake()
    {
        grid = GetComponent<Grid>(); //Get Grid Component
    }

    private void FixedUpdate()
    {
        FindPath(StartPosition.position, TargetPosition.position); //Find path between enemy and player
        if (move)
        {
            StartCoroutine(Movement());
        }
    }

    IEnumerator Movement()
    {
        move = false;
        if (grid.FinalPath.Count != 0) Enemy.transform.position = grid.FinalPath[0].Position;
        else Enemy.GetComponent<ResetGame>().reset = true;
        yield return new WaitForSeconds(0.5f);
        move = true;
    }

    void FindPath(Vector3 a_StartPos, Vector3 a_TargetPos)
    {
        Node StartNode = grid.NodeFromWorldPosition(a_StartPos, true);
        Node TargetNode = grid.NodeFromWorldPosition(a_TargetPos, false);

        List<Node> OpenList = new List<Node>();
        HashSet<Node> ClosedList = new HashSet<Node>();

        OpenList.Add(StartNode);

        while(OpenList.Count > 0)
        {
            Node CurrentNode = OpenList[0];
            for(int i = 1; i < OpenList.Count; i++)
            {
                if(OpenList[i].FCost < CurrentNode.FCost || OpenList[i].FCost == CurrentNode.FCost && OpenList[i].hCost < CurrentNode.hCost)
                {
                    CurrentNode = OpenList[i];
                }
            }
            OpenList.Remove(CurrentNode);
            ClosedList.Add(CurrentNode);

            if(CurrentNode == TargetNode)
            {
                GetFinalPath(StartNode, TargetNode);
            }

            foreach(Node NeighborNode in grid.GetNeighboringNodes(CurrentNode))
            {
                if(!NeighborNode.isWall || ClosedList.Contains(NeighborNode))
                {
                    continue;
                }
                int MoveCost = CurrentNode.gCost + GetmanhattenDistance(CurrentNode, NeighborNode);
                if(MoveCost < NeighborNode.gCost || !OpenList.Contains(NeighborNode))
                {
                    NeighborNode.gCost = MoveCost;
                    NeighborNode.hCost = GetmanhattenDistance(NeighborNode, TargetNode);
                    NeighborNode.Parent = CurrentNode;
                    if (!OpenList.Contains(NeighborNode))
                    {
                        OpenList.Add(NeighborNode);
                    }
                }

            }

        }
    }

    void GetFinalPath(Node a_StartNode, Node a_TargetNode)
    {
        List<Node> FinalPath = new List<Node>();
        Node CurrentNode = a_TargetNode;
        //FinalPath.Add(a_TargetNode);
        while (CurrentNode != a_StartNode)
        {
            FinalPath.Add(CurrentNode);
            CurrentNode = CurrentNode.Parent;
        };
        FinalPath.Reverse();
        grid.FinalPath = FinalPath;
    }

    public int GetmanhattenDistance(Node nodeA, Node nodeB)
    {
        int ix = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int iy = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        return ix + iy;
    }
}
