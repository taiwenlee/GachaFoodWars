using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    protected Canvas pauseMenu;
    //public GameObject restartMenu;
    public static bool isPaused;
    public AudioSource pauseMenuSFX;

    void Start()
    {

        pauseMenu = GetComponent<Canvas>();
        pauseMenu.enabled = false;
        //restartMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenuSFX.Play();
        pauseMenu.enabled = true;
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenuSFX.Play();
        pauseMenu.enabled = false;
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void GoToMainMenu()
    {
        pauseMenuSFX.Play();
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame()
    {
        pauseMenuSFX.Play();
        Application.Quit();
    }
}
