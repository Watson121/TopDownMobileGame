using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    #region Managers

    private UIManager uiManager;
    private PlayerController player;

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
        get { return NumberOfGearsCollected; }
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

    #endregion

    // Start is called before the first frame update
    void Start()
    {
       
        GetBullets();
        FindManagers();

        OnPointChange += PointUpdateHandler;
        OnGearCollection += GearUpdateHandler;

        Time.timeScale = 1.0f;

        // DontDestroyOnLoad(this);
    }

    private void FindManagers()
    {
        // Finding UI Manager
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();

        // Finding the player Controller
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void GetBullets()
    {
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
        uiManager.UpadateGearsCollection(numOfGearsCollected); 
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

    }

    #endregion
}
