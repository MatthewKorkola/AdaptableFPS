using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustCriteria : MonoBehaviour
{

    public int skillStreak = 0;
    public int skillStreakThreshold = 8;

    public int struggleStreak = 0;
    public int struggleStreakThreshold = 60;


    // Modify the difficulty value based on game performance.

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AdjustDifficultyDeath()
    {
        int currentDifficulty = PlayerPrefs.GetInt("difficultyModifier");

        currentDifficulty -= 2;

        Debug.Log(currentDifficulty);

        PlayerPrefs.SetInt("difficultyModifier", currentDifficulty);

        PlayerPrefs.Save();
    }

    public void AdjustDifficultyVictory()
    {
        int currentDifficulty = PlayerPrefs.GetInt("difficultyModifier");

        currentDifficulty += 2;

        Debug.Log(currentDifficulty);

        PlayerPrefs.SetInt("difficultyModifier", currentDifficulty);

        PlayerPrefs.Save();
    }

    public void AdjustDifficultyProficient()
    {
        // needs variables skillStreak and skillStreakThreshold
        // If the player defeats skillStreakThreshold enemies without taking damage, difficulty is increased.
        // The skillStreak counter is reset when the player defeats skillStreakThreshold enemies or takes damage.

        int currentDifficulty = PlayerPrefs.GetInt("difficultyModifier");

        currentDifficulty += 1;

        Debug.Log(currentDifficulty);

        PlayerPrefs.SetInt("difficultyModifier", currentDifficulty);

        PlayerPrefs.Save();
    }

    public void AdjustDifficultyStruggling()
    {
        // needs variables struggleStreak and struggleStreakThreshold
        // If the player takes struggleStreakThreshold damage without defeating any enemies, difficulty is decreased.
        // The struggleStreak counter is reset when the player defeats an enemy or takes struggleStreakThreshold amount of damage.

        int currentDifficulty = PlayerPrefs.GetInt("difficultyModifier");

        currentDifficulty -= 1;

        Debug.Log(currentDifficulty);

        PlayerPrefs.SetInt("difficultyModifier", currentDifficulty);

        PlayerPrefs.Save();
    }
}
