using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // TRASH AND BIN
    [SerializeField] GameObject blueBin;
    [SerializeField] GameObject greenBin;
    [SerializeField] GameObject redBin;
    [SerializeField] GameObject blackBin;

    [SerializeField] GameObject spawner;
    [SerializeField] List<GameObject> trashItems;
    
    // MANAGING
    bool startedGame;
    bool firstTry = false;
    int score;

    public bool FirstTry {
        get { return firstTry; }
        set { firstTry = value; }
    }

    // COMPONENTS
    [SerializeField] Text scoreText;

    // TIMER
    float timeCount = 60f;
    public float TimeCount => timeCount;
    
    [SerializeField] Text timerText;
    [SerializeField] bool isPlaying; // Turn to true when clicking the first card

    // PROPERTIES
    [SerializeField] GameObject blockPanel;

    void Start()
    {
        timerText.text = "Tiempo: " + timeCount.ToString("F2");
        scoreText.text = "Puntos: " + score;
        SpawnTrashItem();
    }

    void Update()
    {
        // START GAME: Set isPlaying to true
        if (firstTry && timeCount == 60f)
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
        isPlaying = false;
        CrossSceneInformation.Score = Convert.ToInt32(scoreText.text.Split(' ')[1]);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(2);
    }

    void HandleTimer()
    {
        timeCount -= Time.deltaTime;
        timerText.text = "Tiempo: " + timeCount.ToString("F2");
    }

    public void SpawnTrashItem()
    {
        var rand = UnityEngine.Random.Range(0, trashItems.Count);
        Instantiate(trashItems[rand], spawner.transform);
    }

    public void SpawnTrashItem(GameObject bin)
    {    
        if(spawner.transform.childCount > 0)
        {
            var item = spawner.transform.GetChild(0).gameObject;
            CheckIfCorrect(item, bin);
            Destroy(item);
        }

        var rand = UnityEngine.Random.Range(0, trashItems.Count);
        Instantiate(trashItems[rand], spawner.transform);
    }

    void CheckIfCorrect(GameObject item, GameObject bin)
    {
        var itemType = item.GetComponent<Trash>().TrashType;

        if(itemType == "black" && bin.name == "BlackBinButton"
        || itemType == "blue"  && bin.name == "BlueBinButton"
        || itemType == "red"   && bin.name == "RedBinButton"
        || itemType == "green" && bin.name == "GreenBinButton")
        {
            GainPoints(true);
        }
        else
        {
            GainPoints(false);
        }
    }

    void GainPoints(bool scored)
    {
        if(scored)
            score += 100;
        else
            score -= 50;
        
        scoreText.text = "Puntos: " + score;
    }
}