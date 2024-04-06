using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMover : Mover
{
    // Variable to hold the Rigidbody Component
    private Rigidbody rb;
    private Transform tf;

    // Start is called before the first frame update
    public override void Start()
    {
        // Getting the Rigidbody component
        rb = GetComponent<Rigidbody>();
        tf = GetComponent<Transform>();
    }

    public override void Move(Vector3 direction, float speed)
    {
        Vector3 moveVector = direction.normalized * speed * Time.deltaTime;
        rb.MovePosition(rb.position + moveVector);
    }


    public override void Rotate(float turnSpeed)
    {
        tf.Rotate(0, turnSpeed * Time.deltaTime, 0);
    }

}