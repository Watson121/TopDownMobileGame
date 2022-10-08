using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Main Class for the Bullet.
/// It deals with movement and collision interaction
/// </summary>
/// 

public enum SauceType
{
    Ketchup,
    Musturd,
    Mayo
}



public class Bullet : MonoBehaviour
{

    #region PROPERIES

    // Property to return Bullet Damage
    public float BulletDamage
    {
        set { bulletDamage = value; }
    }
    private float bulletDamage;

    // Property to return if bullet is moving or not
    public bool BulletMoving
    {
        get { return bulletMoving; }
    }
    [SerializeField]private bool bulletMoving;

    #endregion

    private Transform bullet;
    private Vector3 poolZone;
    private SauceType _bulletType;

    private MeshRenderer meshRenderer;
    [SerializeField] private Material ketchupBullet;
    [SerializeField] private Material mustardBullet;
    [SerializeField] private Material mayoBullet;

    // Getting the bullet transform and pool zone
    private void Awake()
    {
        bullet = this.transform;
        poolZone = bullet.position;
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Bullet Movement
    public IEnumerator BulletFire(Vector3 startPos, Weapon weapon, Vector3 direction, float bulletLifetime)
    {
        bulletDamage = weapon.Damage;
        _bulletType = weapon.WeaponType;

        switch (_bulletType)
        {
            case SauceType.Ketchup:
                meshRenderer.material = ketchupBullet;
                break;
            case SauceType.Musturd:
                meshRenderer.material = mustardBullet;
                break;
            case SauceType.Mayo:
                meshRenderer.material = mayoBullet;
                break;
        }


        float elaspedTime = 0;
        bullet.position = startPos;

        bulletMoving = true;

        while ((elaspedTime < bulletLifetime) && bulletMoving)
        {
            bullet.position += direction * weapon.FiringSpeed * Time.deltaTime;
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
        StopAllCoroutines();
    }

    // If the bullet collides with something then this will fire
    private void OnTriggerEnter(Collider other)
    {
        var obj = other.gameObject.GetComponent<IDamage>();

        if (obj != null)
        {
            if(other.tag == "Player")
            {
                obj.ApplyDamage(bulletDamage);
            }
            else if(other.tag == "Enemy")
            {
                obj.ApplyDamageEnemy(bulletDamage, _bulletType);
            }

            ResetBullet();
        }
        
    }

}
