using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    public string nextScene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //If input is Escape, close game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        //if Input is return, start game
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
        }
    }
}
