using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    // This is the Variable for move speed
    public float moveSpeed;
    // This is the Variable for turn speed
    public float turnSpeed;

    // variable for rate of fire
    public float fireRate;

    // Variable for holding our mover component
    public Mover mover;

    public Shooter shooter;


    //Start is called before the first frame update
    public virtual void Start()
    {   
        mover = GetComponent<Mover>();

        shooter = GetComponent<Shooter>();
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

    // Variable for our shell prefab
    public GameObject shellPrefab;
    // Variable for our firing force
    public float fireForce;
    // Variable for our damage done
    public float damageDone;
    // Variable for how long our bullets survive if they don't collide
    public float shellLifespan;

}
