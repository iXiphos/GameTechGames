using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public int damageAmount { get; set; } //How much damage it should do

    public AudioSource source; //Sound effect

    private void Start()
    {
        //Get audio source
        source = gameObject.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {   //Deal Damage to collision if the collision is enemy, turn of object to play sound effect, then destroy it
        if(collision.tag == "Enemy")
        {
            collision.GetComponent<EnemyStatus>().health -= damageAmount;
            source.Play(0);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            Destroy(gameObject, 3f);
        }
    }
}
