using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

[DisallowMultipleComponent]
public class GameManager : MonoBehaviour
{

    #region Stopping Multiple Instances of the Game Manager

    public static GameManager Instance
    {
        get
        {
            return instance;
        }
        set
        {
            if (instance != null)
            {
                Destroy(value.gameObject);
                return;
            }

            instance = value;
        }
    }
    private static GameManager instance;

    #endregion

    #region Player

    [SerializeField] private IComponent[] playerComponents;

    #endregion

    #region Managers

    private UIManager uiManager;
    private SpawningManager spawningManager;
    private SaveManager saveManager;

    public LevelManager LevelManager
    {
        get { return levelManager; }
    }
    private LevelManager levelManager;

    #endregion

    #region Points

    [Header("Player Points")]
    // Current highscore
    [SerializeField] private static uint highScore;

    public uint HighScore
    {
        get { return highScore; }
    }

    // Current number of points that the player has
    [SerializeField]
    private uint currentPoints = 0;
    
    // Property to set and return points
    public uint Points
    {
        set { 
            currentPoints = value;
        }
        get { 
            return currentPoints; 
        }
    }

    // Events for when the points change
    public delegate void OnPointChangeDelegate(uint newVal);
    public event OnPointChangeDelegate OnPointChange;

    #endregion

    #region Gears
    // These are the number of gears that the player has currently collected, these will be used to upgrade their weapons and ship.
    public int NumberOfGearsCollected
    {


        set
        {
            numOfGearsCollected = value;

           
                
                //uiManager.SetGearsUpgradeScreen();

        }
        get 
        {

            if (unlimitedMoney)
            {
                return 100000;
            }else
            {
                return numOfGearsCollected;
            }
            
        }
    }
    [SerializeField]private int numOfGearsCollected = 0;

    // Events for when gears have been collected
    public delegate void OnGearCollectedDelegate(int newVal);
    public event OnGearCollectedDelegate OnGearCollection;

    #endregion

    #region Upgrade System

    public int HealthLevel
    {
        set { healthLevel = value; }
        get { return healthLevel; }
    }

    public int WeaponLevel 
    {
        set { weaponLevel = value; }
        get { return weaponLevel; }
    }

    public int ShieldLevel
    {
        set { shieldLevel = value; }
        get { return shieldLevel; }
    }

    [SerializeField] private int healthLevel;
    [SerializeField] private int weaponLevel;
    [SerializeField] private int shieldLevel;

    #endregion

    #region Bullet Pools

    public List<Bullet> PlayerBulletPool
    {
        get { return playerBulletPool; }
    }
    [SerializeField] private List<Bullet> playerBulletPool;

    public List<Bullet> EnemyBulletPool
    {
        get { return enemyBulletPool; }
    }
    [SerializeField]private List<Bullet> enemyBulletPool;

    // Current Active Bullets in the level
    [SerializeField] private List<Bullet> activeBullets;

    #endregion

    #region Saves

    public SaveData playerSave;

    #endregion

    #region Debugging

    public bool UnlimitedMoney
    {
        get { return unlimitedMoney; }
    }

    [SerializeField] private bool unlimitedMoney = false;

    #endregion

    #region Unity Events

    public UnityEvent m_OnRestart;



    #endregion

    public Level CurrentLevel
    {
        get { return currentLevel; }
        set
        {
            currentLevel = value;
        }
    }
    [SerializeField] private Level currentLevel;


    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

  

        OnGearCollection += GearUpdateHandler;

        Time.timeScale = 1.0f;

        SceneManager.sceneLoaded += OnSceneLoaded;

        // Finding the level manager
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();

        // Find the first level
        currentLevel = levelManager.levels[0];

        DontDestroyOnLoad(this);
    }

    private void UnityEventSetup()
    {
        m_OnRestart = new UnityEvent();

        for(int i = 0; i < playerComponents.Length; i++)
        {
            m_OnRestart.AddListener(playerComponents[i].ResetComponent);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainScene")
        {
            SetupGame();
        }
        else
        {
            // Finding UI Manager
            uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
            
        
        }
    }

    public void SetupGame()
    {
        GetBullets();
        FindManagers();
        UnityEventSetup();
    }

    private void FindManagers()
    {

        // Finding the player Controller
        //player = GameObject.Find("Player").GetComponent<PlayerController>();

        GameObject player = GameObject.Find("Player");
        playerComponents = player.GetComponents<IComponent>();

        // Finding the spawning manager
        spawningManager = GameObject.Find("SpawningManager").GetComponent<SpawningManager>();

      

    }

    private void GetBullets()
    {

        // Clearing the pools
        playerBulletPool.Clear();
        enemyBulletPool.Clear();
        activeBullets.Clear();

        // Finding the player pool
        Transform playerPool = GameObject.FindGameObjectWithTag("PlayerBulletPool").transform;

        foreach (Transform bullet in playerPool)
        {
            playerBulletPool.Add(bullet.GetComponent<Bullet>());
        }

        Transform enemyPool = GameObject.FindGameObjectWithTag("EnemyBulletPool").transform;

        foreach (Transform bullet in enemyPool)
        {
            enemyBulletPool.Add(bullet.GetComponent<Bullet>());
        }
    }


    #region Active Bullets

    /// <summary>
    ///  Adding an active bullet to the pool
    /// </summary>
    /// <param name="bullet"> Bullet to be added </param>
    public void AddActiveBullet(Bullet bullet)
    {
        Debug.Log("Bullet Added to the active pool");

        activeBullets.Add(bullet); 
    }

    /// <summary>
    /// Removing a bullet from the active pool
    /// </summary>
    /// <param name="bullet"></param>
    public void RemoveActiveBullet(Bullet bullet)
    {
        Debug.Log("Bullet removed from the active pool");

        activeBullets.Remove(bullet);
    }


    #endregion

    #region Points System

    private void ResetPoints()
    {
        if(currentPoints > highScore)
        {
            highScore = currentPoints;
        }

        currentPoints = 0;
    }

    public void PointUpdateHandler(uint newVal)
    {
        currentPoints += newVal;
        //uiManager.UpdateCurrentPoints(currentPoints);
        
    }

    public void HighScoreUpdateHandler(uint newVal)
    {
        if(currentPoints > highScore)
        {
          
            highScore = currentPoints;
            //uiManager.UpdateHighScore(highScore);
        }

    }

    #endregion

    #region Upgrade System

    public void GearUpdateHandler(int newVal)
    {
        NumberOfGearsCollected += newVal;
    }

 

    #endregion

    #region Reseting Level & Saving

    /// <summary>
    /// Reseting the level 
    /// </summary>
    public void ResetLevel(bool restart = false)
    {
        // Setting the Time Scale back to normal speed
        Time.timeScale = 1;

        // Reseting the level
        m_OnRestart.Invoke();

        // Reseting the enemies
        spawningManager.ResetEnemies();

        // Reseting the colletables
        spawningManager.ResetCollectables();

        if (restart)
        {
            // Clearing the pools
            playerBulletPool.Clear();
            enemyBulletPool.Clear();
        }

        // Reseting the active bullets
        foreach (Bullet bullet in activeBullets.ToList())
        {
            // If the bullet is moving it needs to be reset
            if (bullet.BulletMoving == true)
            {
                bullet.ResetBullet();
            }
        }
    }


    #endregion

   

}
