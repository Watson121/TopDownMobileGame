using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Weapon Struct that will be used for all weapons in the game
/// </summary>
public struct Weapon
{
    private float weaponDamage;
    private float firingSpeed;
    private bool gunActive;

    // Base Constructor for the Weapon
    public Weapon(float damage, float speed, bool active)
    {
        weaponDamage = damage;
        firingSpeed = speed;
        gunActive = active;
    }

    // Copy Constructor for the weapon
    public Weapon(Weapon other)
    {
         weaponDamage = other.Damage;
         firingSpeed = other.FiringSpeed;
         gunActive = other.Active;
        
    }

    // Returning the damage of this weapon
    public float Damage
    {
        get { return weaponDamage;  }
    }

    // Returning the firing speed of the weapon
    public float FiringSpeed
    {
        get { return firingSpeed; }
    }

    // Returning in the Weapon is active or not
    public bool Active
    {
        get { return gunActive; }
    }

}
