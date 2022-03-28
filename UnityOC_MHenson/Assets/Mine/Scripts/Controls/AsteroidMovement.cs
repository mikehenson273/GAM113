using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{ 
    #region variables
    public float AsteroidSpeed; //set speed if user wants to specify
    public bool IsRandomSpeed; //if player wants spawned asteroids to have differing speeds
    private Transform PlayerLocation; //grabs player's location
    private Vector3 LastLocation; //holds last location of player for asteroids to head straight there
    private Transform tf; //variable for object transformation
    private bool CheckedPlayer = false; //checks player
    public bool Landed = false; //lands in position
    public float asteroidHealth;
    public int asteroidPoints;

    private int x;
    private int y;
    private int z;

    private GameObject player1;
    #endregion

    // Use this for initialization
    void Start ()
    {
        x = UnityEngine.Random.Range(1, 11);
        y = UnityEngine.Random.Range(1, 11);
        z = UnityEngine.Random.Range(1, 11);
        if (asteroidHealth <= 0)
        {
            asteroidHealth = 50;
        }
        if (asteroidPoints <= 0)
        {
            asteroidPoints = 50;
        }

        PlayerHeading();  //finds player's location upon asteroid spawn

        if (IsRandomSpeed)//generate a random number to act as speed for Asteroid
        {
            RandomGenerateSpeed();
        }

        else
        {
            //do nothing
        }

        tf = GetComponent<Transform>(); //load current position into transform variable to keep things relative
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        AsteroidMoving();
        AsteroidLife();

        if (GameManagement.instance.Player1Alive == false)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D OtherObject)
    {
        if (OtherObject.tag == "Player1")
        {
            Destroy (OtherObject.gameObject);
            GameManagement.instance.player1Lifes -= 1;
        }
    }

    void RandomGenerateSpeed() //Generates random speed between 1-10 for the asteroid
    {
        AsteroidSpeed = Random.Range(11, 31);
    }

    void PlayerHeading()
    {
        if (!CheckedPlayer) //checks and grabs players current position as a vector2
        {
            PlayerLocation = GameObject.FindGameObjectWithTag("Player1").transform;
            LastLocation = PlayerLocation.transform.position;
            CheckedPlayer = true;
        }

        else if (CheckedPlayer) //move towards players last location
        {
            //do nothing
        }
    }

    void AsteroidLife()
    {
        if (asteroidHealth <= 0)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<MeshCollider>().enabled = false;
            MapBuilder enemyCount = GameObject.Find("BuildMap").GetComponent<MapBuilder>();
            enemyCount.enemyCounter -= 1;
            Fracture fractureAsteroid = gameObject.GetComponent<Fracture>();
            fractureAsteroid.FractureObject();
        }
    }

    void AsteroidMoving()
    {
        if (Landed == true)
        {
            tf.position = tf.position + (GetComponent<Transform>().forward * AsteroidSpeed * Time.deltaTime); //Asteroid moves past player's last location
            transform.Rotate(x, y, z, Space.Self);
        }

        else if (tf.position != LastLocation)
        {
            #region Rotational Region to determine the Asteroids rotation
            Vector3 NewDirection = LastLocation - transform.position;
            Vector3 ChangeDirection = Vector3.RotateTowards(transform.forward, NewDirection, 100 * Time.deltaTime, 1f);
            //Quaternion Rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(ChangeDirection, Vector3.back), 100 * Time.deltaTime);
            //Rotation.x = 45;
            //Rotation.y = 45;
            //Rotation.z = 45;
            //transform.rotation = Rotation;
            transform.Rotate(x, y, z, Space.Self);
            #endregion

            tf.position = Vector3.MoveTowards(transform.position, LastLocation, AsteroidSpeed * Time.deltaTime); //moves towards player's position on Spawn
        }

        else if (tf.position == LastLocation)
        {
            Landed = true;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player1") //ensures player 1 can't be damaged by their own attack
        {
            collision.gameObject.GetComponent<AllEntityControls>().shipHealth -= 20;

            gameObject.GetComponent<AudioSource>().PlayOneShot(GameManagement.instance.laserHit, GameManagement.instance.sfxVolume);

            asteroidHealth = 0;
        }
    }
}