using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public List<Bullet> BulletPool
    {
        get { return bulletPool; }
    }

    [SerializeField] private List<Bullet> bulletPool;


    // Start is called before the first frame update
    void Start()
    {
        GameObject[] tempBullets = GameObject.FindGameObjectsWithTag("Bullet");

        foreach (GameObject bullet in tempBullets)
        {
            bulletPool.Add(bullet.GetComponent<Bullet>());
        }
    }

}
