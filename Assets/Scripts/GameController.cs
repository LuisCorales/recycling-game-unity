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
    bool firstTry = false;

    // COMPONENTS
    [SerializeField] Text scoreText;

    // TIMER
    float timeCount = 5f;
    [SerializeField] Text timerText;
    [SerializeField] bool isPlaying; // Turn to true when clicking the first card

    // PROPERTIES
    [SerializeField] GameObject blockPanel;

    void Start()
    {
        timerText.text = timeCount.ToString("F2");
        isPlaying = true;
    }

    void Update()
    {
        // START GAME: Set isPlaying to true
        if (firstTry && timeCount == 5f)
        {
            StartGame();
        }

        // START TIMER
        if (isPlaying)
        {
            HandleTimer();
        }

        // END GAME TO END MENU
        if (isPlaying && timeCount <= 0f)
        {
            isPlaying = false;
            Debug.Log("WIN");
            StartCoroutine(EndGame());
        }
    }

    void StartGame()
    {
        isPlaying = true;
    }

    // Show instructions of the game
    IEnumerator ShowInstructions()
    {
        yield return new WaitForSeconds(10f);
    }

    IEnumerator EndGame()
    {
        CrossSceneInformation.Score = Convert.ToInt32(scoreText.text.Split(' ')[1]);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(2);
    }

    void HandleTimer()
    {
        timeCount -= Time.deltaTime;
        timerText.text = "Tiempo: " + timeCount.ToString("F2");
    }
}