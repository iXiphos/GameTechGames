using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The way my movement works, was causing on trigger enter to break. I had to make my own collision using the system I was using
public class Collectable : MonoBehaviour
{
    Grid grid;

    Node StartNode;

    Node PlayerNode;

    public GameObject player;

    public GameObject Enemy;

    public GameObject Wall;

    public GameObject gameManager;

    public bool openExit;


    // Start is called before the first frame update
    void Start()
    {
        grid = gameManager.GetComponent<Grid>();
    }

    // Update is called once per frame
    void Update()
    {
        StartNode = grid.NodeFromWorldPosition(transform.position, true);
        PlayerNode = grid.NodeFromWorldPosition(player.transform.position, true);
        transform.position = StartNode.Position;
        if (PlayerNode == StartNode)
        {
            openExit = true;
            gameObject.SetActive(false);
        }

        if (openExit)
        {
            UnlockExit();
        }

    }

    void UnlockExit()
    {
        Enemy.SetActive(true);
        Destroy(Wall);
    }

}
