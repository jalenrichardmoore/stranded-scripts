using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{   
    // Spawn Point Variables
    private float minSpawnTime = 5.0f;
    private float maxSpawnTime = 10.0f;
    private float timeUntilSpawn;

    private void Start()
    {
        // Initialize the interval between enemy spawns
        timeUntilSpawn = Time.time + Random.Range(minSpawnTime, maxSpawnTime);
    }

    private void Update()
    {
        if (!GameManager.gm.gameOver)
        {
            if (Time.time > timeUntilSpawn)                                 // If it is time to spawn a new enemy, initialize a random enemy type
            {
                // Generate a random number and select an enemy type based on that enemy type
                int spawnIndex = 0;
                int x = Random.Range(0, 100);

                if (x <= 20) spawnIndex = 0;
                else if (x <= 40) spawnIndex = 1;
                else if (x <= 70) spawnIndex = 2;
                else if (x <= 90) spawnIndex = 3;
                else if (x <= 100) spawnIndex = 4;

                Instantiate(GameManager.gm.enemyList[spawnIndex], transform.position, transform.rotation);            
                
                // Update interval until next enemy spawn
                timeUntilSpawn = Time.time + Random.Range(minSpawnTime, maxSpawnTime);
            }
        }
    }
}