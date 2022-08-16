using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private UIManager uiManager;

    #region Points

    private static uint highScore;
    private uint points = 0;
    public uint Points
    {
        set { 
            points = value;
            OnPointChange(points);
        }
        get { 
            return points; 
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
        if(points > highScore)
        {
            highScore = points;
        }

        points = 0;
    }

    private void PointUpdateHandler(uint newVal)
    {
        uiManager.UpdatePoints(newVal);
    }
    


}
