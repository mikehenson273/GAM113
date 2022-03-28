using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
    public Text player1ScoreToPut,
                 player2ScoreToPut,
                 highScoreToPut;
    public GameObject player1Score,
                      player2Score,
                      highScore;

    private float oldHigh;
    public GameObject player1;
    public GameObject player2;
	// Use this for initialization
	void Start ()
    {
        #region setting up text fields
        player1Score = transform.Find("Player1Score").gameObject;
        player1ScoreToPut = player1Score.GetComponent<Text>();
        player2Score = transform.Find("Player2Score").gameObject;
        player2ScoreToPut = player2Score.GetComponent<Text>();
        highScore = transform.Find("HighScore").gameObject;
        highScoreToPut = highScore.GetComponent<Text>();
        #endregion
        #region check to see if 2 player
        if (GameManagement.instance.isTwoPlayer)
        {
            player2Score.SetActive(true);
        }
        #endregion
        #region checks to see if new high score
        oldHigh = GameManagement.instance.highScore;
        if (GameManagement.instance.player1Points > GameManagement.instance.highScore)
        {
            GameManagement.instance.highScore = GameManagement.instance.player1Points;
        }
        if (GameManagement.instance.player2Points > GameManagement.instance.highScore)
        {
            GameManagement.instance.highScore = GameManagement.instance.player2Points;
        }
        #endregion
        #region shows points

        if (oldHigh == GameManagement.instance.highScore)
        {
            if (highScore.name == "HighScore")
            {
                highScoreToPut.text = "The High Score Is:\n" + GameManagement.instance.highScore + "\nBetter Luck Next Time";
            }
        }
        else if (oldHigh < GameManagement.instance.highScore)
        {
            if (highScore.name == "HighScore")
            {
                if (GameManagement.instance.player1Points == GameManagement.instance.highScore)
                {
                    highScoreToPut.text = "The New High Score Is:\n" + GameManagement.instance.highScore + "\nCongrats Player 1";
                }
                else if (GameManagement.instance.player2Points == GameManagement.instance.highScore)
                {
                    highScoreToPut.text = "The New High Score Is:\n" + GameManagement.instance.highScore + "\nCongrats Player 2";
                }
            }
        }
        if (player1Score.name == "Player1Score")
        {
            player1ScoreToPut.text = "Player 1 Score:\n" + GameManagement.instance.player1Points;
        }
        if (player2Score.name == "Player2Score")
        {
            player2ScoreToPut.text = "Player 2 Score:\n" + GameManagement.instance.player2Points;
        }
        #endregion
        #region reset values
        GameManagement.instance.player1 = player1; //resets player to original player character
        GameManagement.instance.player2 = player2; //resets player to original player character
        GameManagement.instance.player1Lifes = GameManagement.instance.origP1Lives; //resets player lives
        GameManagement.instance.player2Lifes = GameManagement.instance.origP2Lives;
        GameManagement.instance.player1LifeUp = GameManagement.instance.origLifeUp; //resets life up count
        GameManagement.instance.player2LifeUp = GameManagement.instance.origLifeUp;
        GameManagement.instance.player1Points = 0; //resets points
        GameManagement.instance.player2Points = 0;
        #endregion
        #region starts game over music
        GameManagement.instance.gameMusic.Stop();
        GameManagement.instance.gameMusic.clip = GameManagement.instance.gameOverMusic;
        GameManagement.instance.gameMusic.Play();
        #endregion

        PlayerPrefs.SetFloat("3D Asteroids High Score", GameManagement.instance.highScore); //saves score once gameover is reached
        PlayerPrefs.Save(); //saves score in registry
    }
}
