using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsCollected : MonoBehaviour
{
    public int coinsCollected = 0; //Track how many coins have been picked up
    public int scene = 0; //Track the current scene

    public bool escape; //Have all the coins been collected

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Text>().text = "0/1 Coins Collected";
        coinsCollected = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Check what scene this is in and Change UI accordingly
        if (scene == 0)
        {
            //Display how many coins have been collected
            if (coinsCollected == 0)
            {
                gameObject.GetComponent<Text>().text = "0/1 Coins Collected";
            }
            else if(coinsCollected == 1)
            {
                gameObject.GetComponent<Text>().text = "1/1 Coins Collected";
                escape = true;
            }
        }
        else if(scene == 1)
        {
            //Display how many coins have been collected
            if (coinsCollected == 0)
            {
                gameObject.GetComponent<Text>().text = "0/2 Coins Collected";
            }
            else if (coinsCollected == 1)
            {
                gameObject.GetComponent<Text>().text = "1/2 Coins Collected";
            }
            else if (coinsCollected == 1)
            {
                gameObject.GetComponent<Text>().text = "2/2 Coins Collected";
                escape = true;
            }
        }
    }
}
