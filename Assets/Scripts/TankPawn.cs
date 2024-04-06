using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPawn : Pawn

{

    public float timerDelay = 1.0f;
    private float nextEventTime;
    // Start is called before the first frame update
    public override void Start()
    {
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
            Debug.Log("It’s me!");
            nextEventTime = Time.time + timerDelay;
            shooter.Shoot(shellPrefab, fireForce, damageDone, shellLifespan);
        }
    }







}
