using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float BulletDamage
    {
        set { bulletDamage = value; }
    }

    public bool BulletMoving
    {
        get { return bulletMoving; }
    }

    private Transform bullet;
    private Vector3 poolZone;
    private float bulletDamage;
    private bool bulletMoving;

    private void Awake()
    {
        bullet = this.transform;
        poolZone = bullet.position;
    }

    public IEnumerator BulletFire(float bulletSpeed, Vector3 startPos, float bulletDamage)
    {
        this.bulletDamage = bulletDamage;

        float elaspedTime = 0;
        bullet.position = startPos;

        bulletMoving = true;

        while (elaspedTime < 3)
        {
            bullet.position += Vector3.forward * bulletSpeed * Time.deltaTime;
            elaspedTime += Time.deltaTime;
            Debug.Log(elaspedTime);
            yield return null;
        }

        elaspedTime = 0;

        ResetBullet();
   
    }

    private void ResetBullet()
    {
        bullet.position = poolZone;
        bulletMoving = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        var obj = other.gameObject.GetComponent<IDamage>();

        if (obj != null)
        {
            obj.ApplyDamage(bulletDamage);
        }
        
    }

}
