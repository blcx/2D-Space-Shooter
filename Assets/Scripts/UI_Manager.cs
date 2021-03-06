﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    [SerializeField]
    private Text _ScoreText;
    [SerializeField]
    private Sprite[] _livesSprites;
    [SerializeField]
    private Image _Lives_image;

    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartText;

    private GameManager _gameManager;









    // Start is called before the first frame update
    void Start()
    {
        
        _ScoreText.text = "Score: " + 0;
        _gameOverText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();


    }

    public void UpdateScore(int playerScore)
    {

        _ScoreText.text = "Score: " + playerScore.ToString();

    }





    public void UpdateLives(int currentLives)
    {
        _Lives_image.sprite = _livesSprites[currentLives];

        if (currentLives == 0)
        {

            GameOverSequence();

        }
    }

    void GameOverSequence()
    {
        _gameManager.GameOver();
        _gameOverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());


    }


    IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {

            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        
        }
    
    
    }






























}
