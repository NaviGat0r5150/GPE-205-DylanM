using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    [Header("Prefabs")]
    public GameObject playerControllerPrefab;
    public GameObject tankPawnPrefab;
    public Transform playerSpawnTransform;
    private GameObject player;

    //making a list of player controllers for possible multiplayer

    public List<PlayerController> players;

    [Space(5)]
    [Header("Game Over Stuff")]
    public Text gameOverText;
    public Image youLoseImage;
    public float highScore;

    public GameObject testState;



    private void Start()
    {
        
    }

    private void Awake()
    {
        
        SpawnPlayer();


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

    public void ActivateTestState()
    {
        testState.SetActive(true);
    }

    public void DeathScreen()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void ResetPlayerStatus()
    {
        // Reset player health
        player.GetComponent<Health>().currentHealth = player.GetComponent<Health>().maxHealth;

        // Reset player position to spawn position
        player.transform.position = playerSpawnTransform.position;

        // Other player status reset if needed
    }

    // Respawn player at the spawn position
    public void RespawnPlayer()
    {
        // Destroy previous player if exists
        if (player != null)
        {
            Destroy(player);
        }

        // Instantiate new player
        player = Instantiate(playerControllerPrefab, playerSpawnTransform.position, playerSpawnTransform.rotation);
    }

    // Restart gameplay
    public void RestartGameplay()
    {
        // Reset player status
        ResetPlayerStatus();

        // Respawn player
        RespawnPlayer();

        // Reload scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}