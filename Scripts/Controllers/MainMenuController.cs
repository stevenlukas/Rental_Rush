using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    GameObject mainMenuContainer;
    [SerializeField]
    GameObject levelSelectContainer;
    [SerializeField]
    GameObject quitConfirmContainer;
    [SerializeField]
    GameObject highScoreContainer;
    [SerializeField]
    GameObject[] scoreContainers;
    [SerializeField]
    TextMeshProUGUI[] scoreText;

    [SerializeField]
    GameObject[] levels;

    float endTimeElapsed;
    float endMinutes;
    float endSeconds;
    float endMiliseconds;

    void Start()
    {
        if (PlayerPrefs.GetInt("Level1") == 1)
        {
            levels[0].active = true;
        }
        else
        {
            levels[0].active = false;
        }

        if (PlayerPrefs.GetInt("Level2") == 1)
        {
            levels[1].active = true;
        }
        else
        {
            levels[1].active = false;
        }

        if (PlayerPrefs.GetInt("Level3") == 1)
        {
            levels[2].active = true;
        }
        else
        {
            levels[2].active = false;
        }

        if (PlayerPrefs.GetInt("Level4") == 1)
        {
            levels[3].active = true;
        }
        else
        {
            levels[3].active = false;
        }

        if (PlayerPrefs.GetInt("Level5") == 1)
        {
            levels[4].active = true;
        }
        else
        {
            levels[4].active = false;
        }
    }

    //Main Menu
    public void NewGame()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void LevelSelect()
    {
        levelSelectContainer.active = true;
        mainMenuContainer.active = false;
    }
    public void HighScores()
    {
        if(PlayerPrefs.GetInt("Level1") == 1)
        {
            endTimeElapsed = PlayerPrefs.GetFloat("Level1 Time");
            endMinutes = Mathf.FloorToInt(endTimeElapsed / 60);
            endSeconds = Mathf.FloorToInt(endTimeElapsed % 60);
            endMiliseconds = (endTimeElapsed % 1) * 1000;
            scoreText[0].text = $"Night One Time Elapsed: " + string.Format("{0:00}:{1:00}:{2:00}", endMinutes, endSeconds, endMiliseconds);
            scoreContainers[0].active = true;
        }
        if (PlayerPrefs.GetInt("Level2") == 1)
        {
            endTimeElapsed = PlayerPrefs.GetFloat("Level2 Time");
            endMinutes = Mathf.FloorToInt(endTimeElapsed / 60);
            endSeconds = Mathf.FloorToInt(endTimeElapsed % 60);
            endMiliseconds = (endTimeElapsed % 1) * 1000;
            scoreText[1].text = $"Night Two Time Elapsed: " + string.Format("{0:00}:{1:00}:{2:00}", endMinutes, endSeconds, endMiliseconds);
            scoreContainers[1].active = true;
        }
        if (PlayerPrefs.GetInt("Level3") == 1)
        {
            endTimeElapsed = PlayerPrefs.GetFloat("Leve3 Time");
            endMinutes = Mathf.FloorToInt(endTimeElapsed / 60);
            endSeconds = Mathf.FloorToInt(endTimeElapsed % 60);
            endMiliseconds = (endTimeElapsed % 1) * 1000;
            scoreText[2].text = $"Night Three Time Elapsed: " + string.Format("{0:00}:{1:00}:{2:00}", endMinutes, endSeconds, endMiliseconds);
            scoreContainers[2].active = true;
        }
        if (PlayerPrefs.GetInt("Level4") == 1)
        {
            endTimeElapsed = PlayerPrefs.GetFloat("Level4 Time");
            endMinutes = Mathf.FloorToInt(endTimeElapsed / 60);
            endSeconds = Mathf.FloorToInt(endTimeElapsed % 60);
            endMiliseconds = (endTimeElapsed % 1) * 1000;
            scoreText[3].text = $"Night Four Time Elapsed: " + string.Format("{0:00}:{1:00}:{2:00}", endMinutes, endSeconds, endMiliseconds);
            scoreContainers[3].active = true;
        }
        if (PlayerPrefs.GetInt("Level5") == 1)
        {
            endTimeElapsed = PlayerPrefs.GetFloat("Level5 Time");
            endMinutes = Mathf.FloorToInt(endTimeElapsed / 60);
            endSeconds = Mathf.FloorToInt(endTimeElapsed % 60);
            endMiliseconds = (endTimeElapsed % 1) * 1000;
            scoreText[4].text = $"Night Five Time Elapsed: " + string.Format("{0:00}:{1:00}:{2:00}", endMinutes, endSeconds, endMiliseconds);
            scoreContainers[4].active = true;
        }

        highScoreContainer.active = true;
        mainMenuContainer.active = false;
    }
    public void Credits()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadSceneAsync(6);
    }
    public void Quit()
    {
        mainMenuContainer.active = false;
        quitConfirmContainer.active = true;
    }
    //Level Selection

    public void levelOneLoad()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadSceneAsync(1);
    }

    public void levelTwoLoad()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadSceneAsync(2);
    }

    public void levelThreeLoad()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadSceneAsync(3);
    }

    public void levelFourLoad()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadSceneAsync(4);
    }

    public void levelFiveLoad()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadSceneAsync(5);
    }

    //Quit Confirmations
    public void Confirm()
    {
        Application.Quit();
    }
    //High Scores



    //Global to all Menus
    public void Back()
    {
        mainMenuContainer.active = true;
        levelSelectContainer.active = false;
        quitConfirmContainer.active = false;
        highScoreContainer.active = false;
    }





    

    
}
