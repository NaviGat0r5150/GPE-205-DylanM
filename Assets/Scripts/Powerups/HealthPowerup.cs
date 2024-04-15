using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HealthPowerup : Powerup
{

    public float healthToAdd;
    public override void Apply(PowerupManager target)
    {
       //get the health comp of the other object
       Health targetHealth = target.GetComponent<Health>();

        //if there is a health comp.
        if(targetHealth != null )
        {
            targetHealth.RepenishHealth(healthToAdd);
        }
    }

    public override void Remove(PowerupManager target)
    {
        // TO DO Remove health changes
    }
}
