using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public string theGun;

    private bool collected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !collected)
        {
            collected = true;

            PlayerController.instance.AddGun(theGun);

            Destroy(gameObject);

            AudioManager.instance.PlaySFX(4);
        }
    }
}
