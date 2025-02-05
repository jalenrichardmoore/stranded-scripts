using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Static Reference
    public static GameManager gm;

    // Prefab References
    public GameObject [] enemyList;
    public GameObject towerPrefab;
    public GameObject victoryScreen;
    public GameObject gameOverScreen;
    public GameObject scoreScreen;
    public GameObject backgroundScreen;
    
    // UI References
    public Slider towerProgressBar;
    public TMP_Text timerText;
    public TMP_Text ammoText;
    public TMP_Text buildText;
    public TMP_Text scoreText;
    public TMP_Text scoreScreenText;

    // Component Reference
    public FireAmmo fireAmmo;

    // Management Variables
    public GameObject [] towers;

    public float timeRemaining = 180.0f;

    public int numTowers;
    public int enemiesKilled;
    public int lightAmmoPower = 1;
    public int heavyAmmoPower = 2;
    public int score = 0;

    // Flags
    public bool gameOver;
    public bool timerIsRunning = true;

    private void Start()
    {
        // Initialize variables 
        gm = this.gameObject.GetComponent<GameManager>();
        enemiesKilled = 0;
        gameOver = false;
        buildText.enabled = false;
        
        // Initialize list of towers
        towers = GameObject.FindGameObjectsWithTag("Tower");
        numTowers = towers.Length;
        
        // Reset UI
        backgroundScreen.SetActive(false);
        victoryScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        
        if (SceneManager.GetActiveScene().buildIndex == 2) scoreScreen.SetActive(false);
    }
        
    private void Update()
    {
        towerProgressBar.value = enemiesKilled;                             // Update tower progress bar value

        if (towerProgressBar.value == towerProgressBar.maxValue)            // If the tower progress bar is full, update UI
        {
            buildText.enabled = true;

            if (Input.GetKey(KeyCode.E))                                    // If the 'E' key is pressed, construct a new tower
            {
                // Check if the location of the new tower is far enough from all other towers before instantiating
                Vector3 spawnPoint = GameObject.FindWithTag("Player").transform.position - new Vector3(0.0f, -2.0f, 2.0f);
                for (int i = 0; i < numTowers; i++)
                {
                    if (Vector3.Distance(spawnPoint, towers[i].transform.position) <= 5) return;
                }

                Instantiate(towerPrefab, spawnPoint, transform.rotation);
                
                // Update UI and reset counter
                buildText.enabled = false;
                enemiesKilled = 0;
            }
        }

        // Update list of towers
        towers = GameObject.FindGameObjectsWithTag("Tower");
        numTowers = towers.Length;

        //  Update ammo UI
        if (fireAmmo.currentAmmo == FireAmmo.AmmoType.LIGHT) ammoText.text = "Light Ammo: " + fireAmmo.lightAmmoCount + " / " + fireAmmo.maxLightAmmo;
        else if (fireAmmo.currentAmmo == FireAmmo.AmmoType.HEAVY) ammoText.text = "Heavy Ammo: " + fireAmmo.heavyAmmoCount + " / " + fireAmmo.maxHeavyAmmo;

        if (SceneManager.GetActiveScene().buildIndex == 1)                  // 'Stranded' game mode
        {
            if (timeRemaining > 0)                                          // If there is time remaining in the game, decrement timer and update UI
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else                                                            // If there is no time remaining, update flag and end the game
            {
                gameOver = true;
                timeRemaining = 0;
                timerIsRunning = false;
                GameOver();
            }
        }

        // Update UI in 'Endless' game mode
        if (SceneManager.GetActiveScene().buildIndex == 2) scoreText.text = "Score: " + score; 
    }

    // Formats and updates the timer UI
    private void DisplayTime(float timeToDisplay) 
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);               // Calculates the number of remaining minutes
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);               // Calculates the number of remaining seconds

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);  // Updates UI text
    }

    // Increases the player score in 'Endless' game mode, dependent on enemy type
    public void IncreaseScore(string tag) 
    {
        if (GameManager.gm.enemyList[0].CompareTag(tag)) score += 5;
        else if (GameManager.gm.enemyList[1].CompareTag(tag)) score += 10;
        else if (GameManager.gm.enemyList[2].CompareTag(tag)) score += 15;
        else if (GameManager.gm.enemyList[3].CompareTag(tag)) score += 20;
        else if (GameManager.gm.enemyList[4].CompareTag(tag)) score += 25;
    }

    // Updates the UI with the 'Game Over' screen
    public void GameOver() 
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)                  // If in 'Stranded' game mode, display the 'Game Over' screen
        {
            backgroundScreen.SetActive(true);
            gameOverScreen.SetActive(true);
        }
        else                                                                // If in 'Endless' game mode, display the 'Score' screen
        {
            backgroundScreen.SetActive(true);
            scoreScreen.SetActive(true);
            scoreScreenText.text = "Your Score: \n " + score;
        }
    }

    // Updates the UI with the 'Victory' screen
    public void Victory()
    {
        backgroundScreen.SetActive(true);
        victoryScreen.SetActive(true);
    }

    // Loads the Title Screen
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    // Reloads the current level
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}