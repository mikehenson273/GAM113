using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseButtonControls : MonoBehaviour
{
    PauseMenu PauseControl;

	// Use this for initialization
	void Start ()
    {
        PauseControl = GameObject.FindGameObjectWithTag("PauseManager").GetComponent<PauseMenu>();
	}

    public void UnPause() //to allow unpausing via button press on canvas
    {
        PauseControl.IsPaused = false; //unpauses the game once check pause is run
        PauseControl.CheckPause(); //starts the check to see if paused
    }

    public void LoadCanvas(string CanvasIndex1)
    {
        PauseControl.canvasIndex = CanvasIndex1; //sets the canvas index for pausecontrol in pause menu equal to the canvas index here
    }

    public void LoadScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

    public void SFXVolumeChange(float SFXVolume) //changes sfx volume in gamamanagement
    {
        GameManagement.instance.sfxVolume = SFXVolume;
    }

    public void MusicVolumeChange(float MusicVolume)
    {
        GameManagement.instance.musicVolume = MusicVolume;
    }

    public void PlayButtonSound()
    {
        GameManagement.instance.sfxSounds.PlayOneShot(GameManagement.instance.buttonPress, GameManagement.instance.sfxVolume);
    }
}
