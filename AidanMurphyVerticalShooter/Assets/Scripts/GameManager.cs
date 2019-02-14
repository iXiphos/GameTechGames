using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject[] enemyArray;
    public List<GameObject> activeEnemyList;

    public Transform[] enemySpawns;

    private int waveCount = 0;

    public int enemyCount = 0;

    public int WaveDelay;

    private bool waveActive = false;

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
        if (!waveActive)
        {
            StartCoroutine(waveSpawner());
        }
    }

    void RemoveEnemy(GameObject enemy)
    {
        activeEnemyList.Remove(enemy);
    }

    IEnumerator waveSpawner()
    {
        waveActive = false;
        yield return new WaitForSeconds(WaveDelay);
        while (true)
        {
            for(int i = 0; i < enemyCount; i++)
            {
                int rand = Random.Range(0, 4);
                GameObject enemy = Instantiate(enemyArray[0], enemySpawns[rand].position, Quaternion.Euler(0, 0, 0));
                activeEnemyList.Add(enemy);
                yield return null;
            }
            if(activeEnemyList.Count == 0)
            {
                break;
            }
            yield return null;
        }
        waveActive = true;
        waveCount++;
    }
}
