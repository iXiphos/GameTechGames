using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    public GameObject pulseEffect;

    [HideInInspector]
    public Vector3 mousePos, //Mouse Position when mouse is clicked
                   adjustedPos, //The adjusted mouse position
                   randomPos; //Random Spread

    //Screen Shake Stuff
    public float shakeLength;
    public Transform camTransform;
    [HideInInspector]
    public float shakeDuration = 0f;
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;
    [HideInInspector]
    public Vector3 originalPos;

    public GameObject bulletPrefab; //Bullet Object

    [HideInInspector]
    public Transform bulletSpawn; //Where the bullet is spawned from

    public StateMachine<PlayerShooting> stateMachine { get; set; } //Statemachine for different attacks

    [HideInInspector]
    public int ammoCount = 1000; //Ammo count for weapons

    public Text bulletText; //The ammo count text

    public float AttackSpeed; //Speed the player shoots
    public float BulletSpeed; //Speed of the bullet

    [HideInInspector]
    public float attackRate = 0; //Attack timer
    [HideInInspector]
    public float timeToNextAttack = 0; //how long until next attack

    [HideInInspector]
    public float bulletSpeed; //How quickly bullet travels

    public bool reloading;

    // Start is called before the first frame update
    void Start()
    {
        originalPos = camTransform.position; //Original Camera Position
        ammoCount = 31; //Default Ammo count
        bulletSpeed = BulletSpeed; //Set bullet speed
        attackRate = AttackSpeed; //Set attack speed
        stateMachine = new StateMachine<PlayerShooting>(this); //Create Statemachine of type player shooting
        bulletSpawn = GameObject.Find("BulletSpawn").transform; //Get Bullet spawn location
        stateMachine.ChangeState(DefaultFire.Instance); //Set the current state
    }

    // Update is called once per frame
    void Update()
    {
        bulletText.text = "Ammo: " + ammoCount;

        //If ammo count is 0, go back to default fire
        if (Input.GetKeyDown(KeyCode.R) && !reloading)
        {
            StartCoroutine(reload());
        }

        //If button is pressed, fire
        if (Input.GetMouseButton(0) && Time.time > timeToNextAttack && ammoCount != 0 && !reloading)
        {

            GameObject pulse = Instantiate(pulseEffect, transform.position, transform.rotation);
            Destroy(pulse, 10f);
            mousePos = Input.mousePosition; //Get Mouse Position
            adjustedPos = Camera.main.ScreenToWorldPoint(mousePos); //Adjusted Position
            adjustedPos.z = 0; //Set the z to zero
            stateMachine.Update();
            shakeDuration = shakeAmount;
        }
        ScreenShake(); //Shake Screen
    }

    IEnumerator reload()
    {
        reloading = true;
        yield return new WaitForSeconds(3f);
        reloading = false;
        ammoCount = 31;
    }

    //Screen shake
    void ScreenShake()
    {
        if (shakeDuration > 0) //If shaking is ready
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            camTransform.localPosition = originalPos;
        }
    }
}
