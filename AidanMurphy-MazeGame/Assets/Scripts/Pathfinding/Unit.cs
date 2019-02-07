using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Unit : MonoBehaviour
{
    Grid grid;
    public Transform target;
    public float speed = 5;
    Vector3[] path;
    int targetIndex;

    // Start is called before the first frame update
    void Start()
    {
        PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);    
    }

    // Update is called once per frame
    void Update()
    {
        //PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
        if (target.position == gameObject.transform.position)
        {
            gameObject.GetComponent<ResetGame>().reset = true;
            gameObject.GetComponent<Unit>().enabled = false;
        }
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
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

    IEnumerator FollowPath()
    {
        if (target.position == gameObject.transform.position)
        {
            gameObject.GetComponent<ResetGame>().reset = true;
        }
        Vector3 currentWaypoint = new Vector3(0, 0, 0);
        if (path.Length >= 0)
        {
            try
            {
                currentWaypoint = path[0];
            }
            catch
            {
                gameObject.GetComponent<ResetGame>().reset = true;
                target.GetComponent<PlayerMovement>().enabled = false;
                Destroy(target.gameObject);
            }
        }
        else
        {
            gameObject.GetComponent<ResetGame>().reset = true;
        }
        Debug.Log(path[path.Length - 1]);
        while (true)
        {
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
            if (target.transform.position.x != path[path.Length - 1].x || target.transform.position.y != path[path.Length - 1].y)
            {
                //Debug.Log("Change Position");
                PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
                yield return null;
            }
            yield return null;
        }
    }
    public void OnDrawGizmos()
    {
        if(path != null)
        {
            for(int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], new Vector3(0.5f, 0.5f, 0.5f));

                if(i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
            }
        }
    }

}
