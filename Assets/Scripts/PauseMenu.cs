using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public  GameManager game;
    public TMP_Text finishedtext;
    public TMP_Text Scoretext;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Scoretext.text = "Score: " + game.score.ToString();

    }
    public void Finished()
    {
        finishedtext.text = "Game Finished";
        
    }
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    public void LoadCurrentLevel(){
        Time.timeScale =1f;
        SceneManager.LoadScene("Level" + game.level.ToString());
    }
    public void LoadCaveLevel(){
        Time.timeScale =1f;
        SceneManager.LoadScene("Cave");
    }
    public void LoadRandomLevel(){
        Time.timeScale =1f;
        SceneManager.LoadScene("Random");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
