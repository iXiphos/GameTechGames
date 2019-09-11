using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadScore : MonoBehaviour
{

    public GameObject manager;

    public Text score;

    // Start is called before the first frame update
    void Start()
    {
        //Load the Final Score of the player
        score = gameObject.GetComponent<Text>();
        manager = GameObject.Find("GameManager");
        score.text = "You Lasted " + manager.GetComponent<GameManager>().waveCount + " Waves\n Press Space to Restart";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
