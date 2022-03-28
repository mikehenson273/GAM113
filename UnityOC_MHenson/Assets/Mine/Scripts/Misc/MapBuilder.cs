using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBuilder : MonoBehaviour
{
    public List<GameObject> possiblePlayerSpawns;
    public GameObject[] asteroidList;
    public PlayerSpaceShip player1;
    //public GameObject player2; //for if you play 2 player

    [HideInInspector] public int enemyCounter;
    public int enemiesOnScreen;
    public Vector3 player1Positioning;

    private void Awake()
    {
        if (enemiesOnScreen == 0)
        {
            enemiesOnScreen = 20;
        }

        enemyCounter = 0;
        player1 = GameManagement.instance.player1.GetComponent<PlayerSpaceShip>();
    }

    void Start()
    {
        #region starts main game music
        GameManagement.instance.gameMusic.Stop();
        GameManagement.instance.gameMusic.clip = GameManagement.instance.mainGameMusic;
        GameManagement.instance.gameMusic.Play();
        #endregion
        InitialPlayerSpawn();
        GameManagement.instance.possiblePlayerSpawns = possiblePlayerSpawns;

    }

    private void FixedUpdate()
    {
        Spawning();
    }

    void InitialPlayerSpawn() //creates initial spawn for single or multiplayer
    {
        GameObject player1 = GameManagement.instance.player1; //sets up initial player one here
        GameObject player2 = GameManagement.instance.player2; //sets up initial player two here

        possiblePlayerSpawns.AddRange(GameObject.FindGameObjectsWithTag("PlayerSpawnPoint")); //adds player spawns here for initial spawning

        int playerSpawnPoint = UnityEngine.Random.Range(0, possiblePlayerSpawns.Count); //chooses a random initial spawn for player 1
        int player1SpawnPoint = playerSpawnPoint;

        Instantiate(player1, this.possiblePlayerSpawns[playerSpawnPoint].transform.position,
                this.possiblePlayerSpawns[playerSpawnPoint].transform.rotation); //clones player one at a random spawn initially

        GameManagement.instance.player1 = GameObject.FindWithTag("Player1");

        if (GameManagement.instance.isTwoPlayer) //creates initial spawn for second player IF the game mode is chose for player 2
        {
            playerSpawnPoint = UnityEngine.Random.Range(0, possiblePlayerSpawns.Count); //chooses another random initial spawn for player 2
            if (playerSpawnPoint == player1SpawnPoint) //creates an "error" catch so players 1 and 2 can't spawn at the same location
            {
                playerSpawnPoint += 1; //moves to the next location
                if (playerSpawnPoint > possiblePlayerSpawns.Count) //if next location is higher than the possible spawn locations subtract 2
                    //to go to one before player 1 spawn location
                {
                    playerSpawnPoint -= 2;
                }
            }
            Instantiate(player2, this.possiblePlayerSpawns[playerSpawnPoint].transform.position,
        this.possiblePlayerSpawns[playerSpawnPoint].transform.rotation); //clones player two at a random spawn initially
            GameManagement.instance.player2 = GameObject.FindWithTag("Player2");
        }
        GameManagement.instance.possiblePlayerSpawns.AddRange(GameObject.FindGameObjectsWithTag("PlayerSpawnPoint"));
            //sets up player spawns in gamemanagement area
    }

    void Spawning()
    {
        if (player1 != null)
        {
            if (enemyCounter < enemiesOnScreen)
            {
                //spawn them randomly here
                Vector3 spawnPosition = GameObject.FindGameObjectWithTag("Player1").transform.position;
                spawnPosition += UnityEngine.Random.insideUnitSphere * 500;
                //int SpawnPoints = UnityEngine.Random.Range(0, SpawnLocations.Length);
                int entityToSpawn = UnityEngine.Random.Range(0, asteroidList.Length);
                Instantiate(this.asteroidList[entityToSpawn], spawnPosition, Quaternion.identity);
                enemyCounter++;
            }

            else
            {
                //do nothing
            }
        }
    }
}