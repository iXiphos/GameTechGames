using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResetGame : MonoBehaviour
{

    Grid grid;
    public bool reset;
    bool faded = false;

    public Image BlackFade;
    public Text LoseText;
    // Start is called before the first frame update
    void Start()
    {
        BlackFade.canvasRenderer.SetAlpha(0.0f);
        LoseText.canvasRenderer.SetAlpha(0.0f);
        reset = false;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (reset)
        {
            FadeToBlack();
            if(Input.GetKeyDown(KeyCode.Return)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    
    void FadeToBlack()
    {
        BlackFade.CrossFadeAlpha(1.0f, 0.5f, false);
        LoseText.CrossFadeAlpha(1.0f, 0.6f, false);
    }
}
