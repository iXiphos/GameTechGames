using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public GameObject manager;

    private void OnMouseDown()
    {
        manager.GetComponent<EnemyManager>().Score += 10;
        manager.GetComponent<EnemyManager>().enemiesAlive--;
        Destroy(gameObject);
    }
}
