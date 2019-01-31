using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour
{

    public bool reset;

    // Start is called before the first frame update
    void Start()
    {
        reset = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (reset)
        {
            Debug.Log("gg");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
