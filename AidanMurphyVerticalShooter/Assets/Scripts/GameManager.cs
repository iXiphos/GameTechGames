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

    private int waveCount = 0; //Current Wave Count

    public Sprite[] meteorSprites; //Potential Meteor Sprites

    private int enemyCountSmall = 0; //How many small meteors
    private int enemyCountMedium = 0; //How many medium meteors
    private int enemyCountLarge = 0; //How many large meteors

    public int WaveDelay; //Delay between each wave

    private bool dead = false; //Is player dead

    private bool waveActive = false;

    public Text waveText;

    private int tempSmall = 0; //Previous Wave amount of small
    private int tempMedium = 0; //Previous Wave amount of medium
    private int tempLarge = 0; //Previous Wave amount of large

    public Image BlackFade; //Fade to black
    public Text loseText; //Text When player loses

    private void Awake()
    {
        BlackFade.canvasRenderer.SetAlpha(0.0f); //Set alpha to zero for fade to black
        loseText.canvasRenderer.SetAlpha(0.0f); //Set alpha to zero for lose text
        instance = this; //Set up instance
    }

    // Update is called once per frame
    void Update()
    {
        waveText.text = "Wave Count: " + (waveCount + 1); //Display Wave Text
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

    //Wave Spawner, spawn set amount of enemies each wave and after wave 6, begin to increment
    IEnumerator waveSpawner()
    {
        waveActive = true;
        yield return new WaitForSeconds(WaveDelay);
        //Amount of enemies needed to be spawned each wave
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

        //Store the wave values to be used by the next wave
        tempSmall = enemyCountSmall;
        tempMedium = enemyCountMedium;
        tempLarge = enemyCountLarge;

        //How needed to wait for enemies to spawn 
        float waitTime = 0;
        int i = 0;
        //While the wave spawner is still active
        while (true)
        {
            //While enemies still need to be spawned
            while(i < (tempSmall + tempMedium + tempLarge))
            {
                int rand2 = Random.Range(0, 3); //What size should be spawned
                int rand3 = Random.Range(0, 4); //Whate sprite should it have
                Vector3 spawnLoc = new Vector3(Random.Range(-4f, 4f), 11.5f, 0f); //Random Spawn location
                if (rand2 == 0 && enemyCountSmall != 0) //If enemy should be small, and small still needs to be spawned
                {
                    GameObject meteorite = Instantiate(enemy, spawnLoc, Quaternion.Euler(0, 0, 0));
                    meteorite.GetComponent<SpriteRenderer>().sprite = meteorSprites[rand3];
                    meteorite.name = "meterorite" + enemyCountSmall;
                    meteorite.GetComponent<EnemyStatus>().size = 0;
                    meteorite.GetComponent<EnemyStatus>().manager = gameObject;
                    activeEnemyList.Add(meteorite);
                    waitTime = .75f;
                    enemyCountSmall--;
                }
                else if (rand2 == 1 && enemyCountMedium != 0) //If enemy should be medium, and medium still needs to be spawned
                {
                    GameObject meteorite = Instantiate(enemy, spawnLoc, Quaternion.Euler(0, 0, 0));
                    meteorite.GetComponent<SpriteRenderer>().sprite = meteorSprites[rand3];
                    meteorite.name = "meterorite" + enemyCountMedium;
                    meteorite.GetComponent<EnemyStatus>().manager = gameObject;
                    meteorite.GetComponent<EnemyStatus>().size = 1;
                    activeEnemyList.Add(meteorite);
                    waitTime = 1.25f;
                    enemyCountMedium--;
                }
                else if (rand2 == 2 && enemyCountLarge != 0) //If enemy should be large, and large still needs to be spawned
                {
                    GameObject meteorite = Instantiate(enemy, spawnLoc, Quaternion.Euler(0, 0, 0));
                    meteorite.GetComponent<SpriteRenderer>().sprite = meteorSprites[rand3];
                    meteorite.name = "meterorite" + enemyCountMedium;
                    meteorite.GetComponent<EnemyStatus>().manager = gameObject;
                    meteorite.GetComponent<EnemyStatus>().size = 2;
                    activeEnemyList.Add(meteorite);
                    waitTime = 1.5f;
                    enemyCountLarge--;
                }
                else //Try again
                {
                    continue;
                }
                i++; //Increase I
                yield return new WaitForSeconds(waitTime); //Wait amount for next enemy
            }
            if(activeEnemyList.Count == 0) //If all enemies are dead break
            {
                break;
            }
            yield return null;
        }
        waveActive = false; //Set wave active to false, so it starts the next wave
        waveCount++; //Increase Wave count
    }
}
