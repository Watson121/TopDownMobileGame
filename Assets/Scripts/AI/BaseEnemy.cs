using System.Collections;
using System.Collections.Generic;
using BehaviourTree;

// TODO
// Make this Class much more of an abstract class
// So that is can be used for more than one enemy

public class BaseEnemy : Tree, IDamage
{

    #region PROPERTIES

    // The Movement Speed of the Enemy
    public float MoveSpeed
    {
        get { return moveSpeed; }
    }  
    protected static float moveSpeed = 2.0f;
    
    // The Firing Speed of the enemy
    public float FiringSpeed
    {
        set { firingSpeed = UnityEngine.Mathf.Clamp(value, 0, 1.0f); }
        get { return firingSpeed; }
    } 
    protected static float firingSpeed = 1.0f;

    // Returning the damage of the enemy
    public float Damage
    {
        get { return damage; }
    }
    protected static float damage = 5.0f;

    /// <summary>
    /// The health of current enemy
    /// </summary>
    public float Health
    {
        get { return health; }
    }
    [UnityEngine.SerializeField] private const float MAX_HEALTH = 10.0f;
    [UnityEngine.SerializeField] private float health;

    public UnityEngine.Transform FiringPosition
    {
        get { return firingPosition; }
    }
    [UnityEngine.SerializeField] private UnityEngine.Transform firingPosition;

    public GameManager GameManager
    {
        get { return gameManager; }
    }
    [UnityEngine.SerializeField] private GameManager gameManager;


    /// <summary>
    /// This is if the current Enemy is active or not
    /// </summary>
    public bool IsActive
    {
        get { return isActive; }
        set { isActive = value; }
    }

    private bool isActive = false;

    #endregion

    

    /// <summary>
    /// Applies damage to the enemy
    /// </summary>
    /// <param name="damage">How much damage to apply</param>
    public void ApplyDamage(float damage)
    {
        health -= damage;
        health = UnityEngine.Mathf.Clamp(health, 0, MAX_HEALTH);
    }

    private new void Update()
    {
        // If the ship is no longer active, then this should no longer run
        if (isActive != false)
        {
            base.Update();
            UnityEngine.Debug.Log("Working");
        }
        else if(isActive == false)
        {
            Destroy(this);
        }
       
    }

    /// <summary>
    /// Setting up the behivour tree
    /// </summary>
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
