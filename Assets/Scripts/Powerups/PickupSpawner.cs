using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    public GameObject pickupPrefab;
    public float spawnDelay;
    private float nextSpawnTime;
    private GameObject spawnedPickup;

    void Start()
    {
        nextSpawnTime = Time.time + spawnDelay;
    }

    void Update()
    {
        //if there isn't already a pickup in this spot
        if(spawnedPickup == null)
        {
            //if its time to spawn a pickup
            if(Time.time > nextSpawnTime)
            {
                //spawn it and set the next spawn time
                spawnedPickup = Instantiate(pickupPrefab, transform.position, Quaternion.identity);
                nextSpawnTime = Time.time + spawnDelay;
            }
        }
    }
    








}
