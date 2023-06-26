using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public AdjustCriteria adjustCriteria;

    public float waitAfterDying = 2f;

    [HideInInspector]
    public bool levelEnding;

    private void Awake()
    {
        instance = this;
        adjustCriteria = FindObjectOfType<AdjustCriteria>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Click the screen to make the cursor disappear.
        // Press the escape key to make the cursor reappear.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }

    public void PlayerDied()
    {
        StartCoroutine(PlayerDiedCo());
    }

    public IEnumerator PlayerDiedCo()
    {
        yield return new WaitForSeconds(waitAfterDying);

        // difficulty adjustment
        adjustCriteria.AdjustDifficultyDeath();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PauseUnpause()
    {
        if (UIController.instance.pauseScreen.activeInHierarchy)
        {
            UIController.instance.pauseScreen.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            Time.timeScale = 1f;
        }
        else
        {
            UIController.instance.pauseScreen.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            Time.timeScale = 0f;
        }
    }
}
