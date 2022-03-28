using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillSphere : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Asteroid" || other.gameObject.tag == "Player1Laser" || other.gameObject.tag == "Player2Laser"
            || other.gameObject.tag == "EnemyLaser" || other.gameObject.tag == "Enemy")
        {
            if (other.gameObject.tag == "Asteroid" || other.gameObject.tag == "Enemy")
            {
                MapBuilder enemyCount = GameObject.Find("BuildMap").GetComponent<MapBuilder>();
                enemyCount.enemyCounter -= 1;
                //Debug.Log("Enemy Count decreased by 1");
            }
            //Debug.Log(gameObject.tag + " has left the kill sphere");
            Destroy(other.gameObject);
        }
    }
}
