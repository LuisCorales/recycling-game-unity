using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // TRASH AND BIN
    
    // MANAGING
    bool startedGame;
    bool firstTry;

    // COMPONENTS
    [SerializeField] Text scoreText;

    // TIMER
    float timeCount = 60;
    [SerializeField] Text timerText;
    [SerializeField] bool isPlaying; // Turn to true when clicking the first card

    // PROPERTIES
    [SerializeField] GameObject blockPanel;

    void Start()
    {
        timerText.text = timeCount.ToString("F2");
    }

    void Update()
    {
        // START GAME: Set isPlaying to true
        if (firstTry && timeCount == 60)
        {
            StartGame();
        }

        // START TIMER
        if (isPlaying)
        {
            HandleTimer();
        }

        // END GAME TO END MENU
        if (isPlaying && timeCount == 0)
        {
            isPlaying = false;
            StartCoroutine(EndGame());
        }
    }

    void StartGame()
    {
        isPlaying = true;
    }

    IEnumerator EndGame()
    {
        CrossSceneInformation.Score = Convert.ToInt32(scoreText.text);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(2);
    }

    void HandleTimer()
    {
        timeCount -= Time.deltaTime;
        timerText.text = timeCount.ToString("F2");
    }
}