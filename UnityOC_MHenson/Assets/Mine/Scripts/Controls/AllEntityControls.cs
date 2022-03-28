using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllEntityControls : MonoBehaviour
{
    #region Variables
    [Header("Ship Stats")]
    public float moveForwardSpeed;
    public float moveBackSpeed,
             turnSpeed,
             shipHealth,
             laserDamageValue,
             entityPointWorth;
    [HideInInspector]
    public float origFwdSpeed,
                 origBwdSpeed,
                 origTurnSpeed,
                 origHealth,
                 origlaserDamage;

    private CharacterController myCharacterControls;

    #endregion

    // Start is called before the first frame update
    void Awake()
    {
        #region Initial Stat Holders for powerups
        origFwdSpeed = moveForwardSpeed;
        origBwdSpeed = moveBackSpeed;
        origTurnSpeed = turnSpeed;
        origHealth = shipHealth;
        origlaserDamage = laserDamageValue;
        #endregion
        myCharacterControls = gameObject.GetComponent<CharacterController>(); //loads character controller into a variable
    }

    //moves tank
    public void Movement(float speed)
    {
        //holds speed data, starts it facing same direction as gameobject containing, and applies speed to vector instead of being one unit
        Vector3 speedVector = transform.forward * speed;

        /*call simplemove and send to vector
         note that Time.deltaTime is already applieed and converting to meters per second*/
        if (gameObject.GetComponent<CharacterController>().enabled)
        {
            if (speed != 0)
            {
                myCharacterControls.Move(speedVector);
            }
        }
    }

    //rotates tank
    public void Rotation(float speed)
    {
        //creates vector holding rotation data around yaw (y) axis, adjusts rotation depending on speed and changes to per second instead of every frame
        //otherwise it would rotate too fast
        Vector3 vectorRotation = Vector3.up * speed * Time.deltaTime;

        //rotate tank by set value inside of local space and not the world space
        transform.Rotate(vectorRotation, Space.Self);
    }
}
