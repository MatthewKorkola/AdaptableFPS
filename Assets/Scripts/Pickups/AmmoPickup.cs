using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    private bool collected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !collected)
        {
            collected = true;

            PlayerController.instance.activeGun.GetAmmo();

            Destroy(gameObject);

            AudioManager.instance.PlaySFX(3);
        }
    }
}
