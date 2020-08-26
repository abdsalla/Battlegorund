using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.IO;
using System;

public class SaveData
{
    public int score1;
    public int score2;
    public int highScore;
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get { return instance; } }
    private static GameManager instance;


    [Header("Player")]
    public bool isTwoPlayer;
    public GameObject playerOne; // prefab
    public GameObject playerTwo;
    public GameObject currentPlayerOne; 
    public GameObject currentPlayerTwo;
    public int lives1 = 0;
    public int lives2 = 0;
    public int numOfPlayers = 0;
    public SaveData saveData;

    [Header("Scoring")]
    public int score1 = 0;
    public int score2 = 0;
    public int pointsAwarded = 10;
    public int pointsDeducted = 5;

    [Header("AI")]
    public GameObject hunter;
    public GameObject glider;
    public GameObject sniper;
    public GameObject rammer;
    public List<GameObject> activeEnemies;
    public int allowedEnemies;

    [Header("Spawn Locations")]
    public List<GameObject> playerSpawnPoints;
    public List<Transform> enemySpawnPoints;
    [SerializeField] PlayerSpawn playerSpawn;

    [Header("Powerup Spots")]
    public List<GameObject> powerupSpots;

    [Header("Scene Progression")]
    public SceneLoader sceneLoader;

    [Header("Audio")]
    public SoundManager mixer;

    [Header("UI")]
    public UIManager healthTracker;


    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    void Update()
    {
        if (numOfPlayers > 0)
        {
            if (!isTwoPlayer)
            {
                if (currentPlayerOne == null && lives1 >= 1) PlayerSpawn(RandomSpawn(playerSpawnPoints));
            }
            else if (isTwoPlayer)
            {
                if (currentPlayerOne == null && lives1 >= 1) PlayerSpawn(RandomSpawn(playerSpawnPoints));

                if (currentPlayerTwo == null && lives2 >= 1) TwoPlayerSpawn(RandomSpawn(playerSpawnPoints));
            }
        }
    }

    public void PlayerSpawn(GameObject spawnPoint) // Spawns Player 1 at the given spawn point if there is no active Player in the scene
    {
        if (!currentPlayerOne)
        {
            Health playerOneHealth;

            currentPlayerOne = Instantiate(playerOne, spawnPoint.transform.position, Quaternion.identity);
            playerOneHealth = currentPlayerOne.GetComponent<Health>();
            lives1 = 3;
            playerOneHealth.CurrentSheild = 0;
            numOfPlayers++;
        }
    }

    public void TwoPlayerSpawn(GameObject spawnPoint2) // Spawns Player 2 at the given spawn point if there is no active Player in the scene
    {
        if (numOfPlayers > 0 && !currentPlayerTwo)
        {
            Health playerTwoHealth;
            currentPlayerTwo = Instantiate(playerTwo, spawnPoint2.transform.position, Quaternion.identity);
            playerTwoHealth = currentPlayerTwo.GetComponent<Health>();
            lives2 = 3;
            playerTwoHealth.CurrentSheild = 0;
            numOfPlayers++;
        }
    }

    public GameObject RandomSpawn(List<GameObject> spawnPoints)
    {
        int spawnToGet = UnityEngine.Random.seed;
        spawnToGet = UnityEngine.Random.Range(0, spawnPoints.Count);
        return spawnPoints[spawnToGet];
    }

    void CheckGameStatus ()
    {
        if (lives1 <= 0 && lives2 <= 0)
        {
            sceneLoader.RunGameOver();
            SaveScore();
        }
    }

    SaveData CreateSave ()
    {
        SaveData save = new SaveData();

        save.score1 = score1;
        save.score2 = score2;

        return save;
    }

    public void SaveScore()
    {
        SaveData save = CreateSave();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/scoresave.sv" );
        bf.Serialize(file, save);
        file.Close();
    }
}