using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningManager : MonoBehaviour
{
    [Header("Spawning Manager Settings")]
    [SerializeField] private int minX, maxX;
    [SerializeField] private int minY, maxY;
    [SerializeField] private float spawnTimer = 4.0f;

    [SerializeField] private List<GameObject> baseEnemies;
    [SerializeField] BaseEnemy enemyToSpawn;
    private static int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        FindEnemies();

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


    // Update is called once per frame
    void Update()
    {
        
    }

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
        enemyToSpawn = baseEnemies[index].AddComponent<CroutonShip>();

        if(enemyToSpawn.IsActive != true)
        {
            enemyToSpawn.gameObject.transform.position = spawnPoint;
            //enemyToSpawn.ResetEnemy();
        }

        index++;

        if (index >= baseEnemies.Count)
        {
            index = 0;
        }

    }

    private IEnumerator EnemyCountdown()
    {
        yield return new WaitForSeconds(spawnTimer);
        SpawnEnemy(NewSpawnPoint());
        StartCoroutine(EnemyCountdown());
    }

}
