using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    [Header("Prefabs")]
    public GameObject playerControllerPrefab;
    public GameObject tankPawnPrefab;
    public Transform playerSpawnTransform;

    //making a list of player controllers for possible multiplayer

    public List<PlayerController> players;

    [Space(5)]
    [Header("Game Over Stuff")]
    public Text gameOverText;
    public Image youLoseImage;
    public float highScore;



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