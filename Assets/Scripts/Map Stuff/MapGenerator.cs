using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{

    #region variables
    public GameObject[] gridPrefabs;
    public int rows;
    public int cols;
    public float roomWidth = 50.0f;
    public float roomHeight = 50.0f;
    private Room[,] mapGrid;
    private Room[,] lastRoom;

    public GameObject PlayerSpawnPoint;


    #endregion variables

    // Start is called before the first frame update
    void Start()
    {
        //call generate map function for testing
        GenerateMap();

        MoveObjectToSpawnPosition();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GenerateMap()
    {
        //initialize and clear out the grid
        mapGrid = new Room[cols, rows];

        #region Spawn Room Tiles
        //Randomly select room tiles, one grid coord at a time

        //for each grid row...
        for (int currentRow = 0; currentRow < rows; currentRow++)
        {
            //for each column in that row
            for (int currentCol = 0; currentCol < cols; currentCol++)
            {

                //figure out the location
                float xPosition = roomWidth * currentCol;
                float zPosition = roomHeight * currentRow;
                Vector3 newPosition = new Vector3(xPosition, 0, zPosition);

                if ((currentCol > 0 || currentRow > 0) && (currentCol < 4 || currentRow < 4))
                {





                    //create a random room tile at that location
                    GameObject tempRoomObj = Instantiate(RandomRoomPrefab(), newPosition, Quaternion.identity);

                    // set its parent
                    tempRoomObj.transform.parent = transform;

                    // give it a meaningful name
                    tempRoomObj.name = "Room_" + currentCol + "," + currentRow;

                    //get the room.cs component from it
                    Room tempRoom = tempRoomObj.GetComponent<Room>();

                    // save it to the mapgrid array
                    mapGrid[currentCol, currentRow] = tempRoom;

                    #endregion Spawn room tiles


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
                //GENERATE START ROOM AT 0,0
                else if (currentCol == 0 && currentRow == 0)
                {
                    GameObject tempRoomObj = Instantiate(StartRoomPrefab(), newPosition, Quaternion.identity);

                    // set its parent
                    tempRoomObj.transform.parent = transform;

                    // give it a meaningful name
                    tempRoomObj.name = "Room_" + currentCol + "," + currentRow;

                    //get the room.cs component from it
                    Room tempRoom = tempRoomObj.GetComponent<Room>();

                    // save it to the mapgrid array
                    mapGrid[currentCol, currentRow] = tempRoom;





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
                //generate BOSS ROOM AT 4,4
                else if (currentCol == 4 && currentRow == 4)
                {
                    GameObject tempRoomObj = Instantiate(EndRoomPrefab(), newPosition, Quaternion.identity);

                    // set its parent
                    tempRoomObj.transform.parent = transform;

                    // give it a meaningful name
                    tempRoomObj.name = "Room_" + currentCol + "," + currentRow;

                    //get the room.cs component from it
                    Room tempRoom = tempRoomObj.GetComponent<Room>();

                    // save it to the mapgrid array
                    mapGrid[currentCol, currentRow] = tempRoom;
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

            }
        }

    }

Vector3 CalculatePlayerSpawnPosition()
{
    // Assuming you have already calculated the player spawn position
     Vector3 prefabPosition = StartRoomPrefab().transform.position;
    Vector3 spawnOffset = new Vector3(0f, 20f, 0f);
    Vector3 playerSpawnPosition = prefabPosition + spawnOffset;
    return playerSpawnPosition;
}

// Move the object to the player spawn position
void MoveObjectToSpawnPosition()
{
    // Calculate the player spawn position
    Vector3 playerSpawnPosition = CalculatePlayerSpawnPosition();

    // Check if the object to move is not null
    if (PlayerSpawnPoint != null)
    {
        // Set the object's position to the player spawn position
        PlayerSpawnPoint.transform.position = playerSpawnPosition;
    }
    else
    {
        Debug.LogError("Object to move is not assigned!");
    }
}

public GameObject StartRoomPrefab()
    {
        return gridPrefabs[0];
    }


    public GameObject RandomRoomPrefab()
    {
        return gridPrefabs[Random.Range(2, gridPrefabs.Length)];
    }

    public GameObject EndRoomPrefab()
    {
        return gridPrefabs[1];
    }
}
