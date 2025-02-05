using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour
{
    // UI References
    public Slider healthBar;
    public Image fillColor;
    public Gradient fillGradient;

    // Explosion Variables
    public GameObject explosion;
    public AudioClip enemyExplosion;

    // Enemy Variables
    public int health;

    private void Start()
    {
        // Initialize health value, dependent on enemy type
        if (gameObject.CompareTag(GameManager.gm.enemyList[0].tag) || gameObject.CompareTag(GameManager.gm.enemyList[1].tag)) health = 2;
        else if (gameObject.CompareTag(GameManager.gm.enemyList[2].tag) || gameObject.CompareTag(GameManager.gm.enemyList[3].tag)) health = 3;
        else if (gameObject.CompareTag(GameManager.gm.enemyList[4].tag)) health = 4;

        // Initialize health bar UI
        healthBar = transform.GetChild(0).GetChild(0).GetComponent<Slider>();    
        healthBar.maxValue = health;
        healthBar.value = health;
    }
    private void Update()
    {
        // Update health bar value and color 
        healthBar.value = health;
        fillColor.color = fillGradient.Evaluate(healthBar.normalizedValue);

        if (health <= 0)                                                    // If the enemy runs out of health, destroy it and create an explosion
        {
            Destroy(gameObject);
            Instantiate(explosion, transform.position, transform.rotation);
            AudioSource.PlayClipAtPoint(enemyExplosion, transform.position);

            // Increment the kill count/player score, dependent on game mode
            if (SceneManager.GetActiveScene().buildIndex == 1) GameManager.gm.enemiesKilled++;
            else if (SceneManager.GetActiveScene().buildIndex == 2) GameManager.gm.IncreaseScore(gameObject.tag);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the enemy collides with ammo, decrement health accordingly and destroy the enemy
        if (gameObject.CompareTag("Light Ammo"))
        {
            health -= GameManager.gm.lightAmmoPower;
            Destroy(other.gameObject);
        }
        else if (gameObject.CompareTag("Heavy Ammo"))
        {
            health -= GameManager.gm.heavyAmmoPower;
            Destroy(other.gameObject);
        }
    }
}