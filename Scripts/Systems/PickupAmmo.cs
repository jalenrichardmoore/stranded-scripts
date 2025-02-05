using UnityEngine;

public class PickupAmmo : MonoBehaviour
{
    // Component Reference
    public FireAmmo fireAmmo;

    // Ammo Variables
    public AudioClip collectAmmo;

    private void Start()
    {
        // Initialize variables
        fireAmmo = GameObject.FindWithTag("Player").transform.GetChild(0).GetComponent<FireAmmo>();
    }

    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.CompareTag("Player"))                          // If the player collides with the ammo, increment the appropriate ammo count
        {
            switch (gameObject.tag)
            {
                case "Light Ammo":
                    fireAmmo.lightAmmoCount = Mathf.Min(fireAmmo.maxLightAmmo, fireAmmo.lightAmmoCount + 15);
                    break;
                case "Heavy Ammo":
                    fireAmmo.heavyAmmoCount = Mathf.Min(fireAmmo.maxHeavyAmmo, fireAmmo.heavyAmmoCount + 10);
                    break;
            }

            AudioSource.PlayClipAtPoint(collectAmmo, this.transform.position);
            Destroy(gameObject);

            // Update flag and timer in spawn point for the next ammo to spawn
            SpawnAmmo spawnAmmo = GetComponentInParent<SpawnAmmo>();
            spawnAmmo.timeUntilSpawn = Time.time + spawnAmmo.spawnInterval;
            spawnAmmo.ammoSpawnIsThere = false;
        }
    }
}