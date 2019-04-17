using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pulse : MonoBehaviour
{
    public GameObject pulseEffect;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Memes");
        GameObject pulseParticle = Instantiate(pulseEffect, transform.position, transform.rotation);
    }
}
