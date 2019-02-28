using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public GameObject enemyPrefab;

    public Sprite[] sprites;

    int numEnemies = 5;

    public float xRange;
    public float yRangeUp;
    public float yRangeDown;

    public int enemiesAlive = 0;

    public int Score = 0;

    int currWave = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemies());

    }

    // Update is called once per frame
    void Update()
    {
        if(enemiesAlive == 0)
        {
            StartCoroutine(spawnEnemies());
        }
    }

    IEnumerator spawnEnemies() {

        if(currWave % 5 == 0)
        {
            numEnemies++;
        }

        for(int i = 0; i < numEnemies; i++)
        {
            float randSize = Random.Range(0.75f, 2f);
            float randX = Random.Range(-xRange, xRange);
            float randY = Random.Range(yRangeUp, yRangeDown);
            GameObject enemy = Instantiate(enemyPrefab, new Vector2(randX, randY), transform.rotation);
            enemy.name = "Enemy" + i;
            enemy.GetComponent<Enemy>().manager = gameObject;
            enemy.transform.localScale = new Vector2(randSize, randSize);
            enemiesAlive++;
            yield return null;
        }

        currWave++;

    }

}
