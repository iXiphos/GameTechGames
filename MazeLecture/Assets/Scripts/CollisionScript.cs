using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("OnCollisionEnter2D");
    }

    void OnCollisionExit2D(Collision2D other)
    {
        Debug.Log("OnCollisionExit2D");
    }

    void OnCollisionStay2D(Collision2D other)
    {
        Debug.Log("OnCollisionStay2D");
    }

}
