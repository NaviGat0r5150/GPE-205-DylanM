using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
     void Update()
    {
        transform.Rotate(0, 0.5f, 0);
    }
}
