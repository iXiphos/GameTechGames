using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{

    public int nextScene;

    public GameObject gameManager;

    private void Start()
    {
        if (!GameObject.Find("GameManager(Clone)"))
        {
            Instantiate(gameManager);
        }
        else
        {
            gameManager = GameObject.Find("GameManager(Clone)");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && nextScene != 2)
        {
            goToNextScene();
        }
    }

    public void goToNextScene()
    {
        if(nextScene == 0)
        {
            gameManager.GetComponent<GameManager>().timesGamePlayed++;
        }
        SceneManager.LoadScene(nextScene);
    }
}
