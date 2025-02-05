using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour
{
    // UI References
    public Slider playerHealth;

    // Enemy Variables
    public Vector3 destination;
    public GameObject explosion;

    public int attackDamage;

    // Flags
    public bool arrived = false;

    private void Start()
    {
        // Initialize starting destination, dependent on enemy type
        if (GameManager.gm.enemyList[0].CompareTag(gameObject.tag)) destination = transform.position + new Vector3(0.0f, 5.0f, 0.0f);
        else if (GameManager.gm.enemyList[1].CompareTag(gameObject.tag)) destination = transform.position + new Vector3(0.0f, 8.0f, 0.0f); 
        else if (GameManager.gm.enemyList[2].CompareTag(gameObject.tag)) destination = transform.position + new Vector3(0.0f, 10.0f, 0.0f); 
        else if (GameManager.gm.enemyList[3].CompareTag(gameObject.tag)) destination = transform.position + new Vector3(0.0f, 12.0f, 0.0f); 
        else if (GameManager.gm.enemyList[4].CompareTag(gameObject.tag)) destination = transform.position + new Vector3(0.0f, 14.0f, 0.0f); 

        transform.LookAt(destination);                                      // Rotate the enemy to face its destination
    }

    private void Update()
    {
        if (!arrived)                                                       // If the enemy has not arrived at its destination, translate it towards the destination
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, 0.05f);
            
            if (transform.position == destination) arrived = true;          // Update flag to show the enemy has reached its destination
        }
        else
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)              // 'Stranded' game mode
            {
                if (GameManager.gm.numTowers == 0)                          // If there are no current towers, freeze the enemy in midair
                {
                    if (gameObject.GetComponent<Rigidbody>() != null) Destroy(gameObject.GetComponent<Rigidbody>());
                    
                    transform.LookAt(transform.forward);
                    transform.SetParent(GameObject.FindGameObjectWithTag("Enemies").transform);
                }
                else if (GameManager.gm.numTowers > 0)                      // If there is at least one tower, set the enemy's destination to the nearest one
                {
                    if (gameObject.GetComponent<Rigidbody>() == null)
                    {
                        gameObject.AddComponent<Rigidbody>();
                        gameObject.GetComponent<Rigidbody>().useGravity = false;
                    }
            
                    // Find the closest tower to the enemy
                    int minIndex = 0;
                    GameObject closest = GameManager.gm.towers[minIndex];

                    for (int i = 0; i < GameManager.gm.numTowers; i++)
                    {
                        if (Vector3.Distance(transform.position, GameManager.gm.towers[i].transform.position) < Vector3.Distance(transform.position, GameManager.gm.towers[minIndex].transform.position))
                        {
                            minIndex = i;
                            closest = GameManager.gm.towers[i];
                        }
                    }

                    // Rotate the enemy to face the destination and move it towards the destination
                    transform.LookAt(closest.transform);

                    float distance = Vector3.Distance(transform.position, closest.transform.position);
                    if (distance > 0) transform.position = Vector3.MoveTowards(transform.position, closest.transform.position, 0.05f);
                    
                }
            }
            else                                                            // 'Endless' game mode
            {
                // Change destination to the player character
                Transform target = GameObject.FindWithTag("Player").transform;

                // Rotate the enemy to face the destination and move it towards the destination
                transform.LookAt(target);

                float distance = Vector3.Distance(transform.position, target.position);
                if (distance > 0) transform.position = Vector3.MoveTowards(transform.position, target.position, 0.05f);
            }
            
        }
    }

    private void OnCollisionEnter(Collision other) 
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            // If the enemy collides with the player, decrement player health and destroy the enemy
            if (other.gameObject.CompareTag("Player"))
            {
                playerHealth = GameObject.FindWithTag("Victory Progress Bar").GetComponent<Slider>();
                playerHealth.value -= attackDamage;
                Instantiate(explosion, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            // If the enemy collides with the tower, decrement tower health and destroy the enemy
            if (other.gameObject.CompareTag("Tower"))
            {
                other.gameObject.GetComponent<TowerHealth>().health -= attackDamage;
                Instantiate(explosion, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }
}