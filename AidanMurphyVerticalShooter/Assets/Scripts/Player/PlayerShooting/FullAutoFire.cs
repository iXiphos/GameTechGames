using UnityEngine;
using StateStuff;
using System.Collections;
using System.Collections.Generic;

//Quickly Shoot straight, low damage but lots of bullets
public class FullAutoFire : State<PlayerShooting>
{
    private static FullAutoFire _instance; //Creating an instance of fullauto fire

    float randomRange = 10f; //Spread of shot

    //If instance already exists, break
    private FullAutoFire()
    {
        if (_instance != null)
        {
            return;
        }
        _instance = this;
    }

    public static FullAutoFire Instance
    {
        get
        {
            if (_instance == null)
            {
                new FullAutoFire();
            }
            return _instance;
        }
    }

    //What to do when this state is entered
    public override void enterState(PlayerShooting _owner)
    {
        _owner.ammoCount = 80;
        _owner.bulletText.text = "Ammo: " + _owner.ammoCount;
        _owner.attackRate = 0.1f;
        _owner.bulletSpeed = 800;
    }

    //What to do when this state is exited
    public override void exitState(PlayerShooting _owner)
    {
        _owner.attackRate = _owner.AttackSpeed;
        _owner.bulletSpeed = _owner.BulletSpeed;
        _owner.ammoCount = 1000;
    }

    //Update For this state
    public override void updateState(PlayerShooting _owner)
    {
        _owner.bulletText.text = "Ammo: " + _owner.ammoCount;
        Fire(_owner);
    }

    //Fire Function
    void Fire(PlayerShooting _owner)
    {
        _owner.ammoCount--;
        _owner.timeToNextAttack = Time.time + _owner.attackRate;
        float randomZ = Random.Range(-randomRange, randomRange);
        GameObject bullet = GameObject.Instantiate(_owner.bulletPrefab, _owner.bulletSpawn.transform.position, Quaternion.Euler(0, 0, randomZ));
        bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.up * _owner.bulletSpeed);
        bullet.GetComponent<Damage>().damageAmount = 3;
        GameObject.Destroy(bullet, 2.5f);
    }
}
