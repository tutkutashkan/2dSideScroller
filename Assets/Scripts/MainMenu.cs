using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public string menuName;
    public bool open;
    
    public void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene("Level1");
        }
    }
    public void PlayBeach ()
    {
        SceneManager.LoadScene("Level1");
    }
    public void PlayCave ()
    {
        SceneManager.LoadScene("Cave");
    }
    public void PlayRandom()
    {
        SceneManager.LoadScene("Random");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void Open()
    {
        open = true;
        gameObject.SetActive(true);
    }

    public void Close()
    {
        open = false;
        gameObject.SetActive(false);
    }
}

