using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TankShooter : Shooter
{
    public Transform firepointTransform;
    public AudioSource fireSound;

    // Start is called before the first frame update
    public override void Start()
    {




    }

    // Update is called once per frame
    public override void Update()
    {



    }

    public override void Shoot(GameObject shellPrefab, float fireForce, float damageDone, float lifespan)
    {

        // Instantiate our projectile
        GameObject newShell = Instantiate(shellPrefab, firepointTransform.position, firepointTransform.rotation) as GameObject;

        //get the DamageOnHit Component
        DamageOnHit damageOnHit = newShell.GetComponent<DamageOnHit>();

        // If it has one 
        if (damageOnHit != null)
        {
            damageOnHit.damageDone = damageDone;

            damageOnHit.owner = GetComponent<Pawn>();
        }

        PlayFireSound();

        //Get the rigibody

        Rigidbody rigidbody = newShell.GetComponent<Rigidbody>();

        if (rigidbody != null)
        {
            rigidbody.AddForce(firepointTransform.forward * fireForce);
        }



        Destroy(newShell, lifespan);
    }
    void PlayFireSound()
    {
        // Check if the audio source is valid
        if (fireSound != null)
        {
            // Play the fire sound without interrupting other sounds
            fireSound.PlayOneShot(fireSound.clip);
        }
    }


}

