using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShooterPowerup : Powerup
{

    public float fireRateAddition;
    
    public override void Apply(PowerupManager target)
    {
       //get the pawn comp of the other object
       TankPawn targetTankpawn = target.GetComponent<TankPawn>();

        //if there is a pawn comp.
        if (targetTankpawn != null)
        {
            targetTankpawn.IncreaseFireRate(fireRateAddition);
        }
        
    }

    public override void Remove(PowerupManager target)
    {
        // TO DO Remove shooter changes
        //get the health comp of the other object
        TankPawn targetTankpawn = target.GetComponent<TankPawn>();

        //if there is a pawn comp.
        if (targetTankpawn != null)
        {
            targetTankpawn.DecreaseFireRate(fireRateAddition);
        }
    }
}
