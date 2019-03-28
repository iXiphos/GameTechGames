using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pathfinding : MonoBehaviour
{
    Grid grid; //Grid script

    PathRequestManager requestManager; //Request Manager
    private void Awake()
    {
        grid = GetComponent<Grid>(); //Get Grid Component
        requestManager = GetComponent<PathRequestManager>(); //Get Request Manager
    }

    //Start to Find the Path between two points
    public void StartFindPath(Vector3 startPos, Vector3 targetPos)
    {
        StartCoroutine(FindPath(startPos, targetPos));
    }

    IEnumerator FindPath(Vector3 a_StartPos, Vector3 a_TargetPos)
    {
        //Points to keep track off
        Vector3[] wayPoints = new Vector3[0];
        //Is it possible
        bool pathSuccess = false;

        //Get start and end nodes
        Node StartNode = grid.NodeFromWorldPosition(a_StartPos);
        Node TargetNode = grid.NodeFromWorldPosition(a_TargetPos);

        //Check if the nodes are walls or not
        if (!StartNode.isWall && !TargetNode.isWall)
        {
            //Use the Heap sorting algorithem to Efficiently search the nodes
            Heap<Node> OpenList = new Heap<Node>(grid.MaxSize);

            //HashSet of Checked Nodes
            HashSet<Node> ClosedList = new HashSet<Node>();

            //Add StartNode
            OpenList.Add(StartNode);

            //Loop through Nieghboring Nodes and calculate and compare the cost of each action
            while (OpenList.Count > 0)
            {
                //Current Node being checked
                Node CurrentNode = OpenList.RemoveFirst();

                //Add to Checked List
                ClosedList.Add(CurrentNode);

                //Check if last node
                if (CurrentNode == TargetNode)
                {
                    pathSuccess = true;
                    break;
                }

                //Loop through all neighboring Nodes
                foreach (Node NeighborNode in grid.GetNeighboringNodes(CurrentNode))
                {
                    //Check if wall or if already checked
                    if (NeighborNode.isWall || ClosedList.Contains(NeighborNode))
                    {
                        continue;
                    }
                    //Calculate move cost for the nodes
                    int MoveCost = CurrentNode.gCost + GetManhattenDistance(CurrentNode, NeighborNode);
                    if (MoveCost < NeighborNode.gCost || !OpenList.Contains(NeighborNode))
                    {
                        NeighborNode.gCost = MoveCost;
                        NeighborNode.hCost = GetManhattenDistance(NeighborNode, TargetNode);
                        NeighborNode.Parent = CurrentNode;
                        if (!OpenList.Contains(NeighborNode))
                        {
                            OpenList.Add(NeighborNode);
                        }
                        else
                        {
                            OpenList.UpdateItem(NeighborNode);
                        }
                    }

                }

            }
        }
        yield return null;
        //If path was a success, get the final path
        if (pathSuccess)
        {
            wayPoints = GetFinalPath(StartNode, TargetNode);
        }
        //Process the final path so Unit can move
        requestManager.FinishedProcessingPath(wayPoints, pathSuccess);
    }

    Vector3[] GetFinalPath(Node a_StartNode, Node a_TargetNode)
    {
        List<Node> FinalPath = new List<Node>();
        Node CurrentNode = a_TargetNode;
        while (CurrentNode != a_StartNode)
        {
            FinalPath.Add(CurrentNode);
            CurrentNode = CurrentNode.Parent;
        }
        FinalPath.Add(CurrentNode);
        //Debug.Log(FinalPath.Count);
        Vector3[] waypoints = VectorPath(FinalPath);
        Array.Reverse(waypoints);
        return waypoints;
    }

    //Used for optimizing paths but can cause clipping
    Vector3[] SimplifyPath(List<Node> path)
    {
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 directionOld = Vector2.zero;
        for(int i = 1; i < path.Count; i++)
        {
            Vector2 directionNew = new Vector2(path[i-1].gridX - path[i].gridX, path[i-1].gridY - path[i].gridY);
            if(directionNew != directionOld)
            {
                waypoints.Add(path[i].Position);
            }
            directionOld = directionNew;
        }
        return waypoints.ToArray();
    }

    //Get all Nodes in the path
    Vector3[] VectorPath(List<Node> path)
    {
        List<Vector3> waypoints = new List<Vector3>();
        for (int i = 0; i < path.Count; i++)
        {
            waypoints.Add(path[i].Position);
        }
        return waypoints.ToArray();
    }

    //Get the distance between two nodes
    public int GetManhattenDistance(Node nodeA, Node nodeB)
    {
        int ix = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int iy = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        return ix + iy;
    }
}
