using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float Health = 3;

    private bool hitFrames = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DestroyPlayer();
    }

    public void doDamage()
    {
        if (!hitFrames)
        {
            Health--;
            hitFrames = true;
            StartCoroutine(flashSprite());
        }
    }

    IEnumerator flashSprite()
    {
        float timeToEnd = 1.6f + Time.time;
        while(timeToEnd > Time.time)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(.2f);
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            yield return new WaitForSeconds(.2f);
        }
        hitFrames = false;
    }

    void DestroyPlayer()
    {
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
