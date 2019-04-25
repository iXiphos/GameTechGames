using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int timesGamePlayed = 1;

    public Text timesPlayedText;

    private void Awake()
    {
        timesPlayedText = GameObject.Find("Text").GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        //DontDestroyOnLoad(timesPlayedText.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        timesPlayedText = GameObject.Find("Text").GetComponent<Text>();
        timesPlayedText.text = "You Have Played through " + timesGamePlayed + " Times!";
    }
}
