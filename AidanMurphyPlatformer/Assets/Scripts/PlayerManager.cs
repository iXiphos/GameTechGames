using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*To DO:
 * World Interactions
 * Reset Level
 * Puzzle ideas
 * Prototype mechanics 
 */
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

    bool move = true;

    // Start is called before the first frame update
    void Start()
    {
        ghosts = new List<GameObject>();
        players = new List<PlayerMovements>();
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && currPlayer != null && begin)
        {
            begin = false;
            move = false;
            StartCoroutine(Spawn());
        }

        if (respawn && playerTotal != 0)
        {
            respawn = false;
            StartCoroutine(spawnGhosts());
        }
    }

    IEnumerator Spawn()
    {
        Destroy(currPlayer);
        newPlayer = false;
        yield return null;
        yield return null;
        if (ghosts.Count != 0)
        {
            for (int j = 0; j < ghosts.Count; j++)
            {
                Destroy(ghosts[j]);
            }
        }
        ghosts.Clear();
        PlayerMovements ghostPlayer = new PlayerMovements();
        players.Add(ghostPlayer);
        yield return new WaitForSeconds(0.2f);
        playerTotal++;
        begin = true;
        respawn = true;
        move = true;
        ghosts.Clear();
        yield return new WaitForSeconds(0.5f);
        SpawnNewPlayer();
    }

    void SpawnNewPlayer()
    {
        GameObject PlayablePlayer = Instantiate(player, spawnLoc.transform.position, player.transform.rotation, transform);
        currPlayer = PlayablePlayer;
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

    IEnumerator spawnGhosts()
    {
        for (i = 0; i < playerTotal; i++)
        {
            StartCoroutine(ghostMovements(i));
            yield return new WaitForSeconds(0.02f);
        }
    }

    IEnumerator ghostMovements(int ghostNum)
    {
        yield return null;
        int c = 0;
        GameObject ghost = Instantiate(ghostSprite, spawnLoc.transform.position, ghostSprite.transform.rotation, transform);
        ghost.name = "ghost" + ghostNum;
        ghosts.Add(ghost);
        yield return null;
        while (move)
        {
            if (players[ghostNum].loc.Count == c) break;
            players[ghostNum].Retrace(ghost);
            c++;
            yield return new WaitForSeconds(0.000001f);
        }
        players[ghostNum].ResetDuplicate();
        ghost.GetComponent<BoxCollider2D>().enabled = true;
        //ghost.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }

}

public class PlayerMovements
{
    public Queue<Vector3> loc = new Queue<Vector3>();
    public Queue<Sprite> sprites = new Queue<Sprite>();

    Queue<Vector3> newQueue;
    Queue<Sprite> newSpriteQueue;

    bool duplicate = true;

    public void Track(GameObject player)
    {
        loc.Enqueue(player.transform.position);
        sprites.Enqueue(player.GetComponent<SpriteRenderer>().sprite);
    }
    
    public void Retrace(GameObject newPlayer)
    {
        if (duplicate)
        {
            loc.TrimExcess();
            sprites.TrimExcess();
            newQueue = new Queue<Vector3>(loc.ToArray());
            newQueue.TrimExcess();
            newSpriteQueue = new Queue<Sprite>(sprites.ToArray());
            newSpriteQueue.TrimExcess();
            duplicate = false;
        }
        newPlayer.transform.position = newQueue.Dequeue();
        newPlayer.GetComponent<SpriteRenderer>().sprite = newSpriteQueue.Dequeue();
    }

    public void ResetDuplicate()
    {
        duplicate = true;
    }

}
