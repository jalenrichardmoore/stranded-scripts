using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VictoryProgress : MonoBehaviour
{
    // UI References
    public Slider victoryProgress;

    // Progress Bar Variables
    public float updateTime = 1.0f;
    public float timeUntilUpdate;

    public int progressAmount = 1;

    private void Start()
    {
        // Initialize variables
        victoryProgress = GameObject.FindWithTag("Victory Progress Bar").GetComponent<Slider>();
        
        // Initialize update interval in 'Stranded' game mode
        if (SceneManager.GetActiveScene().buildIndex == 1) timeUntilUpdate = Time.time + updateTime;

        // Initialize progress bar value in 'Endless' game mode
        if (SceneManager.GetActiveScene().buildIndex == 2) victoryProgress.value = victoryProgress.maxValue;
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)                  // 'Stranded' game mode
        {
            if (Time.time > timeUntilUpdate)                                // If it is time to update the progress bar, update UI and calculate interval to next update
            {
                victoryProgress.value += progressAmount;
                timeUntilUpdate = Time.time + updateTime;
            }

            if (victoryProgress.value >= victoryProgress.maxValue)          // If the progress bar is filled, end the game and update UI to display the 'Victory' screen
            {
                GameManager.gm.gameOver = true;
                GameManager.gm.Victory();
            }   
        }
        else                                                                // 'Endless' game mode
        {
            if (victoryProgress.value <= 0)                                 // If the progress bar is depleted, end the game and update UI to display the 'Score' screen
            {
                GameManager.gm.gameOver = true;
                GameManager.gm.GameOver();
            }
        }        
    }
}