using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class Task_MoveAndFire : Node
{
    private Transform _transform;
    private BaseEnemy _enemy;
    private Weapon _enemyWeapon;

    private List<Bullet> bullets;
    private static int index = 0;

    

    public Task_MoveAndFire(Transform transform, BaseEnemy enemy)
    {
        this._transform = transform;
        this._enemy = enemy;

        _enemyWeapon = new Weapon(10.0f, 3.0f, true);
        bullets = _enemy.GameManager.BulletPool;
     
    }

    public override NodeState Evaluate()
    {
        // Moving the enemy
        _transform.position += Vector3.back * _enemy.MoveSpeed * Time.deltaTime;

        // Firing the weapon
        _enemy.FiringSpeed -= Time.deltaTime;
        if(_enemy.FiringSpeed == 0)
        {
            Bullet currentBullet = bullets[index];

            if (!(currentBullet.BulletMoving))
            {
                _enemy.StartCoroutine(currentBullet.BulletFire(_enemy.FiringPosition.position, _enemyWeapon));
                _enemy.FiringSpeed = 1.0f;
            }

        }


        state = NodeState.RUNNING;
        return state;



    }

}
