using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickUp : MonoBehaviour
{

    private bool collected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !collected)
        {
            // give ammo
            Destroy(gameObject);
            collected = true;
            PlayerController.instance.activeGun.GetAmmo();
            AudioManager.instance.PlaySXF(7); // index of the sfx
        }
    }
}
