using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*To DO:
 * World Interactions
 * Reset Level
 * Puzzle ideas
 * Prototype mechanics 
 */
public class PlayerManager : MonoBehaviour
{

    List<PlayerMovements> players; //List of Players Ghosts

    public bool newPlayer = true; //Create New Player

    bool begin = true; //Start the Process of Moving Ghosts

    bool respawn = false; //Respawn Player

    [HideInInspector]
    public int playerTotal = -1; //The Amount of ghosts spawned

    GameObject currPlayer; //The Current Player GameObject

    public GameObject ghostSprite; //The sprite of the ghost

    int i = 0; //Used in for loops

    public GameObject spawnLoc; //Player Spawn Location

    public GameObject player; //Player Prefab

    public List<GameObject> ghosts; //Lists of Active Ghosts

    public bool destroyGhost = false; //Destroy the Ghost

    bool move = true; //Should the Ghosts be moving

    public Text totalGhosts; //Total Number of Ghosts Display

    // Start is called before the first frame update
    void Start()
    {
        ghosts = new List<GameObject>(); //Set Ghosts to new List
        players = new List<PlayerMovements>(); //Set players to new list
        StartCoroutine(Spawn()); //Start Spawn Coroutine
    }

    // Update is called once per frame
    void Update()
    {
        //If Player Presses R and Begin is true, restart the level
        if (Input.GetKeyDown(KeyCode.R) && currPlayer != null && begin)
        {
            begin = false;
            move = false;
            StartCoroutine(Spawn()); //Start Spawn Coroutine
        }

        //If respawn is true, spawn in the ghosts
        if (respawn && playerTotal != 0)
        {
            respawn = false;
            StartCoroutine(spawnGhosts());
        }
    }

    //Spawn Player and Incement Ghosts
    public IEnumerator Spawn()
    {
        Destroy(currPlayer); //Destroy the Current Player
        newPlayer = false; //Set New Player to False

        //Wait Two Frames
        yield return null;
        yield return null;

        //if Ghosts are not 0, destory the ghosts
        if (ghosts.Count != 0)
        {
            for (int j = 0; j < ghosts.Count; j++)
            {
                Destroy(ghosts[j]);
            }
        }

        //Clear the ghosts
        ghosts.Clear();

        //Create new player to track movements and add to players array
        PlayerMovements ghostPlayer = new PlayerMovements();
        players.Add(ghostPlayer);

        //Wait 0.2 seconds then increase ghost total, change text and spawn in new player
        yield return new WaitForSeconds(0.2f);
        playerTotal++;
        totalGhosts.text = "Ghost Used: " + (playerTotal);
        begin = true;
        respawn = true;
        move = true;
        ghosts.Clear();
        yield return new WaitForSeconds(0.5f);
        SpawnNewPlayer();
    }

    //Spawn new player object and start to trace player
    void SpawnNewPlayer()
    {
        GameObject PlayablePlayer = Instantiate(player, spawnLoc.transform.position, player.transform.rotation, transform);
        currPlayer = PlayablePlayer;
        StartCoroutine(tracePlayer());
    }

    //Track players location, sprite and rotation each frame
    IEnumerator tracePlayer()
    {
        //Set new player to true
        newPlayer = true;
        //Track the player until false
        while (newPlayer)
        {
            players[playerTotal].Track(currPlayer);
            yield return null;
        }

        //If Destroy ghost, at this point set destroy to true, else false
        if (destroyGhost)
        {
            players[playerTotal].destroy = true;
        }
        else
        {
            players[playerTotal].destroy = false;
        }

        destroyGhost = false;
        yield return null;
    }

    //Spawn in ghosts
    IEnumerator spawnGhosts()
    {
        //Start ghosts movement 
        for (i = 0; i < playerTotal; i++)
        {
            StartCoroutine(ghostMovements(i));
            yield return new WaitForSeconds(0.02f);
        }
    }

    //Move each ghost along the path at twice the speed
    IEnumerator ghostMovements(int ghostNum)
    {
        yield return null;
        int c = 0;
        //Spawn ghost, give it name and add to ghosts array
        GameObject ghost = Instantiate(ghostSprite, spawnLoc.transform.position, ghostSprite.transform.rotation, transform);
        ghost.name = "ghost" + ghostNum;
        ghosts.Add(ghost);

        //While movement is true retrace the steps, if you reach max, break
        while (move)
        {
            if (players[ghostNum].loc.Count == c) break;
            players[ghostNum].Retrace(ghost);
            c++;
            yield return new WaitForSeconds(0.000001f);
            if (players[ghostNum].loc.Count == c) break;
            players[ghostNum].Retrace(ghost);
            c++;
        }

        //Reset Ghosts, set them to be interactable with the world
        players[ghostNum].ResetDuplicate();
        ghost.GetComponent<BoxCollider2D>().enabled = true;
        ghost.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

        //If Destroy, destroy the ghost
        if (players[ghostNum].destroy)
        {
            Destroy(ghost);
        }
    }

}


//Player Movement class tracks player movements and spits it back out
public class PlayerMovements
{
    public Queue<Vector3> loc = new Queue<Vector3>(); //Queue of Locations
    public Queue<Sprite> sprites = new Queue<Sprite>(); //Queue of Sprites
    public Queue<bool> flips = new Queue<bool>(); //Queue of Bool to represent sprites

    //Duplicate Queues
    Queue<Vector3> newQueue;
    Queue<Sprite> newSpriteQueue;
    Queue<bool> newFlips;

    bool duplicate = true; //Should it duplicate

    public bool destroy; //Should it be destroyed

    //Add information to queues
    public void Track(GameObject player)
    {
        loc.Enqueue(player.transform.position);
        sprites.Enqueue(player.GetComponent<SpriteRenderer>().sprite);
        flips.Enqueue(player.GetComponent<SpriteRenderer>().flipX);
    }
    
    //set gameobject components to queues data
    public void Retrace(GameObject newPlayer)
    {
        //If Duplicate, create duplicate of each queue
        if (duplicate)
        {
            //Trim the Excess Memory
            flips.TrimExcess(); 
            loc.TrimExcess();
            sprites.TrimExcess();

            newQueue = new Queue<Vector3>(loc.ToArray());
            newQueue.TrimExcess();

            newSpriteQueue = new Queue<Sprite>(sprites.ToArray());
            newSpriteQueue.TrimExcess();

            newFlips = new Queue<bool>(flips.ToArray());
            newFlips.TrimExcess();

            duplicate = false;
        }
        //Set Data for the GameObject
        newPlayer.transform.position = newQueue.Dequeue();
        newPlayer.GetComponent<SpriteRenderer>().sprite = newSpriteQueue.Dequeue();
        newPlayer.GetComponent<SpriteRenderer>().flipX = newFlips.Dequeue();
    }

    //Reset Duplicate
    public void ResetDuplicate()
    {
        duplicate = true;
    }

}
