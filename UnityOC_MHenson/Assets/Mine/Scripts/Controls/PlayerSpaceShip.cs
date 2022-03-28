using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpaceShip : MonoBehaviour
{
    #region variables
    #region floats and ints
    [Header("Movement")]
    public float timeBetweenFire;
    public float shipSpinSpeed;
    private float fireTimer;
    [HideInInspector]
    public float originalLife;
    #endregion

    #region bools
    private bool hasFired;
    #endregion

    #region gameobjects
    private AllEntityControls basicShipInfo;
    public GameObject ammunition;
    public GameObject player;
    public AudioSource sfxAudio;

    #endregion

    #endregion

    public Camera shipCam;
    public Transform attackPoint1;
    public Transform attackPoint2;
    Ray playerBulletRay;

    // Use this for initialization
    void Start()
    {
        basicShipInfo = gameObject.GetComponent<AllEntityControls>();
        originalLife = basicShipInfo.shipHealth;
        //ammunition = gameObject.GetComponent<AllEntityControls>().Ammo;
        sfxAudio = GetComponent<AudioSource>();
        player = gameObject;
        basicShipInfo.moveForwardSpeed = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player.tag == ("Player1"))
        {
            StatMonitor();
            Player1Moving();
        }
        #region player 2
        /*if (player.tag == ("Player2"))
        {
            StatMonitor();
            if (Input.GetKey(KeyCode.I))
            {
                motor.Movement(tankInfo_.moveForwardSpeed); //forward movement at normal speed
            }
            else if (Input.GetKey(KeyCode.K))
            {
                motor.Movement(-tankInfo_.moveBackSpeed); //allows backward movement for slower speed
            }

            if (Input.GetKey(KeyCode.L))
            {
                motor.Rotation(tankInfo_.turnSpeed); //turn clockwise
            }
            else if (Input.GetKey(KeyCode.J))
            {
                motor.Rotation(-tankInfo_.turnSpeed); //turn counterclockwise
            }

            if (Input.GetKeyDown(KeyCode.Semicolon) && !hasFired && timeBetweenFire >= fireTimer) //fires a shell
            {
                GameObject ammoClone = Instantiate(ammunition, gameObject.GetComponent<TankData>().FirePoint.position, transform.rotation);

                if (gameObject.tag == "Player2")
                {
                    ammoClone.tag = "Player2Shell";
                    ammoClone.GetComponent<PlayerShell>().damageDealt = gameObject.GetComponent<TankData>().shellDamageValue;
                    sfxAudio.PlayOneShot(GameManagement.instance.tankFire, GameManagement.instance.sfxVolume);
                }
                StartCoroutine("TimeUp");
                hasFired = true;
            }

            else if (hasFired && timeBetweenFire <= fireTimer)
            {
                StopCoroutine("TimeUp");
                fireTimer = 0;
                hasFired = false;
            }
        }*/
        #endregion
    }

    void Player1Moving()
    {
        if (Input.GetKeyDown(KeyCode.R)) //resets the rotation inputs to give the player back control
        {
            CameraRotation mouseValues = GetComponent<CameraRotation>();
            mouseValues.rotating.x = 0;
            mouseValues.rotating.y = 0;
        }

        if (Input.GetKey(KeyCode.W))
        {
            basicShipInfo.Movement(basicShipInfo.moveForwardSpeed); //forward movement at normal speed
            #region normal speeds
            if (basicShipInfo.moveForwardSpeed <= basicShipInfo.origFwdSpeed * 2)
            {
                basicShipInfo.moveForwardSpeed = basicShipInfo.moveForwardSpeed + basicShipInfo.origFwdSpeed;
            }
            else if (basicShipInfo.moveForwardSpeed > basicShipInfo.origFwdSpeed * 2 && !Input.GetKey(KeyCode.LeftShift))
            {
                basicShipInfo.moveForwardSpeed = basicShipInfo.origFwdSpeed * 2;
            }
            #endregion

            #region left shift speeds
            else if (Input.GetKey(KeyCode.LeftShift))
            {
                if (basicShipInfo.moveForwardSpeed <= basicShipInfo.origFwdSpeed * 4)
                {
                    basicShipInfo.moveForwardSpeed = basicShipInfo.moveForwardSpeed + basicShipInfo.origFwdSpeed;
                }
                else if (basicShipInfo.moveForwardSpeed > basicShipInfo.origFwdSpeed * 4)
                {
                    basicShipInfo.moveForwardSpeed = basicShipInfo.origFwdSpeed * 4;
                }
            }
            #endregion
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (basicShipInfo.moveForwardSpeed > 0)
            {
                basicShipInfo.moveForwardSpeed = basicShipInfo.moveForwardSpeed - (basicShipInfo.origFwdSpeed * 0.1f);
                basicShipInfo.Movement(basicShipInfo.moveForwardSpeed); //forward movement at normal speed
            }
            basicShipInfo.Movement(basicShipInfo.moveBackSpeed); //allows backward movement for slower speed
        }

        else if (!Input.GetKey(KeyCode.W))
        {
            if (basicShipInfo.moveForwardSpeed > 0)
            {
                basicShipInfo.moveForwardSpeed = basicShipInfo.moveForwardSpeed - (basicShipInfo.origFwdSpeed * 0.01f);
                basicShipInfo.Movement(basicShipInfo.moveForwardSpeed); //forward movement at normal speed
            }
            else
            {
                basicShipInfo.moveForwardSpeed = 0;
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            //basicShipInfo.Rotation(basicShipInfo.turnSpeed); //turn clockwise
            transform.Rotate(0, 0, -shipSpinSpeed, Space.Self);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            //basicShipInfo.Rotation(-basicShipInfo.turnSpeed); //turn counterclockwise
            transform.Rotate(0, 0, shipSpinSpeed, Space.Self);
        }

        if (Input.GetKey(KeyCode.Mouse0) && !hasFired && timeBetweenFire >= fireTimer) //fires a shell
        {
            Player1Shooting();
        }

        else if (hasFired && timeBetweenFire <= fireTimer)
        {
            StopCoroutine("TimeUp");
            fireTimer = 0;
            hasFired = false;
        }
    }

    void Player1Shooting()
    {
        //playerBulletRay = new Ray(gameObject.GetComponent<TankData>().FirePoint.position, transform.forward); //raycast from launch point forward
        //Ray playerBulletRay1 = shipCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); //raycast to center of screen
        Ray playerBulletRay1 = shipCam.ScreenPointToRay(Input.mousePosition); //raycast to where mouse cursor is
        Debug.DrawRay(playerBulletRay1.origin, playerBulletRay1.direction, Color.red);
        RaycastHit rayHitSomething;
        Vector3 targetPoint;
        if (Physics.Raycast(playerBulletRay1, out rayHitSomething))
        {
            targetPoint = rayHitSomething.point;
        }
        else
        {
            targetPoint = playerBulletRay1.GetPoint(75); //just a point far away from the player
        }
        Vector3 directionWithoutBloom = targetPoint - attackPoint1.position;

        //Quaternion localShipRotation = GetComponent<Transform>().rotation;
        GameObject ammoClone1 = Instantiate(ammunition, attackPoint1.position, Quaternion.identity);
        GameObject ammoClone2 = Instantiate(ammunition, attackPoint2.position, Quaternion.identity);
        ammoClone1.transform.forward = directionWithoutBloom.normalized;
        ammoClone1.GetComponent<Rigidbody>().AddForce(directionWithoutBloom.normalized * ammoClone1.GetComponent<PlayerLaser>().laserSpeed * 100);
        ammoClone2.transform.forward = directionWithoutBloom.normalized;
        ammoClone2.GetComponent<Rigidbody>().AddForce(directionWithoutBloom.normalized * ammoClone2.GetComponent<PlayerLaser>().laserSpeed * 100);

        if (gameObject.tag == "Player1")
        {
            ammoClone1.tag = "Player1Laser";
            ammoClone1.GetComponent<PlayerLaser>().damageDealt = basicShipInfo.laserDamageValue;
            ammoClone2.tag = "Player1Laser";
            ammoClone2.GetComponent<PlayerLaser>().damageDealt = basicShipInfo.laserDamageValue;
            sfxAudio.PlayOneShot(GameManagement.instance.shipFire, GameManagement.instance.sfxVolume);
        }
        StartCoroutine("TimeUp");
        hasFired = true;
    }

    private void StatMonitor() //monitors players health and when it reaches 0 reset stats back to normal
    {
        if (basicShipInfo.shipHealth <= 0)
        {
            sfxAudio.PlayOneShot(GameManagement.instance.shipDestruction, GameManagement.instance.sfxVolume);
            basicShipInfo.shipHealth = originalLife;
            basicShipInfo.moveForwardSpeed = basicShipInfo.origFwdSpeed;
            basicShipInfo.moveBackSpeed = basicShipInfo.origBwdSpeed;
            basicShipInfo.turnSpeed = basicShipInfo.origTurnSpeed;
            basicShipInfo.laserDamageValue = basicShipInfo.origlaserDamage;
            GameManagement.instance.Player1Alive = false;

            MapBuilder enemyCount = GameObject.Find("BuildMap").GetComponent<MapBuilder>();
            enemyCount.enemyCounter = 0;
        }
    }

    /*private void PlayMovingSound() //would've implemented but didn't have enough time
    {
        if (!currentlyMoving)
        {
            return; //user stops moving stop making sound
        }
        if (movingSound < movingDelay)
        {
            movingSound += Time.deltaTime; //adds delay so it doesn't keep going
        }
        else
        {
            movingSound = 0; //resets time for delay
            sfxAudio.PlayOneShot(GameManagement.instance.tankMoving, GameManagement.instance.sfxVolume);
        }
    }*/

    IEnumerator TimeUp()
    {
        while (true)
        {
            yield return new WaitForSeconds(.25f);
            fireTimer++;
        }
    }
}
