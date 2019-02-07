using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject Player;
    public GameObject Enemy;
    public GameObject Enemy2;
    public Vector3 endPoint;

    public string nextScene;

    public Image BlackFade;
    public Text WinText;

    public GameObject coinsCollected;

    // Start is called before the first frame update
    void Start()
    {
        BlackFade.canvasRenderer.SetAlpha(0.0f);
        WinText.canvasRenderer.SetAlpha(0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        //If Escape is pressed, close out of the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        //Check if the player is at the end and all coins have been collected, if they are fade to win screen and disable enemies
        if(Player.transform.position == endPoint && coinsCollected.GetComponent<CoinsCollected>().escape)
        {
            Destroy(Enemy);
            Destroy(Enemy2);
            Player.GetComponent<PlayerMovement>().enabled = false;
            FadeToBlack();
            //Go to next scene
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
            }
        }
    }

    //Fade in the black screen and text
    void FadeToBlack()
    {
        BlackFade.CrossFadeAlpha(1.0f, 0.5f, false);
        WinText.CrossFadeAlpha(1.0f, 0.6f, false);
    }
}
