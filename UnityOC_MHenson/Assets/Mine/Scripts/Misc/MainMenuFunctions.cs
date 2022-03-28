using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuFunctions : MonoBehaviour
{
    [HideInInspector] public MapBuilder variables;

    private void Start()
    {
        #region button sound fx
        GameManagement.instance.sfxSounds.clip = GameManagement.instance.buttonPress;
        #endregion
        Cursor.lockState = CursorLockMode.None; //unlocks from center of screen
        Cursor.visible = true; //shows cursor for user to use
    }

    public void isMultiplayer(bool multiPlayer) //determines if the game is set to multiplayer
    {
        if (GameManagement.instance.isTwoPlayer == false)
        {
            GameManagement.instance.isTwoPlayer = true;
        }
        else if (GameManagement.instance.isTwoPlayer == true)
        {
            GameManagement.instance.isTwoPlayer = false;
        }
    }

    public void MapOfTheDay(bool random) //determines if they want to use a map of the day
    {
        if (random)
        {
            GameManagement.instance.mapOfDay = true;
        }
        else if (!random)
        {
            GameManagement.instance.mapOfDay = false;
        }
    }

    public void RowAmount(string RowsAmount)
    {
        int amount = int.Parse(RowsAmount);
        if (amount <= 1) //if their number is less than or equal to one set it to two
        {
            GameManagement.instance.rowAmount = 2;
        }
        else if (amount > 1) //if their number is higher than 1 use it
        {
            GameManagement.instance.rowAmount = amount;
        }
    }

    public void ColumnAmount(string ColsAmount)
    {
        int amount = int.Parse(ColsAmount);
        if (amount <= 1) //if their number is less than or equal to one set it to two
        {
            GameManagement.instance.columnAmount = 2;
        }
        else if (amount > 1) //if their number is higher than 1 use it
        {
            GameManagement.instance.columnAmount = amount;
        }
    }

    public void seedNumber(string SeedNumber)
    {
        int seed = int.Parse(SeedNumber);
        if (seed < 0) //error catch for if someone tries to input lower value sets to 0
        {
            GameManagement.instance.seed = 0;
        }
        else //otherwise let them input their number
        {
            GameManagement.instance.seed = seed;
        }
    }

    public void LoadScene(string SceneName)
    {
        Time.timeScale = 1;
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

    public void ResetHighScore()
    {
        GameManagement.instance.highScore = 0;
        PlayerPrefs.SetFloat("3D Asteroids High Score", GameManagement.instance.highScore); //after resetting score save it to playerprefs
        PlayerPrefs.Save();
    }

    public void PlayButtonSound()
    {
        GameManagement.instance.sfxSounds.PlayOneShot(GameManagement.instance.buttonPress, GameManagement.instance.sfxVolume);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
