using System.Collections;
using System.Collections.Generic;
using BehaviourTree;

public class CroutonShip : Tree, IDamage
{
    public static float moveSpeed = 2.0f;
    public static float fireSpeed = 3.0f;
    public static float damage = 5.0f;

    private bool isActive = false;

    public float Health
    {
        get { return health; }
    }

    public bool IsActive
    {
        get { return isActive; }
        set { isActive = value; }
    }


    [UnityEngine.SerializeField] private const float MAX_HEALTH = 10.0f;
    [UnityEngine.SerializeField] private float health;

    public void ApplyDamage(float damage)
    {

        UnityEngine.Debug.Log("Enemy Hit");

        health -= damage;

        health = UnityEngine.Mathf.Clamp(health, 0, MAX_HEALTH);
    }

    private new void Update()
    {
        if (isActive != false)
        {
            base.Update();
            UnityEngine.Debug.Log("Working");
        }
    }

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
                new Task_MoveAndFire(transform),
            }),

            new Task_Reset(transform, new UnityEngine.Vector3(100, 100, 100), this)

        }); ;

        return root;
    }

    
}
