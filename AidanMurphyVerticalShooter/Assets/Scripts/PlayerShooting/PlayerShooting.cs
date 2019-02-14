using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    //Screen Shake Stuff
    public float shakeLength;
    public Transform camTransform;
    [HideInInspector]
    public float shakeDuration = 0f;
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;
    [HideInInspector]
    public Vector3 originalPos;

    public GameObject bulletPrefab;

    [HideInInspector]
    public Transform bulletSpawn;

    public StateMachine<PlayerShooting> stateMachine { get; set; }

    [HideInInspector]
    public int ammoCount = 1000;

    public Text bulletText;

    public float AttackSpeed;
    public float BulletSpeed;

    [HideInInspector]
    public float attackRate = 0;
    [HideInInspector]
    public float timeToNextAttack = 0;

    [HideInInspector]
    public float bulletSpeed;

    // Start is called before the first frame update
    void Start()
    {
        originalPos = camTransform.position;
        ammoCount = 1000;
        bulletSpeed = BulletSpeed;
        attackRate = AttackSpeed;
        stateMachine = new StateMachine<PlayerShooting>(this);
        bulletSpawn = GameObject.Find("BulletSpawn").transform;
        stateMachine.ChangeState(DefaultFire.Instance);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (ammoCount == 0)
        {
            stateMachine.ChangeState(DefaultFire.Instance);
        }
        if (Input.GetKey(KeyCode.Space) && Time.time > timeToNextAttack)
        {
            stateMachine.Update();
            shakeDuration = shakeLength;
        }
        ScreenShake();
    }

    void ScreenShake()
    {
        if (shakeDuration > 0)
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
