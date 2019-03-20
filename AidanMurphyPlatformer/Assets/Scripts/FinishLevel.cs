using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{
    public Text beatLevelText;

    bool reloading = false;

    public GameObject PlayerManager;

    public string reloadingScene;
    public string nextScene;

    public int par;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" || collision.tag == "Ghost")
        {
            beatLevelText.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player" || collision.tag == "Ghost")
        {
            beatLevelText.enabled = false;
            reloading = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player" || collision.tag == "Ghost")
        {
            reloading = true;
        }
    }

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
