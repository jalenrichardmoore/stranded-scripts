using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class LevelSelect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // UI Reference
    [SerializeField] private TMP_Text howToPlay;

    public void OnPointerEnter(PointerEventData pointer)
    {
        if (this.gameObject.name == "Stranded Button")                      // If the cursor is hovering over 'Stranded,' display the 'Stranded' game mode description
        {
            howToPlay.text = "You have been stranded on a deserted island, alone. " +
                             "Defeat the monsters emerging from the ocean to collect materials and construct a watchtower. " +
                             "Defend that tower long enough to signal for help and escape.";
        }
        else if (this.gameObject.name == "Endless Button")                  // If the cursor is hovering over 'Endless,' display the 'Endless' game mode description
        {
            howToPlay.text = "The inhabitants of the island have begun to fight back. " + 
                             "Defeat as many of them as you can and aim for a high score.";
        }
    }

    public void OnPointerExit(PointerEventData pointer) 
    {
        howToPlay.text = "";                                                // If the cursor moves off a button, clear the description text
    }
}