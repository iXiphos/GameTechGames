using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public int damageAmount { get; set; }

    public AudioSource source;

    private void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            collision.GetComponent<EnemyStatus>().health -= damageAmount;
            source.Play(0);
            Destroy(gameObject);
        }
    }
}
