using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public GameObject player; //holds the player as a game object for positional referencing
    private Vector3 offset; //how far to move the camera so it can move with the player

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position; //set offset equal to cam transform position - player transform

    }

    void LateUpdate() //runs after all updates are done
    {
        transform.position = player.transform.position + offset; //aligns camera into new position by adding the player position and offset together
    }


}
