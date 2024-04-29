using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPickup : Pickup
{
    public ShieldPowerup powerup;

    // Start is called before the first frame update
    public void OnTriggerEnter(Collider other)
    {
        //store other colliding object's powerupmanager
        PowerupManager powerupManager = other.GetComponent<PowerupManager>();

        //if the other object has a PM
        if (powerupManager != null)
        {
            //add the powerup
            powerupManager.Add(powerup);

            //destroy this pickup
            Destroy(gameObject);
        }
    }
}
