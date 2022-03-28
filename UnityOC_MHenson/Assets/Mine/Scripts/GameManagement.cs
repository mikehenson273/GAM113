using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour
{
    #region variables
    #region GameObjects
    public static GameManagement instance; //holds singular instance
    [HideInInspector]
    public GameObject player1, //holds player prefabs
                      player2;
    //[HideInInspector]
    public List<GameObject> possiblePlayerSpawns;
    #endregion
    #region ints/floats
    public int player1Lifes, //player 1 lifes
               player2Lifes, //player 2 lifes
               player1LifeUp; //how many points required to level up for player 1
    [HideInInspector]
    public int player2LifeUp, //how many points required to level up for player 2
               origLifeUp, //stores the original life up amount
               origP1Lives,
               origP2Lives,
               rowAmount,
               columnAmount,
               seed,
               playerSpawnPoint;
    [HideInInspector]
    public float player1Points = 0,
                 player2Points = 0,
                 highScore = 500,
                 sfxVolume = .5f,
                 musicVolume = .5f;
   
    #endregion
    #region bools
    [HideInInspector]
    public bool Player1Alive = true, //checks if player one is alive
                player2Alive = true, //future milestone checks if player 2 is alive
                GameOver,
                IsRetrying,
                mapOfDay,
                isTwoPlayer = false;
    #endregion
    #region audio
    [Header("Audio Settings")]
    public AudioClip mainMenuMusic;
    public AudioClip mainGameMusic,
                     gameOverMusic,
                     buttonPress,
                     damageBoost,
                     speedBoost,
                     fireRateBoost,
                     shipDestruction,
                     laserHit,
                     shipFire,
                     shipMoving;
    [HideInInspector]
    public AudioSource gameMusic,
                       sfxSounds;
    #endregion
    #endregion

    void Awake()
    {
        LoadScore(); //loads score on game load
        //If an instance hasn't been created
        if (instance == null)
        {
            instance = this; //store current instance
            DontDestroyOnLoad(gameObject); //Don't delete if a new scene is loaded
            #region setting up variables
            #region lives
            origLifeUp = player1LifeUp;
            origP1Lives = player1Lifes; //sets up an original amount for user defined amount for player 1
            origP2Lives = player2Lifes; //sets up an original amount for user defined amount for player 2
            player2LifeUp = player1LifeUp; //player 2 always has the same starting life up value as player 1
            #endregion
            #region audio
            sfxVolume = .5f;
            musicVolume = .5f;
            gameMusic = GetComponentInChildren<AudioSource>(); //sets up music source
            sfxSounds = gameObject.AddComponent<AudioSource>(); //adds a new source for tank destruction
            #endregion
            #endregion
            //IsRetrying = false;
        }

        else
        {
            possiblePlayerSpawns.RemoveRange(0, possiblePlayerSpawns.Count);
            Destroy(this.gameObject); //Destroy new instance
            Debug.Log("Error - A new game manager was detected and promptly deleted");
        }
    }

	
	// Update is called once per frame
	void Update ()
    {
        #region sound adjustment area
        sfxSounds.volume = sfxVolume;
        gameMusic.volume = musicVolume;
        #endregion
        #region player 1 camera and object error check
        if (player1 != null)
        {
            if (!isTwoPlayer)
            {
                player1.GetComponentInChildren<Camera>().rect = new Rect(0, 0f, 1f, 1f); //if singleplayer use normal camera
            }
            else if (isTwoPlayer && Player1Alive)
            {
                if (player2Lifes > 0) //if player 2  has more lives than 0
                {
                    player1.GetComponentInChildren<Camera>().rect = new Rect(0, .5f, 1f, .5f); //if multiplayer and both players alive use modified view
                    player2.GetComponentInChildren<Camera>().rect = new Rect(0, 0, 1f, .5f);
                }
                if (player1Lifes <= 0)
                {
                    player2.GetComponentInChildren<Camera>().rect = new Rect(0, 0f, 1f, 1f); //if player1 is dead switch player 2 view to full screen
                }
                else if (player2Lifes <= 0) //if player 2 lifes are less than or equal to zero use normal view for player1
                {
                    player1.GetComponentInChildren<Camera>().rect = new Rect(0, 0f, 1f, 1f); //if 2nd player is dead
                }
            }
        }
        #endregion
        ScoreFunction();
        LifeFunction();
    }

    void ScoreFunction()
    {
        if (player1Points >= player1LifeUp) //if player score equals the neccessary amount grant extra life
        {
            player1Lifes += 1;
            player1LifeUp = player1LifeUp + 500; //raises the required amount by an additional 500
        }
        if (player2Points >= player2LifeUp)
        {
            player2Lifes += 1;
            player2LifeUp = player2LifeUp + 500;
        }
        else
        {
            //do nothing
        }
    }

    void LifeFunction()
    {
        if (!Player1Alive && player1Lifes >= 0) //creates circumstance for checking if player is alive or not and if their life is more than 0
        {
            player1Lifes -= 1;
            if (player1Lifes > 0) //if player still has lives move them to new random location
            {
                playerSpawnPoint = Random.Range(0, possiblePlayerSpawns.Count);
                sfxSounds.PlayOneShot(shipDestruction, sfxVolume);
                player1.transform.position = possiblePlayerSpawns[playerSpawnPoint].transform.position;
                player1.transform.rotation = possiblePlayerSpawns[playerSpawnPoint].transform.rotation;
                Player1Alive = true;
            }
            if (player1Lifes <= 0)
            {
                player1.SetActive(false); //hides player 1 object
            }
            Player1Alive = true; //set up to prevent AI problems
        }

        if (!isTwoPlayer)
        {
            if (player1Lifes == 0)
            {
                SceneManager.LoadScene("GameOver");
            }
        }

        else if (isTwoPlayer)
        {
            if (!player2Alive && player2Lifes > 0) //creates circumstance for checking if player is alive or not and if their life is more than 0
            {
                player2Lifes -= 1;
                if (player2Lifes > 0) //if player still has lives move them to new random location
                {
                    playerSpawnPoint = Random.Range(0, possiblePlayerSpawns.Count);
                    sfxSounds.PlayOneShot(shipDestruction, sfxVolume);
                    player2.transform.position = possiblePlayerSpawns[playerSpawnPoint].transform.position;
                    player2.transform.rotation = possiblePlayerSpawns[playerSpawnPoint].transform.rotation;
                    player2Alive = true;
                }
                if (player2Lifes <= 0)
                {
                    player2.SetActive(false); //deletes player 2 object if life amount is 0 or less
                }
                player2Alive = true; //set up to prevent AI problems
            }

            if (player1Lifes == 0 && player2Lifes == 0)
            {
                SceneManager.LoadScene("GameOver");
            }
        }
    }
    private void LoadScore()
    {
        highScore = PlayerPrefs.GetFloat("3D Asteroids High Score");
    }
}