using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Weapons", order = 1)]
public class WeaponObject : ScriptableObject
{
    public float weaponDamage;
    public float firingSpeed;
    public bool gunActive;
    public SauceType weaponType;
}
