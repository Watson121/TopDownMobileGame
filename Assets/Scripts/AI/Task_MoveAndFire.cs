using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using Viewport;

public class Task_MoveAndFire : Node
{
    private Transform _transform;
    private BaseEnemy _enemy;
    private Weapon _enemyWeapon;

    private List<Bullet> bullets;
    private static int index = 0;

    Vector3 characterPosition;
    float horinzontalSpeed = 4;
    float countdown = 2.0f;

    public Task_MoveAndFire(Transform transform, BaseEnemy enemy)
    {
        this._transform = transform;
        this._enemy = enemy;

        _enemyWeapon = new Weapon(10.0f, 3.0f, true, SauceType.Ketchup);
        bullets = _enemy.GameManager.EnemyBulletPool;

        int random = Random.Range(0, 10);

        if(random < 6)
        {
            horinzontalSpeed = 0;
        }else
        {
            horinzontalSpeed = 4;
        }

    }

    public override NodeState Evaluate()
    {
        characterPosition = _transform.position;

        characterPosition += new Vector3(horinzontalSpeed * Time.deltaTime, 0, (_enemy.MoveSpeed * Time.deltaTime) * -1);
        characterPosition.x = Mathf.Clamp(characterPosition.x, -ViewportBoundaries.frustumWidth, ViewportBoundaries.frustumWidth);

        if (characterPosition.x == ViewportBoundaries.frustumWidth) horinzontalSpeed *= -1;
        else if (characterPosition.x == -ViewportBoundaries.frustumWidth) horinzontalSpeed *= -1;
        
        

        _transform.position = characterPosition;


        /*// Moving the enemy - Forward
        _transform.position += Vector3.back * _enemy.MoveSpeed * Time.deltaTime;
        
        _transform.position += Vector3.left * _enemy.MoveSpeed * Time.deltaTime;
        _transform.position.x = Mathf.Clamp(_transform.position.x, -20, 20);*/
        




        // Firing the weapon
        _enemy.FiringSpeed -= Time.deltaTime;
        if(_enemy.FiringSpeed == 0)
        {
            Bullet currentBullet = bullets[index];

            if (!(currentBullet.BulletMoving))
            {
                _enemy.GameManager.StartCoroutine(currentBullet.BulletFire(_enemy.FiringPosition.position, _enemyWeapon, Vector3.back, 6.0f));
                _enemy.FiringSpeed = 1.0f;
            }

            index++;

            if (index >= bullets.Count)
            {
                index = 0;
            }

        }


        state = NodeState.RUNNING;
        return state;



    }

}
