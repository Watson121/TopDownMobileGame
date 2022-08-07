using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Weapon
{
    private float weaponDamage;
    private float firingSpeed;
    private bool gunActive;

    public Weapon(float damage, float speed, bool active)
    {
        weaponDamage = damage;
        firingSpeed = speed;
        gunActive = active;
    }

    public Weapon(Weapon other)
    {
         weaponDamage = other.Damage;
         firingSpeed = other.FiringSpeed;
         gunActive = other.Active;
        
    }

    public float Damage
    {
        get { return weaponDamage;  }
    }

    public float FiringSpeed
    {
        get { return firingSpeed; }
    }

    public bool Active
    {
        get { return gunActive; }
    }

}
