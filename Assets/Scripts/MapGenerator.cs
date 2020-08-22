using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;


public class MapGenerator : MonoBehaviour
{
    public static MapGenerator mapMaker { get; private set; }

    private IEnumerator spawner;

    [Header("Spawn Vals")]
    public bool isActive = false;
    public GameObject newPower;
    public int choice;
    public int gridSector = 0;
    public int entryNum = 0;
    public int pickupNum = 0;
    public List<GameObject> activePowerUps;
    public Room sector;

    public int mapSeed;
    public int rows;
    public int cols;
    public bool isGenerated;
    public enum MapType { Seeded, Random, MapOfTheDay }
    public MapType mapType = MapType.Random;
    public GameObject[] gridPrefabs;

    private Room[,] grid;

    
    [SerializeField] float roomWidth = 50.0f;
    [SerializeField] float roomLength = 50.0f;


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
        activePowerUps = new List<GameObject>();

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                // Room spawn Location
                float xPosition = roomWidth * col;
                float zPosition = roomLength * row;
                Vector3 newPos = new Vector3(xPosition, 0.0f, zPosition);

                GameObject tempRoomObj = Instantiate(RandRoomPrefab(), newPos, Quaternion.identity) as GameObject; // Spawn a room in our list at the new location with the same rotation
                
                // Set Room parent and title
                tempRoomObj.transform.parent = this.transform;
                tempRoomObj.name = "Room_" + col + "," + row;
                
                // Get Room component for new room section
                Room tempRoom = tempRoomObj.GetComponent<Room>();
                
