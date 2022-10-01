using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private UIManager uiManager;

    #region Points

    // Current highscore
    private static uint highScore;

    // Current number of points that the player has
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


        DontDestroyOnLoad(this);
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

    private void PointUpdateHandler(uint newVal)
    {
        uiManager.UpdaetCurrentPoints(newVal);
    }

    private void HighScoreUpdateHandler(uint newVal)
    {
        
    }
    


}