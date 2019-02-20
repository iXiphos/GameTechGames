using UnityEngine;
using StateStuff;
using System.Collections;
using System.Collections.Generic;

public class FullAutoFire : State<PlayerShooting>
{
    private static FullAutoFire _instance;

    float randomRange = 10f;

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

    public override void enterState(PlayerShooting _owner)
    {
        _owner.ammoCount = 80;
        _owner.bulletText.text = "Ammo: " + _owner.ammoCount;
        _owner.attackRate = 0.1f;
        _owner.bulletSpeed = 800;
    }

    public override void exitState(PlayerShooting _owner)
    {
        _owner.attackRate = _owner.AttackSpeed;
        _owner.bulletSpeed = _owner.BulletSpeed;
        _owner.ammoCount = 1000;
    }

    public override void updateState(PlayerShooting _owner)
    {
        _owner.bulletText.text = "Ammo: " + _owner.ammoCount;
        Fire(_owner);
    }

    void Fire(PlayerShooting _owner)
    {
        _owner.ammoCount--;
        _owner.timeToNextAttack = Time.time + _owner.attackRate;
        float randomZ = Random.Range(-randomRange, randomRange);
        GameObject bullet = GameObject.Instantiate(_owner.bulletPrefab, _owner.bulletSpawn.transform.position, Quaternion.Euler(0, 0, randomZ));
        bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.up * _owner.bulletSpeed);
        bullet.GetComponent<Damage>().damageAmount = 2;
        GameObject.Destroy(bullet, 2.5f);
    }
}