                sector = tempRoom;
                spawner = SpawnPowerUp(0f); 
                StartCoroutine(spawner); // Instantiate the PowerUp
                activePowerUps.Add(newPower); // Add PowerUp to List
                StopCoroutine(spawner);
                entryNum++;
                pickupNum = 1;
                spawner = SpawnPowerUp(0f);
                StartCoroutine(spawner);
                activePowerUps.Add(newPower);
                StopCoroutine(spawner);
                entryNum++;
                pickupNum = 0;
                gridSector++;

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
        isActive = true;
    }

    public IEnumerator SpawnPowerUp(float waitTime)
    {
        int seed = System.DateTime.Now.Millisecond;
        UnityEngine.Random.InitState(seed);
        GameObject newDrop;
        Pickup properties;

        choice = UnityEngine.Random.Range(0, 3);
        Debug.Log("Choice" + choice);

        if (!isActive)
        {   
            switch (choice)
            {
                case 0:
                    if (pickupNum == 0)
                    {
                        newDrop = Instantiate(sector.rp, sector.spawnPoints[pickupNum].position, Quaternion.identity);
                        newPower = newDrop;
                        properties = newDrop.GetComponent<Pickup>();
                        properties.generator = this;
                        properties.spawnedAt = sector;
                        properties.spotNum = entryNum;
                        properties.pNum = pickupNum;
                    }
                    else if (pickupNum == 1)
                    {
                        newDrop = Instantiate(sector.rp, sector.spawnPoints[pickupNum].position, Quaternion.identity);
                        newPower = newDrop;
                        properties = newDrop.GetComponent<Pickup>();
                        properties.generator = this;
                        properties.spawnedAt = sector;
                        properties.spotNum = entryNum;
                        properties.pNum = pickupNum;
                    }
                    break;
                case 1:
                    if (pickupNum == 0)
                    {
                        newDrop = Instantiate(sector.sp, sector.spawnPoints[pickupNum].position, Quaternion.identity);
                        newPower = newDrop;
                        properties = newDrop.GetComponent<Pickup>();
                        properties.generator = this;
                        properties.spawnedAt = sector;
                        properties.spotNum = entryNum;
                        properties.pNum = pickupNum;
                    }
                    else if (pickupNum == 1)
                    {
                        newDrop = Instantiate(sector.sp, sector.spawnPoints[pickupNum].position, Quaternion.identity);
                        newPower = newDrop;
                        properties = newDrop.GetComponent<Pickup>();
                        properties.generator = this;
                        properties.spawnedAt = sector;
                        properties.spotNum = entryNum;
                        properties.pNum = pickupNum;
                    }
                    break;
                case 2:
                    if (pickupNum == 0)
                    {
                        newDrop = Instantiate(sector.shp, sector.spawnPoints[pickupNum].position, Quaternion.identity);
                        newPower = newDrop;
                        properties = newDrop.GetComponent<Pickup>();
                        properties.generator = this;
                        properties.spawnedAt = sector;
                        properties.spotNum = entryNum;
                        properties.pNum = pickupNum;
                    }
                    else if (pickupNum == 1)
                    {
                        newDrop = Instantiate(sector.shp, sector.spawnPoints[pickupNum].position, Quaternion.identity);
                        newPower = newDrop;
                        properties = newDrop.GetComponent<Pickup>();
                        properties.generator = this;
                        properties.spawnedAt = sector;
                        properties.spotNum = entryNum;
                        properties.pNum = pickupNum;
                    }
                    break;
                default:
                    Debug.Log("Choice out of range.");
                    break;
            }
        }
        else Debug.Log("PowerUp already exists.");
        yield break;
    }

    public IEnumerator Timer(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }


    public void ReplacePowerUp()
    {
        GameObject newDrop;
        Pickup properties;

        Debug.Log("Time to replace lost PowerUp");

        if (!isActive)
        {
            switch (choice)
            {
                case 0:
                    if (pickupNum == 0)
                    {
                        newDrop = Instantiate(sector.rp, sector.spawnPoints[pickupNum].position, Quaternion.identity);
                        properties = newDrop.GetComponent<Pickup>();
                        properties.generator = this;
                        newPower = newDrop;
                        properties.spawnedAt = sector;
                        if (entryNum == 8) properties.spotNum = entryNum - 1;
                        else properties.spotNum = entryNum;
                        properties.pNum = pickupNum;
                        isActive = true;
                    }
                    else if (pickupNum == 1)
                    {
                        newDrop = Instantiate(sector.rp, sector.spawnPoints[pickupNum].position, Quaternion.identity);
                        properties = newDrop.GetComponent<Pickup>();
                        properties.generator = this;
                        newPower = newDrop;
                        properties.spawnedAt = sector;
                        if (entryNum == 8) properties.spotNum = entryNum - 1;
                        else properties.spotNum = entryNum;
                        properties.pNum = pickupNum;
                        isActive = true;
                    }
                    break;
                case 1:
                    if (pickupNum == 0)
                    {
                        newDrop = Instantiate(sector.sp, sector.spawnPoints[pickupNum].position, Quaternion.identity);
                        properties = newDrop.GetComponent<Pickup>();
                        properties.generator = this;
                        newPower = newDrop;
                        properties.spawnedAt = sector;
                        if (entryNum == 8) properties.spotNum = entryNum - 1;
                        else properties.spotNum = entryNum;
                        properties.pNum = pickupNum;
                        isActive = true;
                    }
                    else if (pickupNum == 1)
                    {
                        newDrop = Instantiate(sector.sp, sector.spawnPoints[pickupNum].position, Quaternion.identity);
                        properties = newDrop.GetComponent<Pickup>();
                        properties.generator = this;
                        newPower = newDrop;
                        properties.spawnedAt = sector;
                        if (entryNum == 8) properties.spotNum = entryNum - 1;
                        else properties.spotNum = entryNum;
                        properties.pNum = pickupNum;
                        isActive = true;
                    }
                    break;
                case 2:
                    if (pickupNum == 0)
                    {
                        newDrop = Instantiate(sector.shp, sector.spawnPoints[pickupNum].position, Quaternion.identity);
                        properties = newDrop.GetComponent<Pickup>();
                        properties.generator = this;
                        newPower = newDrop;
                        properties.spawnedAt = sector;
                        if (entryNum == 8) properties.spotNum = entryNum - 1;
                        else properties.spotNum = entryNum;
                        properties.pNum = pickupNum;
                        isActive = true;
                    }
                    else if (pickupNum == 1)
                    {
                        newDrop = Instantiate(sector.shp, sector.spawnPoints[pickupNum].position, Quaternion.identity);
                        properties = newDrop.GetComponent<Pickup>();
                        properties.generator = this;
                        newPower = newDrop;
                        properties.spawnedAt = sector;
                        if (entryNum == 8) properties.spotNum = entryNum - 1;
                        else properties.spotNum = entryNum;
                        properties.pNum = pickupNum;
                        isActive = true;
                    }
                    break;
                default:
                    Debug.Log("Choice out of range.");
                    break;
            }
        }
        else Debug.Log("PowerUp already exists.");
    }
}