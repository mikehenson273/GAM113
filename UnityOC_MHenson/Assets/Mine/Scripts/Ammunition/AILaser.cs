using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AILaser : MonoBehaviour
{
    #region variables
    public float ShellSpeed = 1f; //user can specify Shell speed
    public bool UsingTimer; //if user wants a timer
    public float ShellTime; //time user sets
    public float damageDealt;
    private Rigidbody shellBody;
    private int ShellTimer; //timer for shell
    private AudioSource sfxAudio;

    #endregion

    // Use this for initialization
    void Start ()
    {
        shellBody = GetComponent<Rigidbody>();
        sfxAudio = GetComponent<AudioSource>();
        if (UsingTimer) //start timer if user wants one
        {
            StartCoroutine("TimeUp");
        }
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (ShellTime == ShellTimer) //if user wants a timer count destroy Shell and stop timer when equal
        {
            Destroy(gameObject);
            StopCoroutine("TimeUp");
            ShellTimer = 0;
        }
        //tf.position = tf.position + ((transform.forward * ShellSpeed) / 10) + ((transform.up * ShellSpeed) / 10); //for use when dealing with parabolas
        //shellBody.AddForce(0, 1, 0); //for use when starting parabolas

        if (shellBody != null)
        {
            shellBody.AddForce(transform.forward * (ShellSpeed * 10));
        }
    }

    IEnumerator TimeUp()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            ShellTimer++;
        }
    }

    private void OnTriggerEnter(Collider ObjectCollision)
    {
        if (ObjectCollision.gameObject.tag == "Environment" || ObjectCollision.gameObject.tag == "Wall")
        {
            PlayAudio();
        }

        if (ObjectCollision.gameObject.tag == "Player1Body" || 
           (ObjectCollision.gameObject.GetComponent<CharacterController>() && ObjectCollision.gameObject.tag == "Player1Body"))
        {
            ObjectCollision.gameObject.GetComponentInParent<AllEntityControls>().shipHealth -= damageDealt;

            if (ObjectCollision.gameObject.GetComponentInParent<AllEntityControls>().shipHealth <= 0)
            {
                GameManagement.instance.Player1Alive = false;
            }
            PlayAudio();
        }
        else if (ObjectCollision.gameObject.tag == "Player1Barrel" ||
                (ObjectCollision.gameObject.GetComponent<CharacterController>() && ObjectCollision.gameObject.tag == "Player1Barrel"))
        {
            ObjectCollision.gameObject.GetComponentInParent<AllEntityControls>().shipHealth -= (damageDealt * 1.25f);

            if (ObjectCollision.gameObject.GetComponentInParent<AllEntityControls>().shipHealth <= 0)
            {
                GameManagement.instance.Player1Alive = false;
            }
            PlayAudio();
        }

        if (ObjectCollision.gameObject.tag == "Player2Body" ||
           (ObjectCollision.gameObject.GetComponent<CharacterController>() && ObjectCollision.gameObject.tag == "Player2Body"))
        {
            ObjectCollision.gameObject.GetComponentInParent<AllEntityControls>().shipHealth -= damageDealt;

            if (ObjectCollision.gameObject.GetComponentInParent<AllEntityControls>().shipHealth <= 0)
            {
                GameManagement.instance.player2Alive = false;
            }
            PlayAudio();
        }
        else if (ObjectCollision.gameObject.tag == "Player2Barrel" ||
                (ObjectCollision.gameObject.GetComponent<CharacterController>() && ObjectCollision.gameObject.tag == "Player2Barrel"))
        {
            ObjectCollision.gameObject.GetComponentInParent<AllEntityControls>().shipHealth -= (damageDealt * 1.25f);

            if (ObjectCollision.gameObject.GetComponentInParent<AllEntityControls>().shipHealth <= 0)
            {
                GameManagement.instance.player2Alive = false;
            }
            PlayAudio();
        }
    }

    void PlayAudio()
    {
        Destroy(shellBody);
        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        sfxAudio.PlayOneShot(GameManagement.instance.laserHit, GameManagement.instance.sfxVolume);
        Destroy(gameObject, GameManagement.instance.laserHit.length + .1f);
    }
}