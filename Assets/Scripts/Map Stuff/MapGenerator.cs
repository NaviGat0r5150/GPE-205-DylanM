using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject[] gridPrefabs;
    public int rows;
    public int cols;
    public float roomWidth = 50.0f;
    public float roomHeight = 50.0f;
    private Room[,] mapGrid;
    public GameObject PlayerSpawnPoint;
    // Seed for random number generation
    public bool randomSeed = false;
    public bool dateSeed = false;

    public void Start()
    {
        int finalSeed;

        // Check if manual seed is provided in GameSettings
        if (GameSettings.manualSeed != 0)
        {
            finalSeed = GameSettings.manualSeed;
        }
        else if (randomSeed)
        {
            finalSeed = UnityEngine.Random.Range(0, int.MaxValue);
        }
        else if (GameSettings.dateSeed)
        {
            finalSeed = generateSeedFromCurrentDate();
        }
        else
        {
            // If no seed options are enabled, default to a random seed
            finalSeed = UnityEngine.Random.Range(0, int.MaxValue);
        }

        UnityEngine.Random.InitState(finalSeed);

        GenerateMap();
        MoveObjectToSpawnPosition();

        Debug.Log("Generated seed: " + finalSeed);
    }

    //generate seed based on current date:
    public static int generateSeedFromCurrentDate()
    {
        DateTime currentDate = DateTime.Now;
        int dateSeedNumber = currentDate.Year * 10000 + currentDate.Month * 100 + currentDate.Day;
        return dateSeedNumber;
    }


    public void GenerateMap()
    {
        mapGrid = new Room[cols, rows];

        for (int currentRow = 0; currentRow < rows; currentRow++)
        {
            for (int currentCol = 0; currentCol < cols; currentCol++)
            {
                // Generate rooms based on the random seed
                GenerateRoom(currentRow, currentCol);
            }
        }
    }

    public void GenerateRoom(int currentRow, int currentCol)
    {
        // Determine which room prefab to use based on the seed
        GameObject roomPrefab;
        if (currentRow == 0 && currentCol == 0)
        {
            roomPrefab = StartRoomPrefab();
        }
        else if (currentRow == rows - 1 && currentCol == cols - 1)
        {
            roomPrefab = EndRoomPrefab();
        }
        else
        {
            roomPrefab = RandomRoomPrefab();
        }

        // Calculate position for the room
        float xPosition = roomWidth * currentCol;
        float zPosition = roomHeight * currentRow;
        Vector3 newPosition = new Vector3(xPosition, 0, zPosition);

        // Instantiate the room
        GameObject tempRoomObj = Instantiate(roomPrefab, newPosition, Quaternion.identity);
        tempRoomObj.transform.parent = transform;
        tempRoomObj.name = "Room_" + currentCol + "," + currentRow;

        // Save room reference to map grid
        Room tempRoom = tempRoomObj.GetComponent<Room>();
        mapGrid[currentCol, currentRow] = tempRoom;

        // Open/close doors
        
        #region open/close doors
//open interior doors and close external doors
        if (currentRow == 0)
        {
            tempRoom.doorNorth.SetActive(false);
            //also could use Destroy(tempRoom.doorNorth);
        }
        else if (currentRow == rows - 1)
        {
            tempRoom.doorSouth.SetActive(false);
        }
        else
        {
            tempRoom.doorNorth.SetActive(false);
            tempRoom.doorSouth.SetActive(false);
        }

        if (currentCol == 0)
        {
            tempRoom.doorEast.SetActive(false);
            //also could use Destroy(tempRoom.doorNorth);
        }
        else if (currentCol == cols - 1)
        {
            tempRoom.doorWest.SetActive(false);
        }
        else
        {
            tempRoom.doorEast.SetActive(false);
            tempRoom.doorWest.SetActive(false);
        }
        #endregion open/close doors
    }

    Vector3 CalculatePlayerSpawnPosition()
    {
        // Calculate the player spawn position based on the generated map
        Vector3 prefabPosition = StartRoomPrefab().transform.position;
        Vector3 spawnOffset = new Vector3(0f, 20f, 0f);
        Vector3 playerSpawnPosition = prefabPosition + spawnOffset;
        return playerSpawnPosition;
    }

    void MoveObjectToSpawnPosition()
    {
        // Move the player spawn point to the calculated spawn position
        Vector3 playerSpawnPosition = CalculatePlayerSpawnPosition();

        if (PlayerSpawnPoint != null)
        {
            PlayerSpawnPoint.transform.position = playerSpawnPosition;
        }
        else
        {
            Debug.LogError("PlayerSpawnPoint is not assigned!");
        }
    }

    public GameObject StartRoomPrefab()
    {
        return gridPrefabs[0];
    }

    public GameObject RandomRoomPrefab()
    {
        return gridPrefabs[UnityEngine.Random.Range(2, gridPrefabs.Length)];
    }

    public GameObject EndRoomPrefab()
    {
        return gridPrefabs[1];
    }
}