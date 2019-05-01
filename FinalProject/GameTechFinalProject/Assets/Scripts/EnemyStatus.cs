using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    public float health = 10; // Enemy Health

    public GameObject manager; //Game manager

    public GameObject drop; //Item Drop

    public AudioSource source; //Audio Source when hit

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

    //Randomly check if an item should be dropped, 10% chance
    private void dropItem()
    {
        int rand = Random.Range(0, 101);
        //If above 90, drop random item
        if (rand > 90)
        {
            rand = Random.Range(0, 2);
            //GameObject upgrade = Instantiate(drop, transform.position, Quaternion.Euler(0, 0, 0));
            //upgrade.GetComponent<UpgradeWeapon>().newWeapon = rand;
            //upgrade.GetComponent<Rigidbody2D>().AddForce(-transform.up * 50);
            Destroy(gameObject);
        }

    }

    
    //Check for collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player") //If collides with player, deal damage and play audio
        {
            manager.GetComponent<GameManager>().RemoveEnemy(gameObject);
            collision.gameObject.GetComponent<PlayerHealth>().doDamage();
            Destroy(gameObject);
        }
    }
}
