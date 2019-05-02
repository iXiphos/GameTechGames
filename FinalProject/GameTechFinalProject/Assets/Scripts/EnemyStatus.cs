using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    public float health = 10; // Enemy Health

    public GameObject manager; //Game manager

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManager"); //Get game manager
    }

    private void Update()
    {
        if(health <= 0)
        {
            gameObject.GetComponent<Unit>().DestroyEnemy();
        }
    }
   
}
