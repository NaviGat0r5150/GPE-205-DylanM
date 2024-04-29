using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIcon1 : AIController
{

    // Start is called before the first frame update
    public override void Start()
    {
        // Instantiate the pawn object
        pawn = Instantiate(pawn, transform.position, transform.rotation);

        // Assign the instantiated pawn object to the pawn reference of the AIController
        pawn = pawn.GetComponent<Pawn>();

        // Call the Start method of the parent class (AIController)
        base.Start();

        // Attempt to find the Rigidbody component in the pawn object or its children
        rb = pawn.GetComponentInChildren<Rigidbody>();

        if (rb == null)
        {
            // Print an error message if the Rigidbody is not found
            Debug.LogError("Rigidbody component not found in the pawn or its children.");
        }
    }
}
    