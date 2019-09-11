using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public GameObject Door; //Door to Be Opened

    bool open = false; //Is the Door Open

    public int count = 0; //How Many players, ghosts are on the switch

    //Increase count
    void OnTriggerEnter2D(Collider2D collision)
    {
        ++count;
    }

    //If player is in the switch, open
    void OnTriggerStay2D(Collider2D collision)
    {
        if((collision.tag == "Player" || collision.tag == "Ghost") && !open)
        {
            open = true;
        }
    }

    //If player leaves and nothing else is on it, close the door
    void OnTriggerExit2D(Collider2D collision)
    {
        count--;
        if ((collision.tag == "Player" || collision.tag == "Ghost") && count == 0)
        {
             open = false;
        }
    }

    //If open, disable door, if not close door
    void Update()
    {
        if (open)
        {
            Door.GetComponent<BoxCollider2D>().enabled = false;
            Door.SetActive(false);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            Door.GetComponent<BoxCollider2D>().enabled = true;
            Door.SetActive(true);
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

}
