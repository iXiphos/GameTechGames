using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    List<PlayerMovements> players;

    bool newPlayer = true;

    bool begin = true;

    bool respawn = false;

    int playerTotal = -1;

    GameObject currPlayer;

    public GameObject ghostSprite;

    int i = 0;

    public GameObject spawnLoc;

    public GameObject player;

    public List<GameObject> ghosts;

    // Start is called before the first frame update
    void Start()
    {
        ghosts = new List<GameObject>();
        players = new List<PlayerMovements>();
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y) && currPlayer.transform.position != spawnLoc.transform.position)
        {
            Spawn();
        }

        if (respawn && playerTotal != 0)
        {
            ghosts.Clear();
            respawn = false;
            for (i = 0; i < playerTotal; i++)
            {
                StartCoroutine(ghostMovements(i));
            }
        }
    }

    void Spawn()
    {
        playerTotal++;
        Destroy(currPlayer);
        newPlayer = false;
        GameObject PlayablePlayer = Instantiate(player, spawnLoc.transform.position, player.transform.rotation, transform);
        currPlayer = PlayablePlayer;
        if(ghosts.Count != 0)
            for(int j = 0; j < ghosts.Count; j++)
            {
                Destroy(ghosts[j]);
            }
        PlayerMovements ghostPlayer = new PlayerMovements();
        players.Add(ghostPlayer);
        respawn = true;
        StopAllCoroutines();
        StartCoroutine(tracePlayer());
    }

    IEnumerator tracePlayer()
    {
        newPlayer = true;
        while (newPlayer)
        {
            players[playerTotal].Track(currPlayer);
            yield return null;
        }
        yield return null;
    }

    IEnumerator ghostMovements(int ghostNum)
    {
        int c = 0;
        GameObject ghost = Instantiate(ghostSprite, spawnLoc.transform.position, ghostSprite.transform.rotation, transform);
        ghost.name = "ghost" + ghostNum;
        ghosts.Add(ghost);
        Debug.Log(players[ghostNum].loc.Count);
        while (true)
        {
            if (players[ghostNum].loc.Count == c) break;
            players[ghostNum].Retrace(ghost);
            c++;
            yield return null;
        }
        yield return null;
    }

}

public class PlayerMovements
{
    public Queue<Vector3> loc = new Queue<Vector3>();
    public Queue<Sprite> sprites = new Queue<Sprite>();

    public void Track(GameObject player)
    {
        loc.Enqueue(player.transform.position);
        sprites.Enqueue(player.GetComponent<SpriteRenderer>().sprite);
    }
    
    public void Retrace(GameObject newPlayer)
    {
        Vector3 duplicateLoc = loc.Dequeue();
        Sprite duplicateSprite = sprites.Dequeue();
        newPlayer.transform.position = duplicateLoc;
        newPlayer.GetComponent<SpriteRenderer>().sprite = duplicateSprite;
        loc.Enqueue(duplicateLoc);
        sprites.Enqueue(duplicateSprite);
    }

}
