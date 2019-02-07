using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Unit : MonoBehaviour
{
    Grid grid;
    public GameObject GridManager; //Grid Manager Object
    public Transform target; //Player
    public float speed = 5; //How fast the enemy should move
    Vector3[] path; //Array of the path enemy should take
    int targetIndex; //Current target location in path

    // Start is called before the first frame update
    void Start()
    {
        grid = GridManager.GetComponent<Grid>();
        PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);    
    }

    //If a path is found by PathFinding, start to move across path
    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        //If the path doesn't exist, reset the game
        if (pathSuccessful)
        {
            path = newPath;
            targetIndex = 0;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
        else
        {
            gameObject.GetComponent<ResetGame>().reset = true;
        }
    }

    //Follow the path given
    IEnumerator FollowPath()
    {
        //If the player and enemy positions are the same, kill player
        if (target.position == gameObject.transform.position)
        {
            gameObject.GetComponent<ResetGame>().reset = true;
        }

        //Current node the enemy needs to move towards
        Vector3 currentWaypoint = new Vector3(0, 0, 0);

        //If the path exist and is not 0, set current waypoint. Else kill player
        if (path.Length > 0)
        {
            currentWaypoint = path[0];
        }
        else
        {
            gameObject.GetComponent<ResetGame>().reset = true;
            target.GetComponent<PlayerMovement>().enabled = false;
            Destroy(target.gameObject);
        }
        //Loop to move enemy from point to point
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
            //If the player current node moves, calculate a new path for the enemies
            if (grid.NodeFromWorldPosition(path[path.Length - 1], true) != grid.NodeFromWorldPosition(target.position, false))
            {
                    PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
                    yield return null;
            }
            yield return null;
        }
    }

    //This is used to draw the path in the editor. I am leaving it here for future reference.
    //public void OnDrawGizmos()
    //{
    //    if(path != null)
    //    {
    //        for(int i = targetIndex; i < path.Length; i++)
    //        {
    //            Gizmos.color = Color.black;
    //            Gizmos.DrawCube(path[i], new Vector3(0.5f, 0.5f, 0.5f));

    //            if(i == targetIndex)
    //            {
    //                Gizmos.DrawLine(transform.position, path[i]);
    //            }
    //        }
    //    }
    //}

}
