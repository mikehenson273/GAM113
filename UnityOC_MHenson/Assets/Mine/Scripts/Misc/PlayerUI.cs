using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    private Text lifeToPut,
                scoreToPut,
                healthDisplay,
                highScoreToPut;
    private GameObject lifeText,
                      scoreText,
                      healthText,
                      highScore;
	// Use this for initialization
	void Start ()
    {
        lifeText = transform.Find("LivesText").gameObject;
        lifeToPut = lifeText.GetComponent<Text>();
        scoreText = transform.Find("ScoreText").gameObject;
        scoreToPut = scoreText.GetComponent<Text>();
        healthText = transform.Find("HealthText").gameObject;
        healthDisplay = healthText.GetComponent<Text>();
        highScore = transform.Find("HighScore").gameObject;
        highScoreToPut = highScore.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
		if (transform.parent.tag == "Player1") //displays player 1's information
        {
            if (lifeText.name == "LivesText")
            {
                lifeToPut.text = "Lives: " + GameManagement.instance.player1Lifes;
            }
            if (scoreText.name == "ScoreText")
            {
                scoreToPut.text = "Score: " + GameManagement.instance.player1Points;
            }
            if (healthText.name == "HealthText")
            {
                healthDisplay.text = "Health: " + transform.parent.GetComponent<AllEntityControls>().shipHealth + " / " + transform.parent.GetComponent<PlayerSpaceShip>().originalLife;
            }
            if (highScore.name == "HighScore")
            {
                highScoreToPut.text = "High Score:\n" + GameManagement.instance.highScore;
            }
        }
        if (transform.parent.tag == "Player2") //displays player 2's information
        {
            if (lifeText.name == "LivesText")
            {
                lifeToPut.text = "Lives: " + GameManagement.instance.player2Lifes;
            }
            if (scoreText.name == "ScoreText")
            {
                scoreToPut.text = "Score: " + GameManagement.instance.player2Points;
            }
            if (healthText.name == "HealthText")
            {
                healthDisplay.text = "Health: " + transform.parent.GetComponent<PlayerSpaceShip>().originalLife + " / " + transform.parent.GetComponent<AllEntityControls>().shipHealth;
            }
            if (highScore.name == "HighScore")
            {
                highScoreToPut.text = "High Score:\n" + GameManagement.instance.highScore;
            }
        }
    }
}
