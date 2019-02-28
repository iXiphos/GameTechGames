using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public GameObject Door;

    bool open = false;

    public int count = 0;

    void OnTriggerEnter2D(Collider2D collision)
    {
        ++count;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !open)
        {
            open = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        --count;
        if (collision.tag == "Player" && count == 0)
        {
             open = false;
        }
    }

    void Update()
    {
        if (open)
        {
            Door.GetComponent<BoxCollider2D>().enabled = false;
            Door.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            Door.GetComponent<BoxCollider2D>().enabled = true;
            Door.GetComponent<SpriteRenderer>().enabled = true;
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

}
