using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdjustInitial : MonoBehaviour
{

    public string mainMenu;

    // Start is called before the first frame update
    void Start()
    {
        // Read in Steam API values.

        // Use values in a formula to determine what the initial PlayerPref value should be.
        // A more negative difficulty value should be set for weaker players.
        // A more positive difficulty value should be set for stronger players.

        // Use default value for now.
        PlayerPrefs.SetInt("difficultyModifier", 0);
        PlayerPrefs.Save();

        // Transition to the main menu.
        Debug.Log("Setting difficulty.");
        int currentDifficulty = PlayerPrefs.GetInt("difficultyModifier");
        Debug.Log(currentDifficulty);
        SceneManager.LoadScene(mainMenu);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
