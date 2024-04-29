using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShieldPowerup : Powerup
{
    public override void Apply(PowerupManager target)
    {
        //get the health comp of the other object
        Health targetHealth = target.GetComponent<Health>();

        //if there is a health comp.
        if (targetHealth != null)
        {
            targetHealth.hasShield = true;
        }
    }

    public override void Remove(PowerupManager target)
    {
        //get the health comp of the other object
        Health targetHealth = target.GetComponent<Health>();

        //if there is a health comp.
        if (targetHealth != null)
        {
            targetHealth.hasShield = false;
        }
    }
}
