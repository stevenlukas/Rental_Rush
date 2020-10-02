using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public bool paused = false;

    public int currentLevel = 0;

    UIController uiController;

    // Start is called before the first frame update
    void Start()
    {
        uiController = GameObject.FindGameObjectWithTag("UIController").GetComponent<UIController>();
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetButtonDown("Cancel"))
        {
            if(paused)
            {
                paused = false;
                Time.timeScale = 0;
                uiController.Pause(true);
            }
            else
            {
                paused = true;
                Time.timeScale = 1;
                uiController.Pause(false);
            }
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadSceneAsync(currentLevel);
        Time.timeScale = 1;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadSceneAsync(0);
        Time.timeScale = 1;
    }

    public void NextLevel()
    {
        SceneManager.LoadSceneAsync(currentLevel + 1);
        Time.timeScale = 1;
    }
}
