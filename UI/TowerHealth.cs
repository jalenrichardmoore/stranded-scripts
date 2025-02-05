using UnityEngine;
using UnityEngine.UI;

public class TowerHealth : MonoBehaviour
{
    // UI References
    public Image fillColor;
    public Gradient fillGradient;
    public Slider healthBar;

    // Tower Variables
    public GameObject explosion;
    public AudioClip towerExplosion;

    public int health;

    private void Start()
    {
        // Initialize variables
        health = 50;

        // Initialize health bar UI
        healthBar = transform.GetChild(0).GetChild(0).GetComponent<Slider>();
        healthBar.value = health;

    }

    private void Update()
    {
        // Update health bar value and color
        healthBar.value = health;
        fillColor.color = fillGradient.Evaluate(healthBar.normalizedValue);

        if (health <= 0)                                                    // If the tower runs out of health, destroy it and create an explosion
        {
            Destroy(gameObject);
            Instantiate(explosion, transform.position, transform.rotation);
            AudioSource.PlayClipAtPoint(towerExplosion, transform.position);
        }
    }
}