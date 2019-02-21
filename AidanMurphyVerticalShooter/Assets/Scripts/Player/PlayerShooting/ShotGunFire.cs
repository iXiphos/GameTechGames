using UnityEngine;
using StateStuff;
using System.Collections;
using System.Collections.Generic;

public class ShotGunFire : State<PlayerShooting>
{
    private static ShotGunFire _instance; //Creating Instance of Shotgun fire

    float spreadAngle = 10; //The spread of the weapon
    int damageAmount = 5; //How much damage it does

    //If instance already exists, break
    private ShotGunFire()
    {
        if (_instance != null)
        {
            return;
        }
        _instance = this;
    }

    public static ShotGunFire Instance
    {
        get
        {
            if (_instance == null)
            {
                new ShotGunFire();
            }
            return _instance;
        }
    }

    //What to do when state is entered
    public override void enterState(PlayerShooting _owner)
    {
        _owner.ammoCount = 40; //Set ammo count
        _owner.bulletText.text = "Ammo: " + _owner.ammoCount;
        _owner.attackRate = 0.225f; //Set attack rate
        _owner.bulletSpeed = 1100; //Set bullet speed
    }

    //What to do when state is exited
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
        FireShotGun(_owner);
    }

    //Fire the shot gun in pattern of 3
    void FireShotGun(PlayerShooting _owner)
    {
        _owner.ammoCount--;
        _owner.timeToNextAttack = Time.time + _owner.attackRate;
        GameObject bullet = GameObject.Instantiate(_owner.bulletPrefab, _owner.bulletSpawn.position, Quaternion.Euler(0, 0, spreadAngle));
        GameObject bullet1 = GameObject.Instantiate(_owner.bulletPrefab, _owner.bulletSpawn.position, Quaternion.Euler(0, 0, 0));
        GameObject bullet2 = GameObject.Instantiate(_owner.bulletPrefab, _owner.bulletSpawn.position, Quaternion.Euler(0, 0, -spreadAngle));
        bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.up * _owner.bulletSpeed);
        bullet1.GetComponent<Rigidbody2D>().AddForce(bullet1.transform.up * _owner.bulletSpeed);
        bullet2.GetComponent<Rigidbody2D>().AddForce(bullet2.transform.up * _owner.bulletSpeed);
        bullet.GetComponent<Damage>().damageAmount = damageAmount;
        bullet1.GetComponent<Damage>().damageAmount = damageAmount;
        bullet2.GetComponent<Damage>().damageAmount = damageAmount;
        GameObject.Destroy(bullet, 3f);
        GameObject.Destroy(bullet1, 3f);
        GameObject.Destroy(bullet2, 3f);
    }

}
