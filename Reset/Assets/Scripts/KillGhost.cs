using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillGhost : MonoBehaviour
{
    public GameObject PlayerManager; //Player Manager

    void OnCollisionEnter2D(Collision2D col)
    {
        //If the colision is player, destroy the ghost tracking
        if(col.gameObject.tag == "Player")
        {
            PlayerManager.GetComponent<PlayerManager>().newPlayer = false;
            PlayerManager.GetComponent<PlayerManager>().destroyGhost = true;
        }
    }
}
