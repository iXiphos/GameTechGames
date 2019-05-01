using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public int damageAmount = 1;

    public GameObject particleSystems; //Sound effect



    private void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {   //Deal Damage to collision if the collision is enemy, turn of object to play sound effect, then destroy it
        if(collision.tag == "Enemy")
        {
            collision.GetComponent<EnemyStatus>().health -= damageAmount;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            GameObject particle = Instantiate(particleSystems, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.Euler(0, 0, 15));
            Destroy(particle, 10f);
            Destroy(gameObject, 3f);
        }
    }
}
