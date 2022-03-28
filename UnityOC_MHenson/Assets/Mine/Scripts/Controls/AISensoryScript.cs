using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISensoryScript : MonoBehaviour
{
    // data
    public float sightDistance = 10; // How far they can see
    public float lineOfSight = 60; // View angle (Field of View)
    public float hearingDistance = 1.0f; // How well they can hear. If this is 1.0, they hear "normally", otherwise, this is deafness/superhearing
    [HideInInspector] public Vector3 playerVector;
    private float playerDistance_;

    // Components
    private Transform tankTF;
    // Use this for initialization
    void Start()
    {
        tankTF = GetComponent<Transform>();
    }

    public bool CanSee(GameObject target)
    {
        if (!target.CompareTag("Enemy")) //error check to try and prevent an AI from seeing itself
        {
            // If they do not have a collider, they are invisible
            Transform targetController = target.GetComponent<Transform>();

            // If they are outside the view angle, we cannot see them
            // To check, we need the vector to our target, and compare that angle to our forward vector
            playerVector = targetController.position - tankTF.position;
            playerVector.Normalize();

            if (Vector3.Angle(tankTF.forward, playerVector) <= (lineOfSight / 2))
            {
                int playerMask = 1 << 10;
                int powerUpMask = 1 << 12;
                int playerLayerMask = (1 << playerMask | ~(1 << playerMask));
                int powerLayerMask = (1 << powerUpMask | ~(1 << powerUpMask));
                RaycastHit hasHit;

                // If they are in our field-of-view (thus we could get here), 
                //     raycast to make sure nothing is blocking our view

                if (Physics.Raycast(tankTF.position, playerVector, out hasHit, sightDistance, playerLayerMask) ||
                    Physics.Raycast(tankTF.position, playerVector, out hasHit, sightDistance, powerLayerMask))
                {
                    // If our raycast hit the player first, then we can see them
                    if (hasHit.collider.CompareTag("Player1Body") ||
                        hasHit.collider.CompareTag("Player1") ||
                        hasHit.collider.CompareTag("Player2Body") ||
                        hasHit.collider.CompareTag("Player2") ||
                        hasHit.collider.CompareTag("PowerUp"))
                    {
                        //Debug.Log(gameObject.name + " play Hit " + hasHit.collider.name);
                        //Debug.DrawLine(tankTF.position, tankTF.position + playerVector * sightDistance, Color.green);
                        return true;
                    }

                    /*// If our raycast hits a powerup first, then we can see it
                    else if (hasHit.collider.CompareTag("PowerUp"))
                    {
                        Debug.Log(gameObject.name + " play Hit " + hasHit.collider.name);
                        Debug.DrawLine(tankTF.position, tankTF.position + playerVector * sightDistance, Color.green);
                        return false;
                    }*/
                }
            }
            // otherwise, if we hit something else or nothing we failed to see them
            //Debug.Log(gameObject.name + " Hit Nothing");
        }
        return false;
    }
}