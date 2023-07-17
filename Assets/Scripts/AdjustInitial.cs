using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Text;
using System;

public class AdjustInitial : MonoBehaviour
{
    public string mainMenu;

    public class PlayerData
    {
        public long playerId;
        public int player_games;
        public double a_count;
        public double easy;
        public double normal;
        public double hard;
        public double total_achieved;
        public double percent_difficulty;
        public double avg_score;
        public double total_time_played;
        public double avg_time_played;
        public double game_score;
        public double percentage_completed;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetDifficulty());
    }

    IEnumerator GetDifficulty()
    {
        // Read in the player ID from Steam.

        // Prepare the player data to be sent to the server
        PlayerData playerData = new PlayerData
        {
            playerId = 76561197969158018,
            player_games = 23,
            a_count = 1814.0,
            easy = 164.0,
            normal = 64.0,
            hard = 9.0,
            total_achieved = 237.0,
            percent_difficulty = 1043.412562,
            avg_score = 4.402585,
            total_time_played = 69435.0,
            avg_time_played = 3018.913043,
            game_score = 101.259447,
            percentage_completed = 13.065050
        };

        string json = JsonUtility.ToJson(playerData);

        Debug.Log("JSON to send: " + json);

        using (UnityWebRequest www = new UnityWebRequest("http://localhost:5000/predict_difficulty", "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = www.downloadHandler.text;
                DifficultyResponse difficultyResponse = JsonUtility.FromJson<DifficultyResponse>(jsonResponse);

                Debug.Log("Model sets difficulty as " + difficultyResponse.difficulty);

                if (difficultyResponse.difficulty == 1)
                {
                    Debug.Log("Therefore, set difficulty to 0 (normal).");
                    PlayerPrefs.SetInt("difficultyModifier", 0);
                }
                else if (difficultyResponse.difficulty == 2)
                {
                    Debug.Log("Therefore, set difficulty to 6 (hard).");
                    PlayerPrefs.SetInt("difficultyModifier", 6);
                }
                else
                {
                    Debug.Log("Therefore, set difficulty to -7 (easy).");
                    PlayerPrefs.SetInt("difficultyModifier", -7);
                }

                PlayerPrefs.Save();

                // Transition to the main menu.
                Debug.Log("Setting difficulty.");
                int currentDifficulty = PlayerPrefs.GetInt("difficultyModifier");
                Debug.Log(currentDifficulty);
                SceneManager.LoadScene(mainMenu);
            }
            else
            {
                Debug.Log("Error: " + www.error);
                PlayerPrefs.SetInt("difficultyModifier", 0);
                SceneManager.LoadScene(mainMenu);
            }
        }
    }

    private class DifficultyResponse
    {
        public int difficulty;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
