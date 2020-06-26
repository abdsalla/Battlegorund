using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

public class MapGenerator : MonoBehaviour
{
    public static MapGenerator mapMaker { get; private set; }

    public int mapSeed;
    public int rows;
    public int cols;
    public bool isGenerated;
    public enum MapType { Seeded, Random, MapOfTheDay}
    public MapType mapType = MapType.Random;
    public GameObject[] gridPrefabs;
    private Room[,] grid;
    [SerializeField] float roomWidth = 50.0f;
    [SerializeField] float rooomLength = 50.0f;


    void Awake()
    {
        if (mapMaker != null && mapMaker != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            mapMaker = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    void Start()
    {
        switch (mapType)
        {
            case MapType.MapOfTheDay:
                mapSeed = DateToInt(DateTime.Now.Date);
                break;
            case MapType.Random:
                mapSeed = DateToInt(DateTime.Now);
                break;
            case MapType.Seeded:
                break;
            default:
                Debug.LogError("Map Type Does Not Exist.");
                break;
        }

        GenerateGrid();
    }

    public GameObject RandRoomPrefab() // Returns random room in our list
    {
        return gridPrefabs[UnityEngine.Random.Range(0, gridPrefabs.Length)];
    }

    public int DateToInt(DateTime dateToUse)
    {
        return dateToUse.Year + dateToUse.Month + dateToUse.Day + dateToUse.Hour + dateToUse.Minute + dateToUse.Second + dateToUse.Millisecond;
    }

    public void GenerateGrid()
    {
        UnityEngine.Random.seed = mapSeed;
        grid = new Room[cols, rows]; // clear room before creating another

        for(int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                // Room spawn Location
                float xPosition = roomWidth * col;
                float zPosition = rooomLength * row;
                Vector3 newPos = new Vector3(xPosition, 0.0f, zPosition);

                GameObject tempRoomObj = Instantiate(RandRoomPrefab(), newPos, Quaternion.identity) as GameObject; // Spawn a room in our list at the new location with the same rotation

                // Set Room parent and title
                tempRoomObj.transform.parent = this.transform;
                tempRoomObj.name = "Room_" + col + "," + row;

                // Get Room component for new room section
                Room tempRoom = tempRoomObj.GetComponent<Room>();

                if (row == 0) tempRoom.doorNorth.SetActive(false);
                else if (row == rows - 1) tempRoom.doorSouth.SetActive(false);
                else
                {
                    tempRoom.doorNorth.SetActive(false);
                    tempRoom.doorSouth.SetActive(false);
                }

                if (col == 0) tempRoom.doorEast.SetActive(false);
                else if (col == cols - 1) tempRoom.doorWest.SetActive(false);
                else
                {
                    tempRoom.doorEast.SetActive(false);
                    tempRoom.doorWest.SetActive(false);
                }

                grid[col, row] = tempRoom; // Save Grid
            }
        }    
    }
}