using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; //GameManager Instance
    public GameObject enemy; //Enemy to be spawned
    public List<GameObject> activeEnemyList; //Active enemies

    public int waveCount = 0;

    private int waveSize = 1; //Current Wave Count

    public int WaveDelay; //Delay between each wave

    public float enemyDelay;

    private bool dead = false; //Is player dead

    private bool waveActive = false;

    public List<Vector3> spawnLocations;

    public Image BlackFade; //Fade to black
    public Text loseText; //Text When player loses

    private void Awake()
    {
        BlackFade.canvasRenderer.SetAlpha(0.0f); //Set alpha to zero for fade to black
        //loseText.canvasRenderer.SetAlpha(0.0f);
        instance = this; //Set up instance
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (loseText != null) loseText.text = "Wave: " + (waveCount + 1);
        if (!waveActive) //Is there a wave currently active
        {
            StartCoroutine(waveSpawner()); //Start Wave Spawner
        }

        if (dead) //Is player dead
        {
            StartCoroutine(FadeToBlack()); //Fade in black screen
        }
    }

    //Know if player is dead
    public void playerDead()
    {
        waveActive = true;
        StopAllCoroutines();
        dead = true;
    }

    //Fade in the black screen and text
    IEnumerator FadeToBlack()
    {
        dead = false;
        BlackFade.CrossFadeAlpha(1.0f, 0.4f, false);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(2);
        Destroy(gameObject, 1f);
    }

    //Remove enemy from active enemy list
    public void RemoveEnemy(GameObject enemy)
    {
        activeEnemyList.Remove(enemy);
    }

    //Add enemy to active enemy list
    public void AddEnemy(GameObject enemy)
    {
        activeEnemyList.Add(enemy);
    }

    IEnumerator waveSpawner()
    {
        waveActive = true;
        int k = 0;
        while (true)
        {
            if (k < waveSize)
            {
                for (int i = 0; i < waveSize; i++)
                {
                    int rand = Random.Range(0, spawnLocations.Count);
                    GameObject enemies = Instantiate(enemy, spawnLocations[rand], transform.rotation);
                    enemies.GetComponent<EnemyStatus>().manager = gameObject;
                    enemies.GetComponent<Unit>().manager = gameObject;
                    activeEnemyList.Add(enemies);
                    k++;
                    yield return new WaitForSeconds(enemyDelay);
                }
            }
            if (activeEnemyList.Count == 0) //If all enemies are dead break
            {
                if(enemyDelay < 0.2)
                    enemyDelay -= 0.05f;
                waveSize++;
                waveActive = false;
                break;
            }
            yield return null;
        }
        yield return new WaitForSeconds(WaveDelay);
        waveCount++;
    }
}
