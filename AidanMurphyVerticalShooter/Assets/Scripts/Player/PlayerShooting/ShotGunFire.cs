using UnityEngine;
using StateStuff;
using System.Collections;
using System.Collections.Generic;

public class ShotGunFire : State<PlayerShooting>
{
    private static ShotGunFire _instance;

    float spreadAngle = 10;
    int damageAmount = 4;

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

    public override void enterState(PlayerShooting _owner)
    {
        _owner.ammoCount = 20;
        _owner.bulletText.text = "Ammo: " + _owner.ammoCount;
        _owner.attackRate = 0.3f;
        _owner.bulletSpeed = 1000;
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
        FireShotGun(_owner);
    }

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
