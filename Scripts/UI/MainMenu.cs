using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // UI References
    [SerializeField] private GameObject startScreenMenu;
    [SerializeField] private GameObject levelSelectMenu;
    [SerializeField] private GameObject controlsMenu;

    private void Start()
    {
        ShowMenu(startScreenMenu);                                          // Reset UI with the 'Start' screen
    }

    // Updates UI to show 'menu' and disables all other menus
    public void ShowMenu(GameObject menu)
    {
        if (menu == startScreenMenu)                                        // Updates UI to show the 'Start' screen
        {
            levelSelectMenu.SetActive(false);
            controlsMenu.SetActive(false);
            startScreenMenu.SetActive(true);
        }
        else if (menu == levelSelectMenu)                                   // Updates UI to show the 'Level Select' screen
        {
            startScreenMenu.SetActive(false);
            controlsMenu.SetActive(false);
            levelSelectMenu.SetActive(true);
        }
        else if (menu == controlsMenu)                                      // Updates UI to show the 'Controls' screen
        {
            startScreenMenu.SetActive(false);
            levelSelectMenu.SetActive(false);
            controlsMenu.SetActive(true);
        }
    }

    // Updates UI to show the 'Start' screen
    public void StartScreen()
    {
        ShowMenu(startScreenMenu);
    }

    // Updates UI to show the 'Level Select' screen
    public void LevelSelect()
    {
        ShowMenu(levelSelectMenu);
    }

    // Updates UI to show the 'Controls' screen
    public void Controls()
    {
        ShowMenu(controlsMenu);
    }

    // Loads the 'Stranded' game mode
    public void Stranded()
    {
        SceneManager.LoadScene("Stranded Level");
    }

    // Loads the 'Endless' game mode
    public void Endless()
    {
        SceneManager.LoadScene("Endless Level");
    }

    // Quits the game
    public void QuitGame()
    {
        Application.Quit();
    }
}