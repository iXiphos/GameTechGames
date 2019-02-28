using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public GameObject manager; //The Enemy Manager

    //If Clicked On
    private void OnMouseDown()
    {
        manager.GetComponent<EnemyManager>().Score += 10; //Increase Score
        manager.GetComponent<EnemyManager>().enemiesAlive--; //Decrease Enemies Alive
        Destroy(gameObject); //Destory Enemies
    }
}
