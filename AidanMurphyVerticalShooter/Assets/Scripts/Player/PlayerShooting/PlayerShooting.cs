using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    public AudioSource source; //Audio Source for shooting

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

    // Start is called before the first frame update
    void Start()
    {
        originalPos = camTransform.position; //Original Camera Position
        ammoCount = 1000; //Default Ammo count
        bulletSpeed = BulletSpeed; //Set bullet speed
        attackRate = AttackSpeed; //Set attack speed
        stateMachine = new StateMachine<PlayerShooting>(this); //Create Statemachine of type player shooting
        bulletSpawn = GameObject.Find("BulletSpawn").transform; //Get Bullet spawn location
        stateMachine.ChangeState(DefaultFire.Instance); //Set the current state
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //If ammo count is 0, go back to default fire
        if (ammoCount == 0)
        {
            stateMachine.ChangeState(DefaultFire.Instance);
        }

        //If button is pressed, fire
        if (Input.GetKey(KeyCode.Space) && Time.time > timeToNextAttack)
        {
            stateMachine.Update();
            shakeDuration = shakeLength;
            source.Play(0);

        }
        ScreenShake(); //Shake Screen
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
