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

    // Start is called before the first frame update
    void Start()
    {
        originalCameraPosition = mainCamera.transform.position;
        grid = gameManager.GetComponent<Grid>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        StartNode = grid.NodeFromWorldPosition(player.position, true);
        if ((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) && move) StartCoroutine(Movement());
        Shake();
    }

    IEnumerator Movement()
    {
        originalCameraPosition = mainCamera.transform.position;
        move = false;
        NeighboringNode = grid.GetNeighboringNodes(StartNode);
        if (Input.GetAxis("Horizontal") > 0 && NeighboringNode[0].isWall)
        {
            player.position = NeighboringNode[0].Position;
            move = false;
            yield return new WaitForSeconds(0.05f);
            shakeDuration = shakeDur;
            yield return new WaitForSeconds(0.35f);
            move = true;
        }
        else if (Input.GetAxis("Horizontal") < 0 && NeighboringNode[1].isWall)
        {
            transform.position = NeighboringNode[1].Position;
            move = false;
            yield return new WaitForSeconds(0.05f);
            shakeDuration = shakeDur;
            yield return new WaitForSeconds(0.35f);
            move = true;
        }
        else if (Input.GetAxis("Vertical") > 0 && NeighboringNode[2].isWall)
        {
            transform.position = NeighboringNode[2].Position;
            move = false;
            yield return new WaitForSeconds(0.05f);
            shakeDuration = shakeDur;
            yield return new WaitForSeconds(0.35f);
            move = true;
        }
        else if (Input.GetAxis("Vertical") < 0 && NeighboringNode[3].isWall)
        {
            transform.position = NeighboringNode[3].Position;
            move = false;
            yield return new WaitForSeconds(0.05f);
            shakeDuration = shakeDur;
            yield return new WaitForSeconds(0.35f);
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
