using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public GameObject player1,
                      player2;
    private GameOverScript players; //hard set players for players
    // Use this for initialization
    void Start ()
    {
        #region in case coming back from game through pause menu reset time and let audio play
        Time.timeScale = 1; //sets back to normal time
        AudioListener.pause = false; //starts playing audio once again
        #endregion
        #region set player characters and set to single player initially
        GameManagement.instance.isTwoPlayer = false;
        GameManagement.instance.player1 = player1; //sets/resets player to original player character
        GameManagement.instance.player2 = player2; //sets/resets player to original player character
        #endregion
        #region set up starting values if at main menu
        GameManagement.instance.player1Points = 0;
        GameManagement.instance.player2Points = 0;
        GameManagement.instance.player1LifeUp = GameManagement.instance.origLifeUp;
        GameManagement.instance.player2LifeUp = GameManagement.instance.origLifeUp;
        GameManagement.instance.player1Lifes = GameManagement.instance.origP1Lives;
        GameManagement.instance.player2Lifes = GameManagement.instance.origP2Lives;

        #endregion
        #region Start main menu music
        GameManagement.instance.gameMusic.loop = true; //loops music so there's no silence
        GameManagement.instance.gameMusic.clip = GameManagement.instance.mainMenuMusic;
        GameManagement.instance.gameMusic.volume = GameManagement.instance.musicVolume;
        GameManagement.instance.gameMusic.Play(); //plays main menu music clip
        #endregion
    }
}
