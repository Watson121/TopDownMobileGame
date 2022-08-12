using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Main Class for the Bullet.
/// It deals with movement and collision interaction
/// </summary>
public class Bullet : MonoBehaviour
{

    #region PROPERIES

    // Property to return Bullet Damage
    public float BulletDamage
    {
        set { bulletDamage = value; }
    }

    // Property to return if bullet is moving or not
    public bool BulletMoving
    {
        get { return bulletMoving; }
    }

    #endregion

    private Transform bullet;
    private Vector3 poolZone;
    private float bulletDamage;
    private bool bulletMoving;

    // Getting the bullet transform and pool zone
    private void Awake()
    {
        bullet = this.transform;
        poolZone = bullet.position;
    }

    // Bullet Movement
    public IEnumerator BulletFire(Vector3 startPos, Weapon weapon)
    {
        this.bulletDamage = weapon.Damage;

        float elaspedTime = 0;
        bullet.position = startPos;

        bulletMoving = true;

        while (elaspedTime < 3)
        {
            bullet.position += Vector3.forward * weapon.FiringSpeed * Time.deltaTime;
            elaspedTime += Time.deltaTime;
            yield return null;
        }

        ResetBullet();
    }

    // Reseting bullet back to it's start position
    private void ResetBullet()
    {
        bullet.position = poolZone;
        bulletMoving = false;
    }

    // If the bullet collides with something then this will fire
    private void OnTriggerEnter(Collider other)
    {
        var obj = other.gameObject.GetComponent<IDamage>();

        if (obj != null)
        {
            obj.ApplyDamage(bulletDamage);
        }
        
    }

}
