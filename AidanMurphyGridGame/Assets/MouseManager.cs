using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{

    Grid grid;

    bool hover;

    GameObject currentSquare;


    // Start is called before the first frame update
    void Start()
    {
        grid = new Grid();
    }

    private void Update()
    {
        MouseOver();
    }

    private void MouseOver()
    {
        Vector2 mouse = Input.mousePosition;
        currentSquare = grid.SquareFromWorldPosition(mouse);
        if(currentSquare != null)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    private void mouseExit()
    {
        
    }
}
