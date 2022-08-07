using System.Collections;
using System.Collections.Generic;
using BehaviourTree;

public class CroutonShip : Tree, IDamage
{
    public static float moveSpeed = 2.0f;
    public static float fireSpeed = 3.0f;
    public static float damage = 5.0f;

    public float Health
    {
        get { return health; }
    }


    [UnityEngine.SerializeField] private const float MAX_HEALTH = 10.0f;
    [UnityEngine.SerializeField] private float health;

    public void ApplyDamage(float damage)
    {

        UnityEngine.Debug.Log("Enemy Hit");

        health -= damage;

        health = UnityEngine.Mathf.Clamp(health, 0, MAX_HEALTH);
    }

    protected override Node SetupTree()
    {
        health = MAX_HEALTH;

        Node root = new Selector(new List<Node> 
        { 
            new Sequence(new List<Node>
            {
                new CheckHealth(this),
                new CheckIfInView(transform),
                new Task_MoveAndFire(transform),
            }),
        });

        return root;
    }

    
}
