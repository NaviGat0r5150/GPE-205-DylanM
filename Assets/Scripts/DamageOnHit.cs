using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnHit : MonoBehaviour
{
    public float damageDone;
    public Pawn owner;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        //check to see if object has a health component
        //get health comp form other object
        Health otherHealth = other.gameObject.GetComponent<Health>();
        if (otherHealth != null) 
        {
            //this means we do have access to a helth compoinen;inflict damage
            otherHealth.TakeDamage(damageDone, owner);
        }
        // destroy object
        Destroy(gameObject);
    }


}