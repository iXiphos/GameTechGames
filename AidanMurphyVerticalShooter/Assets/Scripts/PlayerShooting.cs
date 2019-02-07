using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum firePattern {
    straight,
    shotGun
}

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;

    Transform bulletSpawn;

    firePattern firingPattern;

    public float attackRate = 0;
    private float timeToNextAttack = 0;

    public float bulletSpeed;

    // Start is called before the first frame update
    void Start()
    {
        firingPattern = firePattern.straight;
        bulletSpawn = GameObject.Find("BulletSpawn").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > timeToNextAttack)
        {
            switch (firingPattern)
            {
                case firePattern.straight:
                    FireStraight();
                    break;
                case firePattern.shotGun:
                    FireShotGun();
                    break;
            }
        }
    }

    void FireStraight()
    {
        timeToNextAttack = Time.time + attackRate;
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(transform.right * bulletSpeed);
        Destroy(bullet, 3f);
    }

    void FireShotGun()
    {

    }

}
