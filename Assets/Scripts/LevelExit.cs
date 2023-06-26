using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    public string nextLevel;

    public float waitToEndLevel;

    public AdjustCriteria adjustCriteria;

    private void Awake()
    {
        adjustCriteria = FindObjectOfType<AdjustCriteria>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.instance.levelEnding = true;

            StartCoroutine(EndLevelCo());

            AudioManager.instance.PlayLevelVictory();
        }
    }

    private IEnumerator EndLevelCo()
    {
        PlayerPrefs.SetString(nextLevel + "_cp", "");

        yield return new WaitForSeconds(waitToEndLevel);

        // difficulty adjustment
        adjustCriteria.AdjustDifficultyVictory();

        SceneManager.LoadScene(nextLevel);
    }
}
