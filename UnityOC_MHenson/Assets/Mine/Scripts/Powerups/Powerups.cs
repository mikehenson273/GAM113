using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour
{
    #region Variables
    public enum PowerUpType {SpeedBoost, DamageBoost, FireRateBoost}
    public PowerUpType ChosenPowerUp;
    public float duration;
    #region bools
    private bool isSpeedBooster;
    private bool isDamageBooster;
    private bool fireRateBooster;
    #endregion
    #region ints/floats
    #region SpeedBoost
    public float increaseMoveAmount;
    public float increaseTurnAmount;
    #endregion
    #region DamageBoost
    public float boostDamage;
    #endregion
    #region RateOfFireBoost
    public float fireRateIncrease;
    private AllEntityControls playerStats;
    #endregion
    #endregion

    public GameObject SFX;
    private AudioSource sfxAudio;
    #endregion

    private void Awake()
    {
        sfxAudio = GetComponent<AudioSource>();
        switch (ChosenPowerUp)
        {
            case PowerUpType.SpeedBoost:
                {
                    isSpeedBooster = true;
                    return;
                }
            case PowerUpType.DamageBoost:
                {
                    isDamageBooster = true;
                    return;
                }
            case PowerUpType.FireRateBoost:
                {
                    fireRateBooster = true;
                    return;
                }

            default:
                {
                    Debug.Log("Invalid type Was Selected");
                    break;
                }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2") || other.CompareTag("Enemy"))
        {
            StartCoroutine(PickUp(other));
        }
    }

    IEnumerator PickUp(Collider entity)
    {
        //Instantiate(SFX, transform.position, transform.rotation);
        AllEntityControls stats = entity.GetComponent<AllEntityControls>();
        GetComponent<MeshRenderer>().enabled = false;
#pragma warning disable CS0618 // Type or member is obsolete
        GetComponent<ParticleSystem>().enableEmission = false; //unsure about newer method of this
#pragma warning restore CS0618 // Type or member is obsolete
        GetComponent<Collider>().enabled = false;

        if (isSpeedBooster)
        {
            sfxAudio.PlayOneShot(GameManagement.instance.speedBoost, GameManagement.instance.sfxVolume);
            stats.moveForwardSpeed += increaseMoveAmount;
            stats.moveBackSpeed += increaseMoveAmount;
            stats.turnSpeed += increaseTurnAmount;
        }
        if (isDamageBooster)
        {
            sfxAudio.PlayOneShot(GameManagement.instance.damageBoost, GameManagement.instance.sfxVolume);
            stats.laserDamageValue += boostDamage;
        }
        if (fireRateBooster)
        {
            if (entity.CompareTag("Player1") || entity.CompareTag("Player2"))
            {
                PlayerSpaceShip playerStats = entity.GetComponent<PlayerSpaceShip>();
                playerStats.timeBetweenFire -= fireRateIncrease;
            }
            sfxAudio.PlayOneShot(GameManagement.instance.fireRateBoost, GameManagement.instance.sfxVolume);
        }

        yield return new WaitForSeconds(duration);

        if (isDamageBooster)
        {
            if (entity.CompareTag("Enemy"))
            {
                stats.laserDamageValue -= boostDamage;
            }
        }
        if (fireRateBooster)
        {
            if (entity.CompareTag("Player1") || entity.CompareTag("Player2"))
            {
                PlayerSpaceShip playerStats = entity.GetComponent<PlayerSpaceShip>();
                playerStats.timeBetweenFire += fireRateIncrease;
            }
        }

        GetComponent<MeshRenderer>().enabled = true;
#pragma warning disable CS0618 // Type or member is obsolete
        GetComponent<ParticleSystem>().enableEmission = true;
#pragma warning restore CS0618 // Type or member is obsolete
        GetComponent<Collider>().enabled = true;
    }
}