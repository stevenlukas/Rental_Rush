using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public float tapeProgress = 0;
    public float tapeSize;
    public float tapePercent;

    public Vector2 startPos;

    [SerializeField]
    GameObject finalBlip;
    [SerializeField]
    UIController uiController;
    [SerializeField]
    LevelController levelController;
    [SerializeField]
    Transform playerPos;

    [SerializeField]
    float impactPenalty;

    public float timeRemaining;
    public float initialTime;

    public bool tapeRewound = false;
    public bool gameLost = false;
    public bool gameWon = false;
    public bool canPause = true;
    public bool gameStart = false;
    public bool tutorial = false;

    void Start()
    {
        startPos = playerPos.position;
        Debug.Log(PlayerPrefs.GetFloat($"Level{levelController.currentLevel} Time"));
        initialTime = timeRemaining;
        if(tutorial)
        {
            gameStart = false;
            Time.timeScale = 0;
        }
    }
    void Update()
    {
        tapePercent = tapeProgress / tapeSize;
        timeRemaining -= Time.deltaTime;

        if(tapeProgress >= tapeSize)
        {
            //tape is full

            tapeRewound = true;
        }

        if(tapePercent > 1)
        {
            tapePercent = 1;
        }

        if(timeRemaining <= 0 && !gameLost)
        {
            Lose();
        }
    }

    public void ImpactPenalty()
    {
        timeRemaining -= impactPenalty;
        
        playerPos.position = startPos;
    }

    public void Win()
    {
        gameWon = true;
        canPause = false;
        if (PlayerPrefs.GetFloat($"Level{levelController.currentLevel} Time") == 0 || PlayerPrefs.GetFloat($"Level{levelController.currentLevel} Time") > uiController.endTimeElapsed)
        {
            PlayerPrefs.SetFloat($"Level{levelController.currentLevel} Time", uiController.endTimeElapsed);
        }
        PlayerPrefs.SetInt($"Level{levelController.currentLevel}", 1);
        Time.timeScale = 0;
    }

    void Lose()
    {
        Time.timeScale = 0;
        gameLost = true;
        canPause = false;
    }

    public void StartGame()
    {
        tutorial = false;
        finalBlip.active = false;
        gameStart = true;
        Time.timeScale = 1;
    }
}
