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

    // Start is called before the first frame update
    void Start()
    {
        grid = GridManager.GetComponent<Grid>();
    }


    void Update()
    {
        if((TurnManager.GetComponent<TurnManager>().TurnCount % 2) != PlayerNumber)
        {
            Move = true;
            StartCoroutine(MovePlayer());
        }
        else if(!Move)
        {
            StopCoroutine(MovePlayer());
        }
    }

    //If a path is found by PathFinding, start to move across path
    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        //If the path doesn't exist, reset the game
        if (pathSuccessful && newPath.Length <= MoveAmount)
        {
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

    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], new Vector3(0.5f, 0.5f, 0.5f));

                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
            }
        }
    }
}
