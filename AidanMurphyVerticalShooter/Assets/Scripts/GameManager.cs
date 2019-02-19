using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject enemy;
    public List<GameObject> activeEnemyList;

    public Transform[] enemySpawns;

    private int waveCount = 0;

    public Sprite[] meteorSprites;

    private int enemyCountSmall = 0;
    private int enemyCountMedium = 0;
    private int enemyCountLarge = 0;

    public int WaveDelay;

    private bool waveActive = false;

    public Text waveText;

    private int tempSmall = 0;
    private int tempMedium = 0;
    private int tempLarge = 0;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        waveText.text = "Wave Count: " + (waveCount + 1);
        if (!waveActive)
        {
            StartCoroutine(waveSpawner());
        }
    }

    public void RemoveEnemy(GameObject enemy)
    {
        activeEnemyList.Remove(enemy);
    }

    public void AddEnemy(GameObject enemy)
    {
        activeEnemyList.Add(enemy);
    }

    IEnumerator waveSpawner()
    {
        waveActive = true;
        yield return new WaitForSeconds(WaveDelay);
        if (waveCount == 0)
        {
            enemyCountSmall = 6;
            enemyCountMedium = 0;
            enemyCountLarge = 0;
        }
        else if (waveCount == 1)
        {
            enemyCountSmall = 8;
            enemyCountMedium = 1;
            enemyCountLarge = 0;
        }
        else if(waveCount == 2)
        {
            enemyCountSmall = 10;
            enemyCountMedium = 1;
            enemyCountLarge = 0;
        }
        else if(waveCount == 3)
        {
            enemyCountSmall = 12;
            enemyCountMedium = 2;
            enemyCountLarge = 0;
        }
        else if (waveCount == 4)
        {
            enemyCountSmall = 6;
            enemyCountMedium = 2;
            enemyCountLarge = 1;
        }
        else if (waveCount == 5)
        {
            enemyCountSmall = 8;
            enemyCountMedium = 3;
            enemyCountLarge = 1;
        }
        else if (waveCount % 6 == 0)
        {
            enemyCountSmall = 10;
            enemyCountMedium = 3;
            enemyCountLarge = 1;
        }
        else if (waveCount % 6 == 1)
        {
            enemyCountSmall = tempSmall + 2;
            enemyCountMedium = tempMedium + 1;
            enemyCountLarge = tempLarge + 0;
        }
        else if (waveCount % 6 == 2)
        {
            enemyCountSmall = tempSmall - 4;
            enemyCountMedium = tempMedium + 0;
            enemyCountLarge = tempLarge + 1;
        }
        else if (waveCount % 6 == 3)
        {
            enemyCountSmall = tempSmall + 2;
            enemyCountMedium = tempMedium + 1;
            enemyCountLarge = tempLarge + 0;
        }
        else if (waveCount % 6 == 4)
        {
            enemyCountSmall = tempSmall + 2;
            enemyCountMedium = tempMedium + 0;
            enemyCountLarge = tempLarge + 0;
        }
        else if (waveCount % 6 == 5)
        {
            enemyCountSmall = tempSmall + -4;
            enemyCountMedium = tempMedium + 1;
            enemyCountLarge = tempLarge + 1;
        }

        tempSmall = enemyCountSmall;
        tempMedium = enemyCountMedium;
        tempLarge = enemyCountLarge;

        float waitTime = 0;
        int i = 0;
        while (true)
        {
            
            while(i < (tempSmall + tempMedium + tempLarge))
            {
                int rand = Random.Range(0, enemySpawns.Length);
                int rand2 = Random.Range(0, 3);
                int rand3 = Random.Range(0, 4);
                if(rand2 == 0 && enemyCountSmall != 0)
                {
                    GameObject meteorite = Instantiate(enemy, enemySpawns[rand].position, Quaternion.Euler(0, 0, 0));
                    meteorite.GetComponent<SpriteRenderer>().sprite = meteorSprites[rand3];
                    meteorite.name = "meterorite" + enemyCountSmall;
                    meteorite.GetComponent<EnemyStatus>().size = 0;
                    meteorite.GetComponent<EnemyStatus>().manager = gameObject;
                    activeEnemyList.Add(meteorite);
                    waitTime = .75f;
                    enemyCountSmall--;
                }
                else if (rand2 == 1 && enemyCountMedium != 0)
                {
                    GameObject meteorite = Instantiate(enemy, enemySpawns[rand].position, Quaternion.Euler(0, 0, 0));
                    meteorite.GetComponent<SpriteRenderer>().sprite = meteorSprites[rand3];
                    meteorite.name = "meterorite" + enemyCountMedium;
                    meteorite.GetComponent<EnemyStatus>().manager = gameObject;
                    meteorite.GetComponent<EnemyStatus>().size = 1;
                    activeEnemyList.Add(meteorite);
                    waitTime = 1.25f;
                    enemyCountMedium--;
                }
                else if (rand2 == 2 && enemyCountLarge != 0)
                {
                    GameObject meteorite = Instantiate(enemy, enemySpawns[rand].position, Quaternion.Euler(0, 0, 0));
                    meteorite.GetComponent<SpriteRenderer>().sprite = meteorSprites[rand3];
                    meteorite.name = "meterorite" + enemyCountMedium;
                    meteorite.GetComponent<EnemyStatus>().manager = gameObject;
                    meteorite.GetComponent<EnemyStatus>().size = 2;
                    activeEnemyList.Add(meteorite);
                    waitTime = 1.5f;
                    enemyCountLarge--;
                }
                else
                {
                    continue;
                }
                i++;
                yield return new WaitForSeconds(waitTime);
            }
            if(activeEnemyList.Count == 0)
            {
                break;
            }
            yield return null;
        }
        waveActive = false;
        waveCount++;
    }
}
