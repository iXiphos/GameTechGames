using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillGhost : MonoBehaviour
{

    public GameObject PlayerManager;

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            PlayerManager.GetComponent<PlayerManager>().newPlayer = false;
            PlayerManager.GetComponent<PlayerManager>().destroyGhost = true;
        }
    }
}
