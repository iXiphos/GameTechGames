using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject Player;
    public GameObject Enemy;
    public Vector3 endPoint;

    public Image BlackFade;
    public Text WinText;

    // Start is called before the first frame update
    void Start()
    {
        BlackFade.canvasRenderer.SetAlpha(0.0f);
        WinText.canvasRenderer.SetAlpha(0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if(Player.transform.position == endPoint)
        {
            Player.GetComponent<PlayerMovement>().enabled = false;
            Enemy.GetComponent<Pathfinding>().enabled = false;
            FadeToBlack();
        }
    }

    void FadeToBlack()
    {
        BlackFade.CrossFadeAlpha(1.0f, 0.5f, false);
        WinText.CrossFadeAlpha(1.0f, 0.6f, false);
    }
}
