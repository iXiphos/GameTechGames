using UnityEngine;
using StateStuff;
using System.Collections;
using System.Collections.Generic;

public class DefaultFire : State<PlayerShooting>
{
    private static DefaultFire _instance;

    private DefaultFire()
    {
        if (_instance != null)
        {
            return;
        }
        _instance = this;
    }

    public static DefaultFire Instance
    {
        get
        {
            if (_instance == null)
            {
                new DefaultFire();
            }
            return _instance;
        }
    }

    public override void enterState(PlayerShooting _owner)
    {
        _owner.bulletText.text = "Ammo: " + "\u221E";
    }

    public override void exitState(PlayerShooting _owner)
    {

    }

    public override void updateState(PlayerShooting _owner)
    {
        _owner.bulletText.text = "Ammo: " + "\u221E";
        Fire(_owner);
    }

    void Fire(PlayerShooting _owner)
    {
        _owner.timeToNextAttack = Time.time + _owner.attackRate;
        GameObject bullet = Object.Instantiate(_owner.bulletPrefab, _owner.bulletSpawn.position, _owner.bulletSpawn.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.up * _owner.bulletSpeed);
        Object.Destroy(bullet, 3f);
    }

}
