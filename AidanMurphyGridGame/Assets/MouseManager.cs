using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{

    Grid grid;

    Node currNode;
    // Start is called before the first frame update
    void Start()
    {
        grid = new Grid();
    }

    private void MouseOver()
    {
        Vector2 mouse = Input.mousePosition;
        currNode = grid.NodeFromWorldPosition(mouse);
    }
}
