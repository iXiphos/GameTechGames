using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class spawnText : MonoBehaviour
{
    public Text txt; //Text To be Spawned

    bool spawned = false;

    //Spawn the Text when the player walks into trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !spawned)
        {
            txt.enabled = true;
        }
    }

    //Clear text and don't display it again when the player leaves the text
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            txt.enabled = false;
            spawned = true;
        }
    }
}
