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

    // Awake-called when the object is first created

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

    private void Start()
    {
        SpawnPlayer();
    }
    public void SpawnPlayer()
        {
        GameObject newPlayerObj = Instantiate(playerControllerPrefab, Vector3.zero, Quaternion.identity) as GameObject;

        GameObject newPawnObj = Instantiate(tankPawnPrefab, playerSpawnTransform.position, Quaternion.identity) as GameObject;

        Controller newPlayerController = newPlayerObj.GetComponent<Controller>();  

        Pawn newPlayerPawn = newPawnObj.GetComponent<Pawn>();

        newPlayerController.pawn = newPlayerPawn;
    }
}