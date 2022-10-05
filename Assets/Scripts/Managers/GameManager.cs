using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private UIManager uiManager;

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

        Time.timeScale = 1.0f;

        // DontDestroyOnLoad(this);
    }

    private void FindManagers()
    {
        // Finding UI Manager
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
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

    public void HighScoreUpdateHandler()
    {
        if(currentPoints > highScore)
        {
          
            highScore = currentPoints;
            uiManager.UpdateHighScore(highScore);
        }

    }
    


}
