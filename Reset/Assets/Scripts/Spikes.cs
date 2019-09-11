using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public GameObject playerManager; //Player Manager

    //When the player enters the spike, auto reset the player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(playerManager.GetComponent<PlayerManager>().Spawn());
        }
    }
}
