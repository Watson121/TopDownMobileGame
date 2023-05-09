using System;
using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;

// TODO
// Make this Class much more of an abstract class
// So that is can be used for more than one enemy


public class BaseEnemy : BehaviourTree.Tree, IDamage
{

    #region Character Speed

    // The Movement Speed of the Enemy
    public float MoveSpeed
    {
        get { return moveSpeed; }
    }  
    protected static float moveSpeed = 8.0f;
    
    // The Firing Speed of the enemy
    public float FiringSpeed
    {
        set { firingSpeed = UnityEngine.Mathf.Clamp(value, 0, 1.0f); }
        get { return firingSpeed; }
    } 
    protected static float firingSpeed = 1.0f;

    #endregion

    #region Damage

    // Returning the damage of the enemy
    public float Damage
    {
        get { return damage; }
    }
    protected static float damage = 5.0f;

    public UnityEngine.Transform FiringPosition
    {
        get { return firingPosition; }
    }
    [UnityEngine.SerializeField] private UnityEngine.Transform firingPosition;

    #endregion

    #region Health

    /// <summary>
    /// The health of current enemy
    /// </summary>
    public float Health
    {
        get { return health; }
    }
    protected const float MAX_HEALTH = 10.0f;
    [UnityEngine.SerializeField] protected float health;

    /// <summary>
    /// This is if the current Enemy is active or not
    /// </summary>
    public bool IsActive
    {
        get { return isActive; }
        set { isActive = value; }
    }
    protected bool isActive = false;

    #endregion

    #region Health Bar

    protected bool displayHealthBar;
    [SerializeField]private Transform healthBar;
    [SerializeField]private List<Transform> healthBarElements = new List<Transform>(); 

    #endregion

    #region Managers

    public GameManager GameManager
    {
        get { return gameManager; }
    }
    [UnityEngine.SerializeField] private GameManager gameManager;
    [UnityEngine.SerializeField] private HUDManager hudManager;

    #endregion

    #region Points

    public int PointsValue
    {
        get { return pointsValue; }
    }
    [UnityEngine.SerializeField] protected int pointsValue;

    #endregion

    #region Enemy Type

    [SerializeField]protected SauceType enemyType = SauceType.Ketchup;

    #endregion

    #region Enemy Mateirals

    [Header("Enemy Materials")]
    [UnityEngine.SerializeField] protected Material ketchupMaterial;
    [UnityEngine.SerializeField] protected Material mustardMaterial;
    [UnityEngine.SerializeField] protected Material mayoMaterial;
    protected MeshRenderer enemyRenderer;

    #endregion

    #region Unity Events

    public UnityEvent m_OnDeath;

    #endregion

    private new void Start()
    {
        // Finding the Game Manager
        gameManager = UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();

        // Finding the HUD Manager
        hudManager = UnityEngine.GameObject.Find("HUDManager").GetComponent<HUDManager>();

        // Finding the firing position
        firingPosition = gameObject.transform.GetChild(0);

        // Getting the mesh renderer
        enemyRenderer = gameObject.transform.GetChild(1).GetComponent<MeshRenderer>();

        FindResources();

        // Setting the type of enemy
        int rand = UnityEngine.Random.Range(0, 3);
        enemyType = (SauceType)rand;

        SettingTheEnemyMaterial();

        healthBar = this.gameObject.transform.GetChild(2).GetChild(0);

        foreach(Transform element in healthBar)
        {
            healthBarElements.Add(element);
        }




        if (displayHealthBar == true)
        {
            
        }

        UnityEventSetup();


        base.Start();
    }

    /// <summary>
    /// Setting up the Unity Events on the Enemy
    /// </summary>
    private void UnityEventSetup()
    {
        m_OnDeath = new UnityEvent();
        m_OnDeath.AddListener(() => gameManager.Points += 100);
        m_OnDeath.AddListener(() => hudManager.UpdateCurrentPoints(gameManager.Points));
    }

    // Finding and loading the materials
    private void FindResources()
    {
        Debug.Log("Find Materials Function called");

        ketchupMaterial = Resources.Load("Materials/Mat_Ketchup", typeof(Material)) as Material;
        mustardMaterial = Resources.Load("Materials/Mat_Musturd", typeof(Material)) as Material;
        mayoMaterial = Resources.Load("Materials/Mat_Mayo", typeof(Material)) as Material;
    }

    // Setting up the enemy material, depending on their type
    protected void SettingTheEnemyMaterial()
    {
        switch (enemyType)
        {
            case SauceType.Ketchup:
                enemyRenderer.material = ketchupMaterial;
                break;
            case SauceType.Musturd:
                enemyRenderer.material = mustardMaterial;
                break;
            case SauceType.Mayo:
                enemyRenderer.material = mayoMaterial;
                break;
        }
    }

    // Updating the Health Bar of the enemy
    protected void UpdateHealthBar()
    {

        Debug.Log(health);

        for(int i = 0; i < healthBarElements.Count; i++)
        {
            if(health > i)
            {
                healthBarElements[i].transform.gameObject.SetActive(true);
            }
            else
            {
                healthBarElements[i].transform.gameObject.SetActive(false);
            }
        }

    }

    private new void Update()
    {
       
            // If the ship is no longer active, then this should no longer run
            if (isActive != false)
            {
                base.Update();
                UnityEngine.Debug.Log("Working");
            }
            else if (isActive == false)
            {
                transform.position = new Vector3(100, 100, 100);
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

    


    /// <summary>
    /// Applies damage to the enemy
    /// </summary>
    /// <param name="damage">How much damage to apply</param>
    public void ApplyDamage(float damage)
    {
        health -= damage;
    }

    public void ApplyDamageEnemy(float damage, SauceType bullet)
    {
        if(bullet == enemyType)
        {
            health -= damage;

            if (health != 0)
            {
                UpdateHealthBar();
            }
        }
     }
}
