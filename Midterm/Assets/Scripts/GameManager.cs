using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject EnemyManager; //Enemy Manager

    public Text scoreText; //The Text for Score

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "CurrentScore: " + EnemyManager.GetComponent<EnemyManager>().Score; //Diplay the Players Current Score
    }
}
