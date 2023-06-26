using System;
using UnityEngine;

public class AdjustConcurrent : MonoBehaviour
{
    public PlayerHealthController playerHealthController;
    public HealthPickup[] healthPickups;
    public AmmoPickup[] ammoPickups;
    public Lava[] lavaHazards;
    public AlienController[] aliens;


    private void Awake()
    {
        // Read in the current difficulty value.
        int difficultyModifier = PlayerPrefs.GetInt("difficultyModifier");

        // Adjust game settings based on the current difficulty value.
        // The following settings are adjustable:
        // - maximum player health
        // - amount of health recovered by collecting a health-recovering item
        // - maximum player ammo
        // - amount of ammo recovered by collecting an ammo-recovering item
        // - enemy damage
        // - hazard damage

        // Adjust game settings based on the current difficulty value.
        // Adjusting maximum player health
        playerHealthController = FindObjectOfType<PlayerHealthController>();
        playerHealthController.maxHealth = playerHealthController.maxHealth - ((difficultyModifier * 10) - (int)Math.Pow(difficultyModifier, 2));

        if (playerHealthController.maxHealth < 40) playerHealthController.maxHealth = 40;
        if (playerHealthController.maxHealth > 999) playerHealthController.maxHealth = 999;

        // health pickup adjustment
        healthPickups = FindObjectsOfType<HealthPickup>();
        if (healthPickups != null)
        {
            foreach (HealthPickup healthPickup in healthPickups)
            {
                healthPickup.healAmount = healthPickup.healAmount - difficultyModifier * 10;

                if (healthPickup.healAmount < 10) healthPickup.healAmount = 10;
                if (healthPickup.healAmount > 999) healthPickup.healAmount = 999;
            }
        }

        // ammo adjustment
        PlayerController playerController = FindObjectOfType<PlayerController>();
        if (playerController != null)
        {
            // adjust ammo for all guns the player currently has
            if (playerController.allGuns != null)
            {
                foreach (Gun gun in playerController.allGuns)
                {
                    AdjustGunAmmo(gun, difficultyModifier);
                }
            }

            // also adjust ammo for unlockable guns
            if (playerController.unlockableGuns != null)
            {
                foreach (Gun gun in playerController.unlockableGuns)
                {
                    AdjustGunAmmo(gun, difficultyModifier);
                }
            }
        }
        UIController.instance.UpdateAmmoText();

        // hazard adjustment
        lavaHazards = FindObjectsOfType<Lava>();
        if (lavaHazards != null)
        {
            foreach (Lava lava in lavaHazards)
            {
                lava.damage += (int)(difficultyModifier * 2.5);

                if (lava.damage < 1) lava.damage = 1; // Set a lower limit for the damage
                if (lava.damage > 999) lava.damage = 999; // Set an upper limit for the damage
            }
        }

        // enemy damage adjustment - alien
        aliens = FindObjectsOfType<AlienController>();
        if (aliens != null)
        {
            foreach (AlienController alien in aliens)
            {
                if (difficultyModifier > 0)
                {
                    alien.damageAmount += difficultyModifier * 3;
                }

                if (alien.damageAmount < 1) alien.damageAmount = 1;
                if (alien.damageAmount > 999) alien.damageAmount = 999;
            }
        }

        // enemy damage adjustment - standard enemy
        // Completed in the BulletController script



    }

    private void AdjustGunAmmo(Gun gun, int difficultyModifier)
    {
        // Modify the ammo of a gun based on the difficulty.
        if (difficultyModifier < 3)
        {
            gun.currentAmmo = gun.currentAmmo - (int)((gun.currentAmmo * 0.1) * difficultyModifier);
        }
        else
        {
            gun.currentAmmo = gun.currentAmmo - (int)((gun.currentAmmo * 0.1) * 2);
        }
        if (gun.currentAmmo < 3) gun.currentAmmo = 3;
        if (gun.currentAmmo > 999) gun.currentAmmo = 999;

        // Modify the pickupAmount of a gun based on the difficulty.
        gun.pickupAmount = gun.pickupAmount - (int)(difficultyModifier * 0.5 * gun.pickupAmount);
        if (gun.pickupAmount < 1) gun.pickupAmount = 1;
        if (gun.pickupAmount > 999) gun.pickupAmount = 999;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
