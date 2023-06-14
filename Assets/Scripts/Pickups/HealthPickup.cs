using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    private bool isCollected;

    public int healAmount;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !isCollected)
        {
            isCollected = true;

            PlayerHealthController.instance.HealPlayer(healAmount);

            Destroy(gameObject);

            AudioManager.instance.PlaySFX(5);
        }
    }
}
