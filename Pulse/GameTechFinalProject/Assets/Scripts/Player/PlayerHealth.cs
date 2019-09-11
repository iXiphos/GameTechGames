using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int Health = 3; //Total Player Health

    private bool hitFrames = false; //Has the player been hit recently

    public Text healthText; //Text For health

    // Update is called once per frame
    void Update()
    {
        DestroyPlayer(); //Destroy player if they take enough damage
        healthText.text = "Health: " + Health;
    }

    //Deal Damage to Player
    public void doDamage()
    {
        //If the player has not been hit recently, deal damage and effects
        if (!hitFrames)
        {
            Health--;
            hitFrames = true;
            StartCoroutine(flashSprite());
        }
    }

    //Flash the Sprite to indicate getting hit
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

    //Destroy player if there health hits 0
    void DestroyPlayer()
    {
        if (Health <= 0)
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().playerDead();
            Destroy(gameObject);
        }
    }
}
