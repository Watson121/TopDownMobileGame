using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float BulletDamage
    {
        set { bulletDamage = value; }
    }

    private Transform bullet;
    private Vector3 poolZone;
    private float bulletDamage;

    private void Awake()
    {
        bullet = this.transform;
        poolZone = bullet.position;
    }

    public IEnumerator BulletFire(float bulletSpeed, Vector3 startPos)
    {
        float elaspedTime = 0;
        bullet.position = startPos;

        while (elaspedTime < 3)
        {
            bullet.position += Vector3.forward * 10.0f * Time.smoothDeltaTime;
            elaspedTime += Time.deltaTime;
            yield return null;
        }

        ResetBullet();
   
    }

    private void ResetBullet()
    {
        bullet.position = poolZone;
    }

    private void OnTriggerEnter(Collider other)
    {
        var obj = other.gameObject.GetComponent<IDamage>();
        Debug.Log(obj);
        
    }

}
