using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelPerfect : MonoBehaviour
{

    Camera cam;

    public GameObject objects;

    private void Awake()
    {
        cam = gameObject.GetComponent<Camera>();
        //cam.orthographicSize = (float)((1920f) / (128f)) * 0.5f;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
