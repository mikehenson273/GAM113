using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaser : MonoBehaviour
{
    #region variables
    
    #region floats and ints
    public float laserSpeed = 1f; //user can specify Shell speed
    public float laserTime; //time user sets
    public float damageDealt;
    private int laserTimer; //timer for shell
    #endregion

    #region bools
    public bool UsingTimer; //if user wants a timer
    #endregion
    
    #region gameobjects and transforms
    private Rigidbody laserBody;
    private PlayerSpaceShip laser;
    private AudioSource sfxAudio;
    #endregion

    #endregion

    // Use this for initialization
    void Start ()
    {
        if (UsingTimer) //start timer if user wants one
        {
            StartCoroutine("TimeUp");
        }

        //tf = GetComponent<Transform>(); //load current position into transform variable to keep things relative
        laserBody = GetComponent<Rigidbody>();
        sfxAudio = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (laserTime == laserTimer) //if user wants a timer count destroy Shell and stop timer when equal
        {
            Destroy(gameObject);
            StopCoroutine("TimeUp");
            laserTimer = 0;
        }
        //tf.position = tf.position + ((transform.forward * ShellSpeed) / 10) + ((transform.up * ShellSpeed) / 10); //for use when dealing with parabolas
        //shellBody.AddForce(0, 1, 0); //for use when starting parabolas
        if (laserBody != null)
        {
            //shellBody.velocity = ShellSpeed * 10;
            //shellBody.AddForce(transform.forward * (ShellSpeed * 10));
            //shellBody.
            //shellBody.AddRelativeForce(ShellSpeed * 10, 0, 0);
        }
    }

    IEnumerator TimeUp()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            laserTimer++;
        }
    }

    private void OnTriggerEnter(Collider ObjectCollision)
    {
        //if (ObjectCollision.gameObject.tag == "KillSphere" || ObjectCollision.gameObject.tag == "Wall")
        //{
        //    PlayAudio();
        //}

        if (ObjectCollision.gameObject.tag == "Asteroid")
        {
            ObjectCollision.gameObject.GetComponent<AsteroidMovement>().asteroidHealth -= damageDealt;

            if (ObjectCollision.gameObject.GetComponent<AsteroidMovement>().asteroidHealth <= 0)
            {
                if (gameObject.tag == "Player1Laser") //if destroyed by player1
                {
                    GameManagement.instance.player1Points += ObjectCollision.gameObject.GetComponent<AsteroidMovement>().asteroidPoints / 2;
                }

                if (gameObject.tag == "Player2Laser") //if destroyed by player2
                {
                    GameManagement.instance.player2Points += ObjectCollision.gameObject.GetComponent<AsteroidMovement>().asteroidPoints / 2;
                }
            }
            PlayAudio();
        }
        /*if (ObjectCollision.gameObject.tag == ("EnemyLaser"))
        {
            Destroy(ObjectCollision.gameObject);
            PlayAudio();
            Destroy(gameObject);
        }*/
        if (ObjectCollision.gameObject.tag == ("Player1") && gameObject.tag != "Player1Laser") //ensures player 1 can't be damaged by their own attack
        {
            ObjectCollision.gameObject.GetComponent<AllEntityControls>().shipHealth -= (damageDealt);

            if (ObjectCollision.gameObject.GetComponent<AllEntityControls>().shipHealth <= 0)
            {
                if (gameObject.tag == "Player2Laser") //if player1 is attacked and destroyed by player2
                {
                    GameManagement.instance.player2Points += ObjectCollision.gameObject.GetComponent<AllEntityControls>().entityPointWorth / 2;
                }
            }
        }
        if (ObjectCollision.gameObject.tag == ("Player2") && gameObject.tag != "Player2Laser")
        {
            ObjectCollision.gameObject.GetComponent<AllEntityControls>().shipHealth -= (damageDealt);

            if (ObjectCollision.gameObject.GetComponent<AllEntityControls>().shipHealth <= 0)
            {
                if (gameObject.tag == "Player1Laser") //if player2 is attacked and destroyed by player1
                {
                    GameManagement.instance.player1Points += ObjectCollision.gameObject.GetComponent<AllEntityControls>().entityPointWorth / 2;
                }
            }
        }
    }

    void PlayAudio()
    {
        Destroy(laserBody);
        gameObject.GetComponentInChildren<Collider>().enabled = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        sfxAudio.PlayOneShot(GameManagement.instance.laserHit, GameManagement.instance.sfxVolume);
        Destroy(gameObject, GameManagement.instance.laserHit.length + .1f);
    }
}