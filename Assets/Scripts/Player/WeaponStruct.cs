using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Weapon
{
    private float damage;
    private float firingSpeed;
    private bool active;

    public Weapon(float damage, float speed, bool active)
    {
        this.damage = damage;
        this.firingSpeed = speed;
        this.active = active;
    }

    public float Damage
    {
        get { return damage;  }
    }

    public float FiringSpeed
    {
        get { return firingSpeed; }
    }

    public bool Active
    {
        get { return active; }
    }

}
