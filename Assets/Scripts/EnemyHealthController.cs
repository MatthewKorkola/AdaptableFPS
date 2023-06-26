using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{

    public int currentHealth = 5;

    public EnemyController theEC;

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

    public void DamageEnemy(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (theEC != null)
        {
            theEC.GetShot();
        }

        if (currentHealth <= 0)
        {
            adjustCriteria.skillStreak += 1;
            Debug.Log("Current skillStreak: " + adjustCriteria.skillStreak);

            adjustCriteria.struggleStreak = 0;
            Debug.Log("Enemy defeated, " + adjustCriteria.struggleStreak);

            if (adjustCriteria.skillStreak == adjustCriteria.skillStreakThreshold)
            {
                adjustCriteria.AdjustDifficultyProficient();
                adjustCriteria.skillStreak = 0;
                Debug.Log("Reached threshold. Current skillStreak: " + adjustCriteria.skillStreak);
            }

            Destroy(gameObject);

            AudioManager.instance.PlaySFX(2);
        }
    }
}
