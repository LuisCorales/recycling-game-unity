using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    // COMPONENTS
    [SerializeField] Text scoreText;
    [SerializeField] Text leaderboard;

    // INFO
    int score;
    string username;
    string email;

    [SerializeField] dreamloLeaderBoard dreamloLB;
    List<dreamloLeaderBoard.Score> scoreList;

    void Start()
    {
        score = CrossSceneInformation.Score;
        username = CrossSceneInformation.Username;
        email = CrossSceneInformation.Email;

        SetScoreText(score);

        dreamloLB = dreamloLeaderBoard.GetSceneDreamloLeaderboard();

        if(score < 0)
            score = 0;

        // Add user, score, seconds, email
        dreamloLB.AddScore(username , Convert.ToInt32(score), 0, email);
        StartCoroutine(Tryer()); 
    }

    public void SetScoreText(float score)
    {
        scoreText.text = "¡Conseguiste " + score + " puntos!";
    }

    IEnumerator Tryer()
    {
        do
        {
            scoreList = dreamloLB.ToListHighToLow();

			if (scoreList == null) 
			{
				leaderboard.text = "(Cargando...)";
			} 
			else 
			{
                leaderboard.text = "";
				int maxToDisplay = 10;
				int count = 0;

				foreach (dreamloLeaderBoard.Score currentScore in scoreList)
				{
					count++;

                    leaderboard.text += count + ". Puntaje: " + currentScore.score.ToString() + " - " + currentScore.playerName.Replace("+"," ") + "\n";

                    if (count >= maxToDisplay) 
                        break;
				}    
            }

            yield return new WaitForSeconds(3);

        }while(scoreList.Count==0);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0); // Load the start menu
    }
}
