using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pulse : MonoBehaviour
{
    public GameObject pulseEffect;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.GetComponent<Rigidbody2D>().velocity.x > 2.5f || gameObject.GetComponent<Rigidbody2D>().velocity.y > 2.5f)
        {
            GameObject pulseParticle = Instantiate(pulseEffect, transform.position, transform.rotation);
            //Destroy(pulseParticle, gameObject.GetComponent<Rigidbody2D>().velocity.x + gameObject.GetComponent<Rigidbody2D>().velocity.y - 1.5f);
        }
    }
}
