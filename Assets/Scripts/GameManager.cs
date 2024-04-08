using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public GameObject playerControllerPrefab;
    public GameObject tankPawnPrefab;

    public Transform playerSpawnTransform;

    //making a list of player controllers for possible multiplayer

    public List<PlayerController> players;

 private void Start()
    {
        SpawnPlayer();
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //if gamemanager instance already exists, destroy the game object
            Destroy(gameObject); 
        }

    }

   
    public void SpawnPlayer()
        {
        GameObject newPlayerObj = Instantiate(playerControllerPrefab, Vector3.zero, Quaternion.identity) as GameObject;

        GameObject newPawnObj = Instantiate(tankPawnPrefab, playerSpawnTransform.position, playerSpawnTransform.rotation) as GameObject;

        Controller newController = newPlayerObj.GetComponent<Controller>();  

        Pawn newPawn = newPawnObj.GetComponent<Pawn>();

        newPawnObj.AddComponent<NoiseMaker>();
        newPawn.noiseMaker = newPawnObj.GetComponent<NoiseMaker>();
        newPawn.noiseMakerVolume = 3;

        newController.pawn = newPawn;
    }

}