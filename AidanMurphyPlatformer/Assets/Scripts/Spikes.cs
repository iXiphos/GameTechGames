using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public LayerMask layer;

    public GameObject playerManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("spike");
            StartCoroutine(playerManager.GetComponent<PlayerManager>().Spawn());
        }
    }
}
