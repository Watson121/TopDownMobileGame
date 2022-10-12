using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawningManager : MonoBehaviour
{
    [Header("Enemy Spawning Settings")]
    [SerializeField] private int minX, maxX;
    [SerializeField] private int minY, maxY;
    [SerializeField] private float spawnTimer = 4.0f;
    [SerializeField] private List<GameObject> baseEnemies;
    [SerializeField] BaseEnemy enemyToSpawn;
    private static int enemyIndex = 0;

    [Header("Collectable Spawning Settings")]
    [SerializeField] private List<GameObject> baseCollectables;
    [SerializeField] private Collectable collectableToSpawn;
    private static int collectableIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
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
    }

    private void FindCollectables()
    {
        // Finding the pool of Collectables
        Transform collectablePool = GameObject.FindGameObjectWithTag("CollectablePool").transform;

        // Putting the collectables into the collectable pool
        foreach (Transform collectable in collectablePool)
        {
            baseCollectables.Add(collectable.gameObject);
        }
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
        enemyToSpawn = baseEnemies[enemyIndex].AddComponent<CroutonShip>();

        if(enemyToSpawn.IsActive != true)
        {
            enemyToSpawn.gameObject.transform.position = spawnPoint;
        }

        enemyIndex++;

        if (enemyIndex >= baseEnemies.Count)
        {
            enemyIndex = 0;
        }

    }

    private IEnumerator EnemyCountdown()
    {
        yield return new WaitForSeconds(spawnTimer);
        SpawnEnemy(NewSpawnPoint());
        StartCoroutine(EnemyCountdown());
    }

    #endregion

    /// <summary>
    /// Spawning a collectable into the world
    /// </summary>
    /// <param name="spawnPoint"> The Position that the collectable will spawn at </param>
    public void SpawnCollectable(Vector3 spawnPoint)
    {
        collectableToSpawn = baseCollectables[collectableIndex].AddComponent<MoneyCoin>();

        if(collectableToSpawn.IsActive != true)
        {
            collectableToSpawn.gameObject.transform.position = spawnPoint + new Vector3(0, 1, 0);
            StartCoroutine(collectableToSpawn.CollectableMovement());
        }

        collectableIndex++;

        if (collectableIndex >= baseCollectables.Count)
        {
            collectableIndex = 0;
        }
    }

}
