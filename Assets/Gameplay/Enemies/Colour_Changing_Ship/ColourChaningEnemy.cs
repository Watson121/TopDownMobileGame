using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ColourChaningEnemy : BaseEnemy, IDamage
{
    protected override Node SetupTree()
    {


        displayHealthBar = true;
        health = 2;
        //moveSpeed = 1.5f;
        firingSpeed = 0.8f;
        isActive = true;
        pointsValue = 100;
        UpdateHealthBar();

        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckHealth(this),
                new CheckIfInView(transform),
                new Task_MoveAndFire(transform, this),
            }),

            new Task_Reset(transform, new UnityEngine.Vector3(100, 100, 100), this)

        }); ;

        return root;
    }

    public new void ApplyDamageEnemy(float damage, SauceType bullet)
    {
        Debug.Log("Apply Damage to Enemy!!!");

        if (bullet == enemyType)
        {
            health -= damage;

            enemyType++;

            if(enemyType > SauceType.Mayo)
            {
                enemyType = SauceType.Ketchup;
            }


            SettingTheEnemyMaterial();
            UpdateHealthBar();
        }
    }

}
