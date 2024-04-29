using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    public Rigidbody rb; // Reference to the Rigidbody component

    // This is the Variable for move speed
    public float moveSpeed;
    // This is the Variable for turn speed
    public float turnSpeed;

    // variable for rate of fire
    public float fireRate;

    // Variable for holding our mover component
    public Mover mover;

    public Shooter shooter;

    public float noiseMakerVolume;

    public NoiseMaker noiseMaker;

    protected bool isMoving = false;

    //Start is called before the first frame update
    public virtual void Start()
    {   
        mover = GetComponent<Mover>();

        shooter = GetComponent<Shooter>();

        noiseMaker = GetComponent<NoiseMaker>();
    }

    //Update is called once per frame
    public virtual void Update()
    {       
    }


    public abstract void MoveForward();
    public abstract void MoveBackward();
    public abstract void RotateClockwise();
    public abstract void RotateCounterClockwise();

    public abstract void Shoot();

    public abstract void RotateTowards(Vector3 targetPosition);

    public abstract void MakeNoise();

    public abstract void StopNoise();

    // Variable for our shell prefab
    public GameObject shellPrefab;
    // Variable for our firing force
    public float fireForce;
    // Variable for our damage done
    public float damageDone;
    // Variable for how long our bullets survive if they don't collide
    public float shellLifespan;

    public abstract void IncreaseFireRate(float amount);
    public abstract void DecreaseFireRate(float amount);

    public abstract void IncreaseMovementSpeed(float amount);   
    public abstract void DecreaseMovementSpeed(float amount);

    public abstract void IncreaseDamageDone(float amount);
    public abstract void DecreaseDamageDone(float amount);

 

    // Method to stop movement
    public virtual void StopMoving()
    {
        isMoving = false;
    }

    // Method to start movement
    public virtual void StartMoving()
    {
        isMoving = true;
    }

    public abstract void Jump(float jumpForce, Vector3 direction);
}


