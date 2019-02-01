using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int speed;

    Grid grid;

    Node StartNode;

    public GameObject gameManager;

    List<Node> NeighboringNode;

    Vector3 target;

    bool move = true;

    public Transform player;

    public float ShakeAmt = 0;

    private float shakeDuration;

    public float shakeDur;

    public float dampingSpeed;

    Vector3 originalCameraPosition;

    public Camera mainCamera;

    private Vector3 start, end;

    private bool locSet = false;

    public float delay;

    public bool moved = false;

    // Start is called before the first frame update
    void Start()
    {
        originalCameraPosition = mainCamera.transform.position;
        grid = gameManager.GetComponent<Grid>();
        StartCoroutine(wait());
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if ((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) && move) StartCoroutine(Movement());
        if(locSet) Move(start, end);
    }


    void Move(Vector3 currLoc, Vector3 targetLoc)
    {
        if(currLoc != targetLoc)
        gameObject.transform.position = Vector3.MoveTowards(transform.position, targetLoc, Time.deltaTime * speed);
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(1f);
        StartNode = grid.NodeFromWorldPosition(player.position, true);
    }

    IEnumerator Movement()
    {
        StartNode = grid.NodeFromWorldPosition(player.position, true);
        originalCameraPosition = mainCamera.transform.position;
        move = false;
        NeighboringNode = grid.GetNeighboringNodes(StartNode);
        if (Input.GetAxis("Horizontal") > 0 && !NeighboringNode[0].isWall)
        {
            moved = true;
            start = player.position;
            end = NeighboringNode[0].Position;
            locSet = true;
            yield return new WaitForSeconds(delay);
            move = true;
        }
        else if (Input.GetAxis("Horizontal") < 0 && !NeighboringNode[1].isWall)
        {
            moved = true;
            start = player.position;
            end = NeighboringNode[1].Position;
            locSet = true;
            yield return new WaitForSeconds(delay);
            move = true;
        }
        else if (Input.GetAxis("Vertical") > 0 && !NeighboringNode[2].isWall)
        {
            moved = true;
            start = player.position;
            end = NeighboringNode[2].Position;
            locSet = true;
            yield return new WaitForSeconds(delay);
            move = true;
        }
        else if (Input.GetAxis("Vertical") < 0 && !NeighboringNode[3].isWall)
        {
            moved = true;
            start = player.position;
            end = NeighboringNode[3].Position;
            locSet = true;
            yield return new WaitForSeconds(delay);
            move = true;
        }
        else
        {
            move = true;
        }
        yield return new WaitForSeconds(0.0f);
    }

    void Shake()
    {
        if(shakeDuration > 0)
        {
            mainCamera.transform.position = originalCameraPosition + Random.insideUnitSphere * ShakeAmt;
            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0;
        }
    }

}
