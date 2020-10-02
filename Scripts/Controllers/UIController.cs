using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField]
    GameController gameController;
    [SerializeField]
    LevelController levelController;

    [SerializeField]
    TextMeshProUGUI time;
    [SerializeField]
    TextMeshProUGUI level;
    [SerializeField]
    TextMeshProUGUI timeElapsed;

    [SerializeField]
    UnityEngine.UI.Slider progressSlider;

    [SerializeField]
    GameObject pauseMenu;
    [SerializeField]
    GameObject confirmMenu;
    [SerializeField]
    GameObject winContainer;
    [SerializeField]
    GameObject gameOverContainer;
    [SerializeField]
    GameObject hudContainer;
    [SerializeField]
    GameObject[] tutorialContainers;

    float minutes;
    float seconds;
    float miliseconds;

    float endMinutes;
    float endSeconds;
    float endMiliseconds;
    public float endTimeElapsed;

    int tutorialIteration = 0;

    void Start()
    {
        progressSlider.maxValue = gameController.tapeSize;
        if(gameController.tutorial)
        {
            TutorialBlip();
        }
    }

    void Update()
    {
        minutes = Mathf.FloorToInt(gameController.timeRemaining / 60);
        seconds = Mathf.FloorToInt(gameController.timeRemaining % 60);
        miliseconds = (gameController.timeRemaining % 1) * 1000;
        time.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, miliseconds);
        level.text = ($"Night: {levelController.currentLevel}");
        progressSlider.value = gameController.tapeProgress;

        endTimeElapsed = gameController.initialTime - gameController.timeRemaining;
        endMinutes = Mathf.FloorToInt(endTimeElapsed / 60);
        endSeconds = Mathf.FloorToInt(endTimeElapsed % 60);
        endMiliseconds = (endTimeElapsed % 1) * 1000;
        timeElapsed.text = "Time Elapsed: " + string.Format("{0:00}:{1:00}:{2:00}", endMinutes, endSeconds, endMiliseconds);

        if(gameController.gameLost)
        {
            gameOverContainer.active = true;
        }

        if(gameController.gameWon)
        {
            winContainer.active = true;
            hudContainer.active = false;
        }
    }

    public void Pause(bool paused)
    {
        if(gameController.canPause)
        {
            if (paused)
            {
                pauseMenu.active = true;
            }
            else
            {
                pauseMenu.active = false;
            }
        }
    }

    public void Resume()
    {
        levelController.paused = false;
        Time.timeScale = 1;
        Pause(false);
    }

    public void RestartLevel()
    {
        levelController.RestartLevel();
    }

    public void MainMenu()
    {
        levelController.MainMenu();
    }

    public void QuitGame()
    {
        pauseMenu.active = false;
        confirmMenu.active = true;
    }

    public void Confirm()
    {
        levelController.Quit();
    }

    public void Cancel()
    {
        confirmMenu.active = false;
        pauseMenu.active = true;
    }

    public void NextLevel()
    {
        levelController.NextLevel();
    }

    public void TutorialBlip()
    {
        switch (levelController.currentLevel)
        {
            case 1:
                switch(tutorialIteration)
                {
                    case 0:
                        //explain situation
                        tutorialContainers[tutorialIteration].active = true;
                        tutorialIteration++;
                        break;

                    case 1:
                        //explain who killer is and your goal
                        tutorialContainers[tutorialIteration - 1].active = false;
                        tutorialContainers[tutorialIteration].active = true;
                        tutorialIteration++;

                        break;

                    case 2:
                        //explain how to move
                        tutorialContainers[tutorialIteration - 1].active = false;
                        tutorialContainers[tutorialIteration].active = true;
                        tutorialIteration++;

                        break;

                    case 3:
                        //explain what pickups are
                        tutorialContainers[tutorialIteration - 1].active = false;
                        tutorialContainers[tutorialIteration].active = true;

                        break;
                }

                break;

            case 2:

                switch (tutorialIteration)
                {
                    case 0:
                        //congratulate survival
                        tutorialContainers[tutorialIteration].active = true;
                        tutorialIteration++;

                        break;

                    case 1:
                        //explain chasers
                        tutorialContainers[tutorialIteration - 1].active = false;
                        tutorialContainers[tutorialIteration].active = true;
                        tutorialIteration++;

                        break;

                    case 2:
                        //advise to press any key to continue
                        tutorialContainers[tutorialIteration - 1].active = false;
                        tutorialContainers[tutorialIteration].active = true;
                        tutorialIteration++;

                        break;

                    default:
                        //remove tutorial display
                        tutorialContainers[tutorialIteration - 1].active = false;
                        tutorialContainers[tutorialIteration].active = true;
                        Time.timeScale = 1;
                        break;
                }

                break;

            case 3:

                switch (tutorialIteration)
                {
                    case 0:
                        //congratulate survival
                        tutorialContainers[tutorialIteration].active = true;
                        tutorialIteration++;

                        break;

                    case 1:
                        //explain difficulty increments
                        tutorialContainers[tutorialIteration - 1].active = false;
                        tutorialContainers[tutorialIteration].active = true;
                        tutorialIteration++;
                        break;

                    case 2:
                        //advise to press any key to continue
                        tutorialContainers[tutorialIteration - 1].active = false;
                        tutorialContainers[tutorialIteration].active = true;
                        tutorialIteration++;
                        break;

                    default:
                        //remove tutorial display
                        tutorialContainers[tutorialIteration - 1].active = false;
                        tutorialContainers[tutorialIteration].active = true;
                        Time.timeScale = 1;
                        break;
                }

                break;

            default:

                switch (tutorialIteration)
                {
                    case 0:
                        //advise to press any key to continue
                        tutorialContainers[tutorialIteration].active = true;
                        tutorialIteration++;
                        break;

                    default:
                        //remove tutorial display
                        tutorialContainers[tutorialIteration - 1].active = false;
                        tutorialContainers[tutorialIteration].active = true;
                        Time.timeScale = 1;
                        break;
                }

                break;
        }
    }
}
