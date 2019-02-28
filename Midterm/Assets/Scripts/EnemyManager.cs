using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public GameObject enemyPrefab; //The Enemy That will be spawned

    int numEnemies = 5; //How many enemies to spawn

    public float xRange; //Range between were enemies can spawn in X directions
    public float yRangeUp; //Range between were enemies can spawn in Y max
    public float yRangeDown; //Range between were enemies can spawn in Y min

    public int enemiesAlive = 0; //How many enemies are alive

    public int Score = 0; //Current Score of Player

    int currWave = 0; //The current Wave the player is on

    public float EnemyDelay; //How long between each enemy drop

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemies()); //Spawn First set of enemies

    }

    // Update is called once per frame
    void Update()
    {
        //If there are no enemies alive spawn a new wave of them
        if(enemiesAlive == 0)
        {
            StartCoroutine(spawnEnemies());
        }
    }

    //Spawn New Set of Enemies
    IEnumerator spawnEnemies() {

        currWave++; //Increase Wave count

        //Every 5 waves spawn one more enemy until it reaches 10
        if (currWave % 5 == 0 && numEnemies != 10)
        {
            numEnemies++;
        }

        //Loop through and spawn number of enemies
        for(int i = 0; i < numEnemies; i++)
        {
            float randSize = Random.Range(0.75f, 1.75f); //Randomize the size of the enemy
            float randX = Random.Range(-xRange, xRange); //Get X spawn location
            float randY = Random.Range(yRangeUp, yRangeDown); //Get y spawn Location
            GameObject enemy = Instantiate(enemyPrefab, new Vector2(randX, randY), transform.rotation); //Spawn Enemy
            enemy.name = "Enemy" + i; //Give enemy unique name
            enemy.GetComponent<Enemy>().manager = gameObject; //Set manager in enemy script to this
            enemy.transform.localScale = new Vector2(randSize, randSize); //Set the scale to be random
            enemiesAlive++; //Increase enemies alive
            yield return new WaitForSeconds(EnemyDelay); //Wait a frame then spawn new enemies
        }
    }

}
