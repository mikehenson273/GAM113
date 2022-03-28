using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteFractures : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 4); //cleans up asteroid debris after 4 seconds have passed
    }
}
