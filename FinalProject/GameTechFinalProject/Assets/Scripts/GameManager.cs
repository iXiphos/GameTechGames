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

    private int waveCount = 0;

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
        //BlackFade.canvasRenderer.SetAlpha(0.0f); //Set alpha to zero for fade to black
        //loseText.canvasRenderer.SetAlpha(0.0f); //Set alpha to zero for lose text
        instance = this; //Set up instance
    }

    // Update is called once per frame
    void Update()
    {
        if (!waveActive) //Is there a wave currently active
        {
            StartCoroutine(waveSpawner()); //Start Wave Spawner
        }

        if (dead) //Is player dead
        {
            if(waveCount == 0) loseText.text = "You Survived " + (waveCount + 1) + " Wave!" + "\n" + "Press Enter to Try Again"; //Show user how many waves they survived
            else loseText.text = "You Survived " + (waveCount + 1) + " Waves!" + "\n" + "Press Enter to Try Again"; //Show user how many waves they survived
            FadeToBlack(); //Fade in black screen
            //Go to next scene
            if (Input.GetKeyDown(KeyCode.Return)) //If player presses enter, reset scene
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
            }
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
    void FadeToBlack()
    {
        BlackFade.CrossFadeAlpha(1.0f, 0.4f, false);
        loseText.CrossFadeAlpha(1.0f, 0.5f, false);
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
        waveSize++;
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
    }
}
