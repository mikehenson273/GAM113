using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fracture : MonoBehaviour
{
    [Tooltip("\"Fractured\" is the object that this will break into")]
    public GameObject fractured;

    private bool destroyedAsteroid = false;

    public void FractureObject()
    {
        if (destroyedAsteroid == false)
        {
            for (int i = 0; i < 2; i++)
            {
                Instantiate(fractured, transform.position, transform.rotation); //Spawn in the broken version
            }
            destroyedAsteroid = true;
        }

        fractured.transform.Rotate(15, 15, 15, Space.Self);

        Destroy(gameObject, GameManagement.instance.laserHit.length + .1f); //my line which waits for sound to stop playing before destroying it
        //Destroy(gameObject); //og line Destroy the object to stop it getting in the way
    }
}
