using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    public float health = 10; // Enemy Health

    public int size = 0; //What size meteor is it

    float speed = 100; //Meteorite speed

    float boundsLeft = -5.1f; //How far left the meteor can go
    float boundsRight = 5.1f; //How far right the meteor can go
    float boundsDown = -11f; //How far down until meteor is destroyed

    public GameObject manager; //Game manager

    public GameObject drop; //Item Drop

    public AudioSource source; //Audio Source when hit

    public GameObject particleSystem; //Explosion Particle effect

    // Start is called before the first frame update
    void Start()
    {
        source = GameObject.Find("BulletSpawn").GetComponent<AudioSource>(); //Get Audio Source
        manager = GameObject.Find("GameManager"); //Get game manager
        //What size is meteor and set the values accordindly
        switch (size)
        {
            case 0: //Small Meteor
                gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
                health = 6f;
                speed = 300;
                break;
            case 1: //Medium Meteor
                gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1f);
                health = 10f;
                speed = 250;
                break;
            case 2: //Large Meteor
                gameObject.transform.localScale = new Vector3(2f, 2f, 1f);
                health = 14f;
                speed = 200;
                break;
        }
        gameObject.GetComponent<Rigidbody2D>().AddForce(-transform.up * speed); //Get meteor moving
    }

    // Update is called once per frame
    void Update()
    {
        //If the health is 0, destory the meteor or split the meteor
        if (health <= 0)
        {
            //What sprite the meteorite should be
            int rand3 = Random.Range(0, 4);
            //If Medium Size split into two smaller ones
            if (size == 1)
            {
                StartCoroutine(SpawnParticles());
                GameObject enemy = Instantiate(gameObject, new Vector3(gameObject.transform.position.x + 1, gameObject.transform.position.y - 0.5f), Quaternion.Euler(0, 0, 15));
                rand3 = Random.Range(0, 4);
                enemy.GetComponent<SpriteRenderer>().sprite = manager.GetComponent<GameManager>().meteorSprites[rand3];
                manager.GetComponent<GameManager>().AddEnemy(enemy);
                enemy.GetComponent<EnemyStatus>().size = 0;
                GameObject enemy1 = Instantiate(gameObject, new Vector3(gameObject.transform.position.x - 1f, gameObject.transform.position.y - 0.5f), Quaternion.Euler(0, 0, -15));
                rand3 = Random.Range(0, 4);
                enemy1.GetComponent<SpriteRenderer>().sprite = manager.GetComponent<GameManager>().meteorSprites[rand3];
                manager.GetComponent<GameManager>().AddEnemy(enemy1);
                enemy1.GetComponent<EnemyStatus>().size = 0;
            }
            else if(size == 2) //If large split into two medium ones
            {
                StartCoroutine(SpawnParticles());
                GameObject enemy = Instantiate(gameObject, new Vector3(gameObject.transform.position.x + 1f, gameObject.transform.position.y - 0.5f), Quaternion.Euler(0, 0, 15));
                manager.GetComponent<GameManager>().AddEnemy(enemy);
                enemy.GetComponent<EnemyStatus>().size = 1;
                rand3 = Random.Range(0, 4);
                enemy.GetComponent<SpriteRenderer>().sprite = manager.GetComponent<GameManager>().meteorSprites[rand3];
                GameObject enemy1 = Instantiate(gameObject, new Vector3(gameObject.transform.position.x - 1f, gameObject.transform.position.y - 0.5f), Quaternion.Euler(0, 0, -15));
                manager.GetComponent<GameManager>().AddEnemy(enemy1);
                enemy1.GetComponent<EnemyStatus>().size = 1;
                rand3 = Random.Range(0, 4);
                enemy1.GetComponent<SpriteRenderer>().sprite = manager.GetComponent<GameManager>().meteorSprites[rand3];
            }
            manager.GetComponent<GameManager>().RemoveEnemy(gameObject); //Remove from enemy collection of items
            dropItem(); //See if item should be dropped
            Destroy(gameObject); //Destory Meteor
        }
    }

    //Spawn Explosion Particles
    IEnumerator SpawnParticles()
    {
        GameObject particleSystems = Instantiate(particleSystem, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.Euler(0, 0, 15));
        yield return null;
        Destroy(particleSystem);
    }

    //Randomly check if an item should be dropped, 10% chance
    private void dropItem()
    {
        int rand = Random.Range(0, 101);
        //If above 90, drop random item
        if(rand > 90)
        {
            rand = Random.Range(0, 2);
            GameObject upgrade = Instantiate(drop, transform.position, Quaternion.Euler(0, 0, 0));
            upgrade.GetComponent<UpgradeWeapon>().newWeapon = rand;
            upgrade.GetComponent<Rigidbody2D>().AddForce(-transform.up * 50);
            Destroy(gameObject);
        }

    }
    
    //Movement for meteorite
    private void FixedUpdate()
    {
        movement();
    }

    //If the meteorite hits a wall, bounce it off
    void movement()
    {
        Vector2 maxPosX;
        if (transform.position.x < boundsLeft) //Check for left bounds
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            transform.Rotate(-2 * gameObject.transform.rotation.eulerAngles);
            gameObject.GetComponent<Rigidbody2D>().AddForce(-transform.up * speed);
            maxPosX = new Vector2(boundsLeft, transform.position.y);
            transform.position = maxPosX;
        }
        else if (transform.position.x > boundsRight) //Check for right bounds
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            transform.Rotate(-2 * transform.rotation.eulerAngles);
            gameObject.GetComponent<Rigidbody2D>().AddForce(-transform.up * speed);
            maxPosX = new Vector2(boundsRight, transform.position.y);
            transform.position = maxPosX;
        }

        if (transform.position.y < boundsDown) //Check for bottom and destroy it if it reaches that point
        {
            manager.GetComponent<GameManager>().RemoveEnemy(gameObject);
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
            source.Play();
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "Enemy") //If another meteorite, bounce it off of it
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            transform.Rotate(-2 * gameObject.transform.rotation.eulerAngles);
            gameObject.GetComponent<Rigidbody2D>().AddForce(-transform.up * speed);
        }
    }
}
