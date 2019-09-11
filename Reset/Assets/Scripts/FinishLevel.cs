using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{
    public Text beatLevelText; //Text Element to Display when flag is reached

    bool reloading = false; //Is it ok to go to next level

    public GameObject PlayerManager; //Player Manager Script

    public string reloadingScene; //Scene to be reset
    public string nextScene; //Next Scene to go To

    public int par; //How many ghosts should this take
    
    //If Player on trigger, display the beat level text
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" || collision.tag == "Ghost")
        {
            beatLevelText.enabled = true;
        }
    }

    //When Player leaves the trigger, hide the beat level text and don't let them go to next level
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player" || collision.tag == "Ghost")
        {
            beatLevelText.enabled = false;
            reloading = false;
        }
    }
    
    //While player is in the trigger, they can go to next level
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player" || collision.tag == "Ghost")
        {
            reloading = true;
        }
    }

    //Display Text based on par score, if escape is pressed reset the scene
    //If return is pressed on the flag, it goes to the next scene
    void Update()
    {
        if(PlayerManager.GetComponent<PlayerManager>().playerTotal < par)
        {
            beatLevelText.text = "You Beat it, " + (par - PlayerManager.GetComponent<PlayerManager>().playerTotal) + " Under Par!";
        }
        else if(PlayerManager.GetComponent<PlayerManager>().playerTotal > par)
        {
            beatLevelText.text = "You Beat it, " + (PlayerManager.GetComponent<PlayerManager>().playerTotal - par) + " Over Par!";
        }
        else
        {
            beatLevelText.text = "You Beat it on Par!";
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(reloadingScene);
        }

        if (Input.GetKeyDown(KeyCode.Return) && reloading)
        {
            SceneManager.LoadScene(nextScene);
        }

    }

}
