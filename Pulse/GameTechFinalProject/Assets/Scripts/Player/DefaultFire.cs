using UnityEngine;
using StateStuff;
using System.Collections;
using System.Collections.Generic;

public class DefaultFire : State<PlayerShooting>
{
    private static DefaultFire _instance; //Creating instance of Default Fire

    //If instance already exists, break
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

    //What to do when state is entered
    public override void enterState(PlayerShooting _owner)
    {
        //_owner.bulletText.text = "Ammo: " + "\u221E";
    }

    //What to do when state is exited
    public override void exitState(PlayerShooting _owner)
    {

    }

    //Update For this state
    public override void updateState(PlayerShooting _owner)
    {
        //_owner.bulletText.text = "Ammo: " + "\u221E";
        Fire(_owner);
    }

    //Default Fire Function
    void Fire(PlayerShooting _owner)
    {
        _owner.timeToNextAttack = Time.time + _owner.attackRate;
        _owner.ammoCount--;
        GameObject bullet = GameObject.Instantiate(_owner.bulletPrefab, _owner.transform.position, _owner.transform.rotation);
        bullet.transform.right = _owner.adjustedPos - _owner.transform.position;
        bullet.GetComponent<Rigidbody2D>().velocity = _owner.BulletSpeed * bullet.transform.right;
        GameObject.Destroy(bullet, 5f);
    }

}
