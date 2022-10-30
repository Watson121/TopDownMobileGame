using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    #region Managers

    private UIManager uiManager;
    private PlayerController player;
    private SpawningManager spawningManager;

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
            OnPointChange(currentPoints);
        }
        get { 
            return currentPoints; 
        }
    }

    // Events for when the points change
    public delegate void OnPointChangeDelegate(uint newVal);
    public event OnPointChangeDelegate OnPointChange;

    #endregion

    #region Upgrade System

    // These are the number of gears that the player has currently collected, these will be used to upgrade their weapons and ship.
    public uint NumberOfGearsCollected
    {
        get { return numOfGearsCollected; }
    }
    private uint numOfGearsCollected = 0;

    // Events for when gears have been collected
    public delegate void OnGearCollectedDelegate(uint newVal);
    public event OnGearCollectedDelegate OnGearCollection;

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

    // Start is called before the first frame update
    void Start()
    {
        OnPointChange += PointUpdateHandler;
        OnGearCollection += GearUpdateHandler;

        Time.timeScale = 1.0f;

        SceneManager.sceneLoaded += OnSceneLoaded;

        DontDestroyOnLoad(this);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainScene")
        {
            SetupGame();
        }
    }

    public void SetupGame()
    {
        GetBullets();
        FindManagers();
    }

    private void FindManagers()
    {
        // Finding UI Manager
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();

        // Finding the player Controller
        player = GameObject.Find("Player").GetComponent<PlayerController>();

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
        uiManager.UpdateCurrentPoints(currentPoints);
        
    }

    public void HighScoreUpdateHandler(uint newVal)
    {
        if(currentPoints > highScore)
        {
          
            highScore = currentPoints;
            uiManager.UpdateHighScore(highScore);
        }

    }

    #endregion

    #region Upgrade System

    public void GearUpdateHandler(uint newVal)
    {
        numOfGearsCollected += newVal;
        uiManager.UpdateGearCollection(numOfGearsCollected); 
    }

    #endregion

    #region Reseting Level & Saving

    /// <summary>
    /// Reseting the level 
    /// </summary>
    public void ResetLevel()
    {
        // Setting the Time Scale back to normal speed
        Time.timeScale = 1;

        // Reseting the player
        player.PlayerReset();

        // Reseting the enemies
        spawningManager.ResetEnemies();

        // Reseting the colletables
        spawningManager.ResetCollectables();

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
