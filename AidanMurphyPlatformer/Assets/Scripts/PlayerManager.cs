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
        if (Input.GetKeyDown(KeyCode.Y) && currPlayer != null && begin)
        {
            begin = false;
            StopAllCoroutines();
            StartCoroutine(Spawn());
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

    IEnumerator Spawn()
    {
        playerTotal++;
        Destroy(currPlayer);
        newPlayer = false;
        if(ghosts.Count != 0)
            for(int j = 0; j < ghosts.Count; j++)
            {
                Destroy(ghosts[j]);
            }
        PlayerMovements ghostPlayer = new PlayerMovements();
        players.Add(ghostPlayer);
        respawn = true;
        yield return new WaitForSeconds(0.5f);
        SpawnNewPlayer();
        yield return new WaitForSeconds(0.5f);
        begin = true;
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

    IEnumerator ghostMovements(int ghostNum)
    {
        int c = 0;
        GameObject ghost = Instantiate(ghostSprite, spawnLoc.transform.position, ghostSprite.transform.rotation, transform);
        ghost.name = "ghost" + ghostNum;
        ghosts.Add(ghost);
        while (true)
        {
            if (players[ghostNum].loc.Count == c) break;
            players[ghostNum].Retrace(ghost);
            c++;
            yield return null;
        }
        ghost.GetComponent<BoxCollider2D>().enabled = true;
        ghost.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
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
