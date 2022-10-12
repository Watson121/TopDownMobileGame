using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamage
{
    public void ApplyDamage(float damage);

    public void ApplyDamageEnemy(float damage, SauceType bullet);
}

public interface ICollectable
{
    public void Collect(Collectable collectable);
}
