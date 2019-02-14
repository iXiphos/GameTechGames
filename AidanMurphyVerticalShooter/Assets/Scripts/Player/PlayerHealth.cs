using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float Health = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DestroyPlayer();
    }

    void DestroyPlayer()
    {
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
