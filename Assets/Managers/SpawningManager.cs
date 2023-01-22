using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawningManager : MonoBehaviour
{

    #region Enemy Spawning Settings

    [Header("Enemy Spawning Settings")]
    [SerializeField] private int minX, maxX;
    [SerializeField] private int minY, maxY;
    [SerializeField] private float spawnTimer = 6.0f;
    [SerializeField] private List<GameObject> baseEnemies;
    [SerializeField] BaseEnemy enemyToSpawn;
    private static int enemyIndex = 0;

    // Enemy Numbers
    [SerializeField] private List<EEnemyType> enemyTypesToSpawn = new List<EEnemyType>();

    [SerializeField] private int croutonShips = 0;
    [SerializeField] private int colourChangingShips = 0;

    #endregion

    #region Collectable Spawning Settings

    [Header("Collectable Spawning Settings")]
    [SerializeField] private List<GameObject> gearsCollectables;
    [SerializeField] private List<GameObject> shieldCollectables;
    [SerializeField] private List<GameObject> healthCollectables;
    [SerializeField] private Collectable collectableToSpawn;
    private static int gearIndex = 0;
    private static int shieldIndex = 0;
    private static int healthIndex = 0;

    #endregion

    #region Debugging

    [Header("Debugging - Collectables")]
    [SerializeField] private bool dropOnlyHealth = false;
    [SerializeField] private bool dropOnlyShields = false;
    [SerializeField] private bool dropOnlyGears = false;

    [Header("Debugging - Enemy Spawning")]
    [SerializeField] private bool onlySpawnCroutonShip = false;
    [SerializeField] private bool onlySpawnColourSwitching = false;

    #endregion

    #region Managers

    private GameManager gameManager;
    private LevelManager levelManager;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        GetManagers();
        FindEnemies();
        FindCollectables();
        
        

        StartCoroutine(EnemyCountdown());
    }

    private void FindEnemies()
    {
        // Finding the pool of enemies
        Transform enemyPool = GameObject.FindGameObjectWithTag("EnemyPool").transform;

        // Putting the enemies into the enemy pool
        foreach (Transform enemy in enemyPool)
        {
            baseEnemies.Add(enemy.gameObject);
        }

        SettingEnemyNumbers();

        enemyTypesToSpawn.Add(EEnemyType.CroutonShip);
        enemyTypesToSpawn.Add(EEnemyType.ColourChangingShip);
    }

    private void FindCollectables()
    {
        // Finding the pool of Gears
        Transform gearPool = GameObject.FindGameObjectWithTag("GearPool").transform;

        // Putting the gears into the gear pool
        foreach (Transform gear in gearPool)
        {
            gearsCollectables.Add(gear.gameObject);
        }

        //Finding the pool of Shields
        Transform shieldPool = GameObject.FindGameObjectWithTag("ShieldPool").transform;

        // Putting the shields into the shield pool
        foreach (Transform shield in shieldPool)
        {
            shieldCollectables.Add(shield.gameObject);
        }

        // Finding the pool of Healths Packages
        Transform healthPool = GameObject.FindGameObjectWithTag("HealthPool").transform;

        // Adding the health packages to the health pool
        foreach (Transform health in healthPool)
        {
            healthCollectables.Add(health.gameObject);
        }
    }

    /// <summary>
    /// Getting the Managers, need for the Spawning Manager to function
    /// </summary>
    private void GetManagers()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        levelManager = gameManager.LevelManager;
    }

    /// <summary>
    /// Setting the number of enemies to spawn
    /// </summary>
    private void SettingEnemyNumbers()
    {
        croutonShips = gameManager.CurrentLevel.enemiesToSpawn[0].numberToSpawn;
        colourChangingShips = gameManager.CurrentLevel.enemiesToSpawn[1].numberToSpawn;
    }



    #region Enemy Spawning

    private Vector3 NewSpawnPoint()
    {
        Vector3 newSpawnPoint;

        int newX = (int)Random.Range(minX, maxX);
        int newY = (int)Random.Range(minY, maxY);

        newSpawnPoint = new Vector3(newX, newY, 13);

        return newSpawnPoint;
    }

    private void SpawnEnemy(Vector3 spawnPoint)
    {
        enemyToSpawn = DecideWhichEnemyToSpawn();

        if (enemyToSpawn != null)
        {

            if (enemyToSpawn.IsActive != true)
            {
                enemyToSpawn.gameObject.transform.position = spawnPoint;
            }

            enemyIndex++;

            if (enemyIndex >= baseEnemies.Count)
            {
                enemyIndex = 0;
            }
        }
        else
        {
            Debug.Log("All enemies defeated!");
        }

    }

    private BaseEnemy DecideWhichEnemyToSpawn()
    {
        BaseEnemy enemyToSpawn;

        if(croutonShips <= 0)
        {
            enemyTypesToSpawn.Remove(EEnemyType.CroutonShip);
        }

        if(colourChangingShips <= 0)
        {
            enemyTypesToSpawn.Remove(EEnemyType.ColourChangingShip);
        }

#if UNITY_EDITOR

        if (onlySpawnCroutonShip && croutonShips > 0)
        {
            enemyToSpawn = baseEnemies[enemyIndex].AddComponent<CroutonShip>();
            croutonShips--;
        }

        if(onlySpawnColourSwitching && colourChangingShips > 0)
        {
            enemyToSpawn = baseEnemies[enemyIndex].AddComponent<ColourChaningEnemy>();
            colourChangingShips--;
        }

#endif




        if (enemyTypesToSpawn.Count != 0)
        {

            EEnemyType randomEnemy = (EEnemyType)Random.Range(0, enemyTypesToSpawn.Count);

            switch (randomEnemy)
            {
                case EEnemyType.CroutonShip:
                    enemyToSpawn = baseEnemies[enemyIndex].AddComponent<CroutonShip>();
                    croutonShips--;
                    return enemyToSpawn;
                case EEnemyType.ColourChangingShip:
                    enemyToSpawn = baseEnemies[enemyIndex].AddComponent<ColourChaningEnemy>();
                    colourChangingShips--;
                    return enemyToSpawn;
            }

            

        }

        return null;

    }

    private IEnumerator EnemyCountdown()
    {
        yield return new WaitForSeconds(spawnTimer);
        SpawnEnemy(NewSpawnPoint());
        StartCoroutine(EnemyCountdown());
    }

    #endregion

    #region Collectable Spawning

    /// <summary>
    /// Spawning a collectable into the world
    /// </summary>
    /// <param name="spawnPoint"> The Position that the collectable will spawn at </param>
    public void SpawnCollectable(Vector3 spawnPoint)
    {

        CollectableType collectableType = GetACollectable();

        switch (collectableType)
        {
            case CollectableType.EMoney:
                collectableToSpawn = gearsCollectables[gearIndex].GetComponent<Gear>();
                break;
            case CollectableType.EShield:
                collectableToSpawn = shieldCollectables[shieldIndex].GetComponent<Shield>();
                break;
            case CollectableType.EHealth:
                collectableToSpawn = healthCollectables[healthIndex].GetComponent<HealthPackage>();
                break;
        }


        if (collectableToSpawn.IsActive != true)
        {
            collectableToSpawn.gameObject.transform.position = spawnPoint + new Vector3(0, 1, 0);
            StartCoroutine(collectableToSpawn.CollectableMovement());
        }

        switch (collectableType)
        {
            case CollectableType.EMoney:
                gearIndex++;

                if (gearIndex >= gearsCollectables.Count)
                {
                    gearIndex = 0;
                }

                return;
            case CollectableType.EShield:
                shieldIndex++;

                if(shieldIndex >= shieldCollectables.Count)
                {
                    shieldIndex = 0;
                }

                return;
            case CollectableType.EHealth:
                healthIndex++;

                if(healthIndex >= healthCollectables.Count)
                {
                    healthIndex = 0;
                }

                return;
        }

       
    }

    /// <summary>
    /// Function works out which collectable that will spawn
    /// </summary>
    /// <returns> Collectable to Spawn </returns>
    private CollectableType GetACollectable()
    {
   
    // For Debugging, should only run while in editor
#if UNITY_EDITOR

        if (dropOnlyShields)
        {
            return CollectableType.EShield;
        }

        if (dropOnlyGears)
        {
            return CollectableType.EMoney;
        }

        if (dropOnlyHealth)
        {
            return CollectableType.EHealth;
        }
#endif

        return (CollectableType)Random.Range(0, 2);
    }

    #endregion

    #region Level Reset

    /// <summary>
    /// Reseting the enemies
    /// </summary>
    public void ResetEnemies()
    {
        foreach(GameObject enemy in baseEnemies)
        {
            BaseEnemy e = enemy.GetComponent<BaseEnemy>();

            if (e)
            {
                e.IsActive = false;
            }
        }
    }

    /// <summary>
    /// Reseting the collectables within the world
    /// </summary>
    public void ResetCollectables()
    {
        foreach (GameObject collectable in gearsCollectables)
        {
            Collectable c = collectable.GetComponent<Collectable>();

            if (c)
            {
                c.ResetCollectable();
            }
        }
    }


    #endregion

}
