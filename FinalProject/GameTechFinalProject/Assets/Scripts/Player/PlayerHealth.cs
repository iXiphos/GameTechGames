using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int Health = 3; //Total Player Health

    private bool hitFrames = false; //Has the player been hit recently

    public Image[] healthImages; //Images for health

    public Sprite emptyHealth; //Empty Health Sprite

    public Image redFlash; //Image that flashes when damage is taken

    // Update is called once per frame
    void Update()
    {
        DestroyPlayer(); //Destroy player if they take enough damage
    }

    //Deal Damage to Player
    public void doDamage()
    {
        //If the player has not been hit recently, deal damage and effects
        if (!hitFrames)
        {
            Health--;
            healthImages[Health].sprite = emptyHealth;
            hitFrames = true;
            StartCoroutine(flashSprite());
        }
    }

    //Flash the Sprite to indicate getting hit
    IEnumerator flashSprite()
    {
        StartCoroutine(flashRed());
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

    //Flash the screen red to also indicate getting hit
    IEnumerator flashRed()
    {
        redFlash.enabled = true;
        yield return new WaitForSeconds(0.05f);
        redFlash.enabled = false;
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
