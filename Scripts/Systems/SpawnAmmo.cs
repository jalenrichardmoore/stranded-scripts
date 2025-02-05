using UnityEngine;

public class SpawnAmmo : MonoBehaviour
{
    // Spawn Point Variables
    public GameObject [] ammo;
    public GameObject newAmmo;

    public float spawnInterval = 9.0f;
    public float timeUntilSpawn;

    // Flags
    public bool ammoSpawnIsThere = false;

    private void Start()
    {
        timeUntilSpawn = Time.time + spawnInterval;                         // Initialize the interval between ammo spawns
    }

    private void Update()
    {
        if (!GameManager.gm.gameOver)
        {
            if (Time.time > timeUntilSpawn && !ammoSpawnIsThere)            // If it is time to spawn a new ammo and no ammo is currently there, spawn a random new ammo
            {
                int nextAmmo = Random.Range(0, 2);
                newAmmo = Instantiate(ammo[nextAmmo], transform.position, transform.rotation);
                newAmmo.transform.SetParent(this.transform);
                
                ammoSpawnIsThere = true;                                    // Update flag to show an ammo is currently there
            }
        }
    }
}