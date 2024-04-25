using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class TankPawn : Pawn

{
    
    public float timerDelay;
    private float nextEventTime;
    // Start is called before the first frame update
    public override void Start()
    {
        float secondsPerShot;
        if(fireRate <= 0)
        {
            secondsPerShot = Mathf.Infinity;
        }
        else
        {
            secondsPerShot = 1 / fireRate;
        }
        timerDelay = secondsPerShot;
        nextEventTime = Time.time + timerDelay;
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Start();

    } 
    public override void MoveForward()
    {
        if (mover != null)
        {
            mover.Move(transform.forward, moveSpeed);
        }
        else
        {
            Debug.LogWarning("Warning: No Mover in TankPawn.MoveForward");
        }
    }

    public override void MoveBackward()
    {
        mover.Move(transform.forward, -moveSpeed);
    }

    public override void RotateClockwise()
    {
        mover.Rotate(turnSpeed);
    }

    public override void RotateCounterClockwise()
    {
        mover.Rotate(-turnSpeed);
    }

    public override void Shoot()
    {
        if (Time.time >= nextEventTime)
        { 
            shooter.Shoot(shellPrefab, fireForce, damageDone, shellLifespan);
            nextEventTime = Time.time + timerDelay;
        }
    }

    public override void RotateTowards(Vector3 targetPosition)
    {
        //find the vector to the target
        Vector3 vectorToTarget = targetPosition - transform.position;
        //find the rotation to look down that vector
        Quaternion targetRotation = Quaternion.LookRotation(vectorToTarget, Vector3.up);
        //then we rotate closer to that vector, but at the current turnspeed
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    public override void MakeNoise()
    {
        if (noiseMaker != null)
        {
            noiseMaker.volumeDistance = noiseMakerVolume;
        }
        else Debug.Log("NUH UH");
    }

    public override void StopNoise()
    {
        if (noiseMaker != null)
        {
            noiseMaker.volumeDistance = 0;
        }
    }

    public override void IncreaseFireRate(float amount)
    {
        if (shooter != null)
        {
            timerDelay /= amount;
            timerDelay = Mathf.Clamp(timerDelay, 0, Mathf.Infinity);
            Debug.Log(gameObject.name + "shoots " + amount + " faster");
            
        }
    }
    public override void DecreaseFireRate(float amount)
    {
        if (shooter != null)
        {
            timerDelay *= amount;

        }
    }
    public override void IncreaseMovementSpeed(float amount)
    {
        if (mover != null)
        {
            moveSpeed += amount;
            moveSpeed = Mathf.Clamp(moveSpeed, 0, Mathf.Infinity);
        }
    }
    public override void DecreaseMovementSpeed(float amount)
    {
        if (mover != null)
        {
            moveSpeed -= amount;

        }
    }


}








