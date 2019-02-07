using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResetGame : MonoBehaviour
{
    public bool reset; //Variable to tell to reset

    public Image BlackFade; //Black Screen for Fade
    public Text LoseText; //Text to display

    // Start is called before the first frame update
    void Awake()
    {
        //Turn of object and hide the text and black screen
        gameObject.SetActive(false);
        BlackFade.canvasRenderer.SetAlpha(0.0f);
        LoseText.canvasRenderer.SetAlpha(0.0f);
        reset = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (reset)
        {
            FadeToBlack();
            //If input is enter, reset scene
            if(Input.GetKeyDown(KeyCode.Return)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    
    //Fade the text and black screen in
    void FadeToBlack()
    {
        BlackFade.CrossFadeAlpha(1.0f, 0.5f, false);
        LoseText.CrossFadeAlpha(1.0f, 0.6f, false);
    }
}
