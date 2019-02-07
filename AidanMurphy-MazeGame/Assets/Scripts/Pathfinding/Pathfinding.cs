using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pathfinding : MonoBehaviour
{
    Grid grid; //Grid script

    PathRequestManager requestManager;
    private void Awake()
    {
        grid = GetComponent<Grid>(); //Get Grid Component
        requestManager = GetComponent<PathRequestManager>();
    }

    public void StartFindPath(Vector3 startPos, Vector3 targetPos)
    {
        StartCoroutine(FindPath(startPos, targetPos));
    }


    IEnumerator FindPath(Vector3 a_StartPos, Vector3 a_TargetPos)
    {

        Vector3[] wayPoints = new Vector3[0];

        bool pathSuccess = false;

        Node StartNode = grid.NodeFromWorldPosition(a_StartPos, true);
        Node TargetNode = grid.NodeFromWorldPosition(a_TargetPos, false);


        if (!StartNode.isWall && !TargetNode.isWall)
        {
            //Debug.Log("working");
            Heap<Node> OpenList = new Heap<Node>(grid.MaxSize);
            HashSet<Node> ClosedList = new HashSet<Node>();

            OpenList.Add(StartNode);

            while (OpenList.Count > 0)
            {
                Node CurrentNode = OpenList.RemoveFirst();

                ClosedList.Add(CurrentNode);

                if (CurrentNode == TargetNode)
                {
                    pathSuccess = true;
                    break;
                }

                foreach (Node NeighborNode in grid.GetNeighboringNodes(CurrentNode))
                {
                    if (NeighborNode.isWall || ClosedList.Contains(NeighborNode))
                    {
                        continue;
                    }
                    int MoveCost = CurrentNode.gCost + GetmanhattenDistance(CurrentNode, NeighborNode);
                    if (MoveCost < NeighborNode.gCost || !OpenList.Contains(NeighborNode))
                    {
                        NeighborNode.gCost = MoveCost;
                        NeighborNode.hCost = GetmanhattenDistance(NeighborNode, TargetNode);
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
        if (pathSuccess)
        {
            wayPoints = GetFinalPath(StartNode, TargetNode);
        }
        Debug.Log(pathSuccess);
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
        //Debug.Log(FinalPath.Count);
        Vector3[] waypoints = VectorPath(FinalPath);
        Array.Reverse(waypoints);
        return waypoints;
    }

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

    Vector3[] VectorPath(List<Node> path)
    {
        List<Vector3> waypoints = new List<Vector3>();
        for (int i = 0; i < path.Count; i++)
        {
            waypoints.Add(path[i].Position);
        }
        return waypoints.ToArray();
    }

    public int GetmanhattenDistance(Node nodeA, Node nodeB)
    {
        int ix = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int iy = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        return ix + iy;
    }
}
