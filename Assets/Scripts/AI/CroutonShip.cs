using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CroutonShip : BaseEnemy
{

   

    protected override Node SetupTree()
    {
        

        health = MAX_HEALTH;
        isActive = true;

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
}
