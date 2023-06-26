using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    public int maxHealth, currentHealth;

    public float invincibleLength = 1f;
    private float invincCounter;

    public AdjustCriteria adjustCriteria;

    private void Awake()
    {
        instance = this;
        adjustCriteria = FindObjectOfType<AdjustCriteria>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        UIController.instance.healthSlider.maxValue = maxHealth;
        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = "Health: " + currentHealth + "/" + maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (invincCounter > 0)
        {
            invincCounter -= Time.deltaTime;
        }
    }

    public void DamagePlayer(int damageAmount)
    {

        if (invincCounter <= 0 && !GameManager.instance.levelEnding)
        {
            AudioManager.instance.PlaySFX(7);

            currentHealth -= damageAmount;

            adjustCriteria.skillStreak = 0;
            Debug.Log("Player hit. Current skillStreak: " + adjustCriteria.skillStreak);

            adjustCriteria.struggleStreak += damageAmount;
            Debug.Log("Total damage received since last enemy kill: " + adjustCriteria.struggleStreak);

            if (adjustCriteria.struggleStreak >= adjustCriteria.struggleStreakThreshold)
            {
                adjustCriteria.AdjustDifficultyStruggling();
                adjustCriteria.struggleStreak = 0;
                Debug.Log("Damage threshold reached. New value: " + adjustCriteria.struggleStreak);
            }

            UIController.instance.ShowDamage();

            if (currentHealth <= 0)
            {
                gameObject.SetActive(false);

                currentHealth = 0;

                GameManager.instance.PlayerDied();

                AudioManager.instance.StopBGM();
                AudioManager.instance.PlaySFX(6);
                AudioManager.instance.PlaySFX(7);
            }

            invincCounter = invincibleLength;

            UIController.instance.healthSlider.value = currentHealth;
            UIController.instance.healthText.text = "Health: " + currentHealth + "/" + maxHealth;
        }
    }

    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = "Health: " + currentHealth + "/" + maxHealth;
    }
}
