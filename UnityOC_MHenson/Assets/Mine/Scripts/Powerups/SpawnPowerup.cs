using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPowerup : MonoBehaviour
{
    public GameObject[] PowerUpTypes;
    private Transform powerupSpawnPoint;
    private int powerupSelected;

    private void Awake()
    {
        powerupSpawnPoint = gameObject.GetComponent<Transform>();
        powerupSelected = Random.Range(0, PowerUpTypes.Length);
        Instantiate(this.PowerUpTypes[powerupSelected], powerupSpawnPoint.transform.position,
            powerupSpawnPoint.transform.rotation);
    }
}
